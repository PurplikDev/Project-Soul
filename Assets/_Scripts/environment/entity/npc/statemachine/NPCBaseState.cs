using roguelike.core.statemachine;
using roguelike.environment.entity.player;
using UnityEngine;

namespace roguelike.environment.entity.npc.statemachine {
    public abstract class NPCBaseState : BaseState<NPCState> {

        protected NPCStateMachine stateMachine;

        protected bool isPlayerClose = false;

        public NPCBaseState(NPCStateMachine stateMachine, NPCState key) : base(key) {
            this.stateMachine = stateMachine;
        }

        public override void OnTriggerEnter(Collider collider) {
            var player = collider.GetComponent<Player>();
            if(player != null) {
                isPlayerClose = true;
            }
        }

        public override void OnTriggerStay(Collider collider) { }

        public override void OnTriggerExit(Collider collider) {
            var player = collider.GetComponent<Player>();
            if (player != null) {
                isPlayerClose = false;
            }
        }

        
    }
}