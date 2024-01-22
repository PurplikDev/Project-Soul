using UnityEngine;

namespace roguelike.environment.entity.statemachine {
    public class HostileEntitySearchState : HostileEntityBaseState {
        public HostileEntitySearchState(HostileEntityStateMachine stateMachine) : base(stateMachine, EntityStates.SEARCH) { }

        int searchingLoops;
        bool isDoneSearching;
        float searchTimer;
        Vector3 searchPos;

        public override void EnterState() {
            searchingLoops = 0;
            searchTimer = 0;
            isDoneSearching = false;
            searchPos = stateMachine.hostileEntity.Position;
        }

        public override void UpdateState() {
            searchTimer += Time.deltaTime;

            if(searchTimer > 1.5f) {
                searchPos = stateMachine.hostileEntity.Position;
            }

            if(searchTimer > 2.5f) {
                searchingLoops++;
                searchTimer = 0;

                // idea: shoot a raycast here to check in the direction, if there is a wall near try to reroll?

                searchPos = stateMachine.GetRandomPosition();
                stateMachine.SetLookRotation(searchPos);

                if (searchingLoops >= stateMachine.MinimumSearchingIteractions && stateMachine.AdditionalSearchChance > Random.Range(0, 100)) {
                    isDoneSearching = true;
                }
            }

            stateMachine.entityController.SimpleMove(stateMachine.GetCurrentMovementSpeed(searchPos));

            if(stateMachine.isTargetting) {
                isDoneSearching = true;
            }
        }

        public override void ExitState() {
            stateMachine.LoseAgro();
        }

        public override EntityStates GetNextState() {
            if(isDoneSearching) {
                return EntityStates.IDLE;
            }
            return EntityStates.SEARCH;
        }
    }
}