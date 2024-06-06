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
            


            finishCanvas.SetActive(true);

        }

    }
}
