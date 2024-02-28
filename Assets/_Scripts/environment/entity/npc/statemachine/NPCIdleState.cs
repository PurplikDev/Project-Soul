using UnityEngine;

namespace roguelike.environment.entity.npc.statemachine {
    public class NPCIdleState : NPCBaseState {
        public NPCIdleState(NPCStateMachine stateMachine) : base(stateMachine, NPCState.IDLE) {}

        public override void EnterState() {
            Debug.Log("enter idle npc");
        }

        public override void ExitState() {
            Debug.Log("exit idle npc");
        }

        public override NPCState GetNextState() {
            return NPCState.IDLE;
        }

        public override void UpdateState() {
            
        }
    }
}