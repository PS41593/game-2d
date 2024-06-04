using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private static int score =0 ;
    [SerializeField] TextMeshProUGUI ScoreText ;

    
    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "Score: "+score;
    }
    public void AddScore(int ScoreToAdd)
    {
        score += ScoreToAdd;
        ScoreText.text = "Score: " + score; 
    }
    // Update is called once per frame
   
   public int getScore() { return score; }
}

