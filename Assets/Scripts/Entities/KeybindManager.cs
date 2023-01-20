using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Keybind Manager", menuName = "Player/Keybinds")]
public class KeybindManager : ScriptableObject
{
    [Header("Movement Keybinds")]
    public static KeyCode forward = KeyCode.W;
    public static KeyCode backwards = KeyCode.S;
    public static KeyCode left = KeyCode.A;
    public static KeyCode right = KeyCode.D;
    public static KeyCode jump = KeyCode.Space;
    public static KeyCode sprint = KeyCode.LeftShift;

    [Header("Attack Keybinds")]
    public static KeyCode primaryAction = KeyCode.Mouse0;
    public static KeyCode secondaryAction = KeyCode.Mouse1;

    [Header("Functional Keybinds")]
    public static KeyCode openInventory = KeyCode.E;
    public static KeyCode switchBackSlot = KeyCode.F;
    public static KeyCode switchFocus = KeyCode.R;

    [Header("Misc Keybinds")]
    public static KeyCode pauseGame = KeyCode.Escape;
}
