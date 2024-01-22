using UnityEngine;

namespace roguelike.environment.entity.statemachine {
    public class HostileEntitySearchState : HostileEntityBaseState {
        public HostileEntitySearchState(HostileEntityStateMachine stateMachine) : base(stateMachine, EntityStates.SEARCH) { }

        int searchingLoops;
        bool isDoneSearching;

        public override void EnterState() {
            
            searchingLoops = 0;
        }

        public override void UpdateState() {

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


        private void ResetSearch() {
            if(searchingLoops < stateMachine.MinimumSearchingIteractions || stateMachine.AdditionalSearchChance < Random.Range(0, 100) {
                searchingLoops++;
            } else {
                isDoneSearching = true;
            }
        }
    }
}