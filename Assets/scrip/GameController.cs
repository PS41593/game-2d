using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] int score =0 ;
    [SerializeField] int live =3 ;
    [SerializeField] TextMeshProUGUI ScoreText ;
    [SerializeField] TextMeshProUGUI liveText;
    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "Coin: "+score;
    }
    public void AddScore(int ScoreToAdd)
    {
        score += ScoreToAdd;
        ScoreText.text = "Coin: " + score; 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
