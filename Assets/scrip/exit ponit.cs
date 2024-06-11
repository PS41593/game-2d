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


    Skillboss _skillboss;


    private void Start()
    {
        storageHelper = new StorageHelper();
        storageHelper.LoadData();
        played = storageHelper.played;
        _skillboss = FindObjectOfType<Skillboss>();
    }
    private void Update()
    {
        if (_skillboss.Hp <= 0)
        {
            informationcanvas.SetActive(false);
            //Debug.Log("fix game");
            try
            {
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
                //Debug.Log("Count: " + played.plays.Count);
                // tải đữ liệu trong file hiển thị lên bảng thành tích
                played.plays.Sort((x, y) => y.score.CompareTo(x.score));
                for (int i = 0; i < 5; i++)
                {
                    var rowIntace = Instantiate(row, row.transform.parent);
                    rowIntace.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = (i + 1).ToString();
                    rowIntace.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = played.plays[i].score.ToString();
                    rowIntace.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = played.plays[i].timeplayed.ToString();

                }
                finishCanvas.SetActive(true);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }

}
        /*private void OnTriggerEnter2D(Collider2D other)
        {
            if (_skillboss.Hp <= 0)
            {
                finishCanvas.SetActive(true);
                //Debug.Log("fix game");
                try {
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
                    played.plays.Sort((x, y) => y.score.CompareTo(x.score));
                    for (int i = 0; i < 5; i++)
                    {
                        var rowIntace = Instantiate(row, row.transform.parent);
                        rowIntace.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = (i + 1).ToString();
                        rowIntace.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = played.plays[i].score.ToString();
                        rowIntace.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = played.plays[i].timeplayed.ToString();

                    }
                    finishCanvas.SetActive(true);
                }catch (Exception e)
                {
                    Debug.Log(e);
                }


            }

        }*/
    
