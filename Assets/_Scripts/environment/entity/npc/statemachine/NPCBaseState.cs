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
    }
}