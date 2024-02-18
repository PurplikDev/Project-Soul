using roguelike.core.utils.dialogutils;
using roguelike.environment.entity.player;
using roguelike.environment.ui.dialog;
using roguelike.system.singleton;
using UnityEngine;

namespace roguelike.system.manager {
    public class DialogManager : Singleton<DialogManager> {

        public static void StartDialog(Player player, DialogData data) {
            var dialogScreen = player.GetComponentInChildren<DialogScreen>();
            dialogScreen.gameObject.SetActive(true);
            dialogScreen.Display(data);
        }
    }
}