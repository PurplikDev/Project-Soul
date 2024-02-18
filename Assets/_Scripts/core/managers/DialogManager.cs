using roguelike.core.utils.dialogutils;
using roguelike.environment.entity.player;
using roguelike.environment.ui.dialog;
using roguelike.environment.ui.statemachine;
using roguelike.system.singleton;

namespace roguelike.system.manager {
    public class DialogManager : Singleton<DialogManager> {

        private static UIStateMachine _uiStateMachine;

        protected virtual void Start() {
            _uiStateMachine = GameManager.Instance.Player.UIStateMachine;
        }

        public static void StartDialog(Player player, DialogData data) {
            _uiStateMachine.

            var dialogScreen = player.DialogScreen.GetComponent<DialogScreen>();
            dialogScreen.gameObject.SetActive(true);
            dialogScreen.Display(data);
        }
    }
}