using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Keybind Manager", menuName = "Player/Keybinds")]
public class KeybindManager : ScriptableObject
{
    [Header("Movement Keybinds")]
    public KeyCode forward = KeyCode.W;
    public KeyCode backwards = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode jump = KeyCode.Space;

    [Header("Attack Keybinds")]
    public KeyCode primaryAction = KeyCode.Mouse0;
    public KeyCode secondaryAction = KeyCode.Mouse1;

    [Header("Functional Keybinds")]
    public KeyCode openInventory = KeyCode.E;
    public KeyCode switchBackSlot = KeyCode.F;
    public KeyCode switchFocus = KeyCode.R;

    //[Header("Misc Keybinds")]
}
