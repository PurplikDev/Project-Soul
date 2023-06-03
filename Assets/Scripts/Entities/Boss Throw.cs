using io.purplik.ProjectSoul.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrow : MonoBehaviour
{
    public void Throw()
    {
        Debug.Log("throwing");
    }

    public void Stomp()
    {
        GetComponentInParent<BossEntity>().player.Damage((int) GetComponentInParent<BossEntity>().damage.Value);
    }
}
