using UnityEngine.UIElements;

namespace roguelike.environment.entity.npc {
    public class NPC : Entity {

        public UIDocument dialogWindow;

        protected override void Awake() {
            // logic here
            base.Awake();
        }
    }
}