using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Menu : MonoBehaviour
{
    GameController _gameController;
    private void Start()
    {
        
        _gameController = FindObjectOfType<GameController>();
        
    }
    
    // Start is called before the first frame update
    public void playgame()
    {
        SceneManager.LoadScene(1);
        //_gameController.ReSetScore();
        GameController.ReSetScore();

    }

    // Update is called once per frame
    public void Exitgame()
    {
        Application.Quit();
    }

}
