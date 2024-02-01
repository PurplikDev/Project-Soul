using roguelike.core.utils.dialogutils;
using roguelike.environment.entity.player;
using roguelike.environment.ui.dialog;
using roguelike.system.singleton;
using UnityEngine;

namespace roguelike.system.manager {
    public class DialogManager : Singleton<DialogManager> {

        private void Start() {

            var portrait = Resources.Load<Sprite>("sprites/items/test");

            StartDialog(GameManager.Instance.Player, new DialogData(portrait, "joe", "joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe"));
        }

        public static void StartDialog(Player player, DialogData data) {
            var dialogScreen = player.GetComponentInChildren<DialogScreen>();
            dialogScreen.gameObject.SetActive(true);
            dialogScreen.Display(data);
        }
    }
}