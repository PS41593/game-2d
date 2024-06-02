using game;
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

            // tải đữ liệu trong file hiển thị lên bảng thành tích

            finishCanvas.SetActive(true);
        }

    }
}
