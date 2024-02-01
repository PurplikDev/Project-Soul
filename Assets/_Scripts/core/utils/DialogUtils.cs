using UnityEngine;

namespace roguelike.core.utils.dialogutils {
    public class DialogUtils {
    }

    public class DialogData {

        public Sprite DialogPortrait { get; private set; }
        public string DialogNametag { get; private set; }
        public string DialogText { get; private set; }

        public DialogData(Sprite portrait, string nametag, string text) {
            DialogPortrait = portrait;
            DialogNametag = nametag;
            DialogText = text;
        }
    }
}