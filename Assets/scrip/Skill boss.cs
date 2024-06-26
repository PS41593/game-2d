﻿using System.Collections;
using game;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;


public class Skillboss : MonoBehaviour
{
    [SerializeField]
    private float leftBoundary;
    [SerializeField]
    private float rightBoundary;
    [SerializeField]
    private bool _isMovingRight = true;
    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField] private Slider bot;
    public int Hp;
    public int maxHp = 100;

    [SerializeField] private GameObject itemPrefab;

    private float cooldown = 4f;
    private float fire = 0f;
    public GameObject bulletPrefab;
    public Transform guntransform;
    /*[SerializeField] GameObject informationcanvas;
    [SerializeField] GameObject finishCanvas;*/
    private StorageHelper storageHelper;
    private GameDataPlayed played;
    [SerializeField] GameObject row;
    void Start()
    {
        storageHelper = new StorageHelper();
        storageHelper.LoadData();
        played = storageHelper.played;
        Hp = maxHp;
        bot.value = Hp;
    }
    // Update is called once per frame
    void Update()
    {
        // Lấy vị trí hiện tại
        var currenPosition = transform.position;
        if (currenPosition.x > rightBoundary)
        {
            // Niếu vị trí hiện tại của snail > rightBoundary
            // Di chuyển sang trái
            _isMovingRight = false;
        }
        else if (currenPosition.x < leftBoundary)
        {
            // Niếu vi trí hiện tại của Snail < leftBoundary
            // Di chuyển sang phải
            _isMovingRight = true;
        }
        // Di chuyển ngang 
        // (1,0,0) * 1 * 0.02 = (0.02,0,0)
        var direction = Vector3.right;
        if (_isMovingRight == false)
        {
            direction = Vector3.left;
        }
        // var direction = _isMovingRight ? Vector3.right : Vector3.left; cái này bằng if trên
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Scale Hiện tại
        // Trái < 0 Phải >0
        var currentSacle = transform.localScale;
        var sli = bot.transform.localScale;
        if ((_isMovingRight && currentSacle.x < 0) || (_isMovingRight == false && currentSacle.x > 0))
        {
            currentSacle.x *= -1;
            sli *= -1;
        }
        transform.localScale = currentSacle;
        bot.transform.localScale = sli;
        if (Time.time > fire)
        {
            magic();
            fire = Time.time + cooldown;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "arrow")
        {
            Hp -= 10;
            bot.value = Hp;
            if (Hp <= 0)
            {
                Destroy(gameObject, 1f);
                StartCoroutine(GoUp());
                /*informationcanvas.SetActive(true);
                Debug.Log("fix game");
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
                    Time.timeScale = 0;
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }*/
            }
        }

    }


    IEnumerator GoUp()
    {
        //lay vi tri ban dau
        var startPosition = transform.position;
        //vi tri hien tai 
        var currentPosition = startPosition;
        while (true)
        {
            currentPosition.y += 0.1f;
            transform.localPosition = currentPosition;
            yield return new WaitForSeconds(0.02f);
            if (currentPosition.y >= startPosition.y + 1f)
            {
                break;
            }

        }
        StartCoroutine(GoDown());
    }
    IEnumerator GoDown()
    {
        //lay vi tri ban dau
        var startPosition = transform.position;
        //vi tri hien tai 
        var currentPosition = startPosition;
        while (true)
        {
            currentPosition.y -= 0.1f;
            transform.localPosition = currentPosition;
            yield return new WaitForSeconds(0.02f);
            if (currentPosition.y <= startPosition.y - 1f)
            {
                break;
            }

        }
        ////tao ra vat pham
        Instantiate(itemPrefab, transform.position, Quaternion.identity);
        ////bien mat khoi
        Destroy(gameObject);
    }
    public void magic()
    {
        bulletPrefab.transform.localScale = _isMovingRight ? new Vector2(6.276599f, 9.053264f) : new Vector2(-6.276599f, 9.053264f);
        // tao ra vien dan tai vi tri sung
        var onBullet = Instantiate(bulletPrefab, guntransform.position, Quaternion.identity);
        // Cho vien dan bay theo huong nhan vat                     
        var velocity = new Vector2(30f, 0);
        if (_isMovingRight == false)
        {
            velocity = new Vector2(-30f, 0);
        }
        onBullet.GetComponent<Rigidbody2D>()
            .velocity = velocity;
        // Destroy Dan
        Destroy(onBullet, 0.5f);
    }
}
