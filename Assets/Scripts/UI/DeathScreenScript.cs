using io.purplik.ProjectSoul.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenScript : MonoBehaviour
{
    [SerializeField] string[] insults = { "Maybe try getting good next time...", "Wow, you died like THAT?!, shame on you...", "I've seen more skill in [REDACTED]...", "Honestly, you suck.", "Biggest skill issue ever..." };
    [SerializeField] TextMeshProUGUI insultText;

    private void Awake()
    {
        insultText.text = insults[Random.Range(0,insults.Length)];
    }

    public void DeleteSave()
    {
        GameObject.Find("ItemSaveManager").GetComponent<ItemSaveManager>().DeleteAllSave();
        SceneManager.LoadScene(0);
    }
}
