using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    [SerializeField] GameObject howToPlay;

    public void StartGame()
    {
        GetComponent<SoundPlayer>().PlaySound();
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        GetComponent<SoundPlayer>().PlaySound();
        Application.Quit();
    }

    public void HowToPlay()
    {
        GetComponent<SoundPlayer>().PlaySound();
        howToPlay.SetActive(!howToPlay.activeSelf);
    }
}
