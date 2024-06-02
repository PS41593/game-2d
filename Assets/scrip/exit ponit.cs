using game;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class exitponit : MonoBehaviour
{
    [SerializeField] GameObject informationcanvas;
    [SerializeField] GameObject finishCanvas;
    private StorageHelper storageHelper;
    private GameDataPlayed played;
    [SerializeField] GameObject row;
    private void Start()
    {
        storageHelper = new StorageHelper();
        storageHelper.LoadData();
        played = storageHelper.played;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            informationcanvas.SetActive(false);
            
           var Score = FindObjectOfType<GameController>().getScore();
            // lưu Thành tích Của người chơi 
            var gamedata = new GameData()
            {
                score = Score,
                timeplayed = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };
            played.plays.Add(gamedata);
            storageHelper.SaveData();
            played = storageHelper.played;
            Debug.Log("Count: " + played.plays.Count);
            // tải đữ liệu trong file hiển thị lên bảng thành tích
            played.plays.Sort((x,y) => y.score.CompareTo(x.score));
            for (int i = 0; i < played.plays.Count; i++)
            {
                var rowIntace = Instantiate(row, row.transform.parent);
                rowIntace.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = (i + 1).ToString();
                rowIntace.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = played.plays[i].score.ToString();
                rowIntace.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = played.plays[i].timeplayed.ToString();

            }


            finishCanvas.SetActive(true);

        }

    }
}
