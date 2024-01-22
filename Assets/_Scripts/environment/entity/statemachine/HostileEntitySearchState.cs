using UnityEngine;

namespace roguelike.environment.entity.statemachine {
    public class HostileEntitySearchState : HostileEntityBaseState {
        public HostileEntitySearchState(HostileEntityStateMachine stateMachine) : base(stateMachine, EntityStates.SEARCH) { }

        int searchingLoops;
        bool isDoneSearching;
        float searchTimer;

        public override void EnterState() {
            searchingLoops = 0;
            searchTimer = 0;
            isDoneSearching = false;
        }

        public override void UpdateState() {
            searchTimer += Time.deltaTime;

            var randomPos = new Vector3(0, 1, 0);

            if (searchTimer > 2) {
                searchingLoops++;
                searchTimer = 0;
                randomPos.x += 2.5f;

                if (searchingLoops >= stateMachine.MinimumSearchingIteractions && stateMachine.AdditionalSearchChance > Random.Range(0, 100)) {
                    isDoneSearching = true;
                }
            }

            stateMachine.entityController.SimpleMove(stateMachine.GetCurrentMovementSpeed(randomPos));

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