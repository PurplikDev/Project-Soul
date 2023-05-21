using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ResetGame()
    {
        Debug.Log("Trying to reset game!");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
