using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void playgame()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    public void Exitgame()
    {
        Application.Quit();
    }
}
