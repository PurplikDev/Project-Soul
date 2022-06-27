using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager playerManager;

    void Awake() {
        playerManager = this;
    }

    #endregion

    public GameObject playerObject;
}
