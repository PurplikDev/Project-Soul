using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntityController : MonoBehaviour
{
    [SerializeField] EntityObject entityObject;
    [SerializeField] KeybindManager keybindManager;

    public enum PlayerState
    {
        IDLE,
        WALK,
        RUN,
        JUMP,
        INTERACTING
    }

    void Update()
    {
        
    }

    private void CheckInputs()
    {

    }
}
