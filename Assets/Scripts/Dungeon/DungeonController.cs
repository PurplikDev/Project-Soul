using UnityEngine;

public class DungeonController : MonoBehaviour
{
    public int loops = 0;
    private int limit;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        limit = Random.Range(2, 6);
    }
    public void Increase()
    {
        loops++;
    }

    public bool IsItTimeToEnd()
    {
        return !(limit < loops);
    }
}
