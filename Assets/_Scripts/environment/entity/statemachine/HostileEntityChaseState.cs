using UnityEngine;

namespace roguelike.environment.entity.statemachine {
    public class HostileEntityChaseState : HostileEntityBaseState {
        public HostileEntityChaseState(HostileEntityStateMachine stateMachine) : base(stateMachine, EntityStates.CHASE) { }

        private float _forgetTicker;

        public override void EnterState() {
            _forgetTicker = 0;
        }

        public override void UpdateState() {
            Vector3 targetPos;

            if(!stateMachine.hasLineOfSight) {
                targetPos = stateMachine.lastSeenLocation;
                _forgetTicker += 1f * Time.deltaTime;
            } else {
                targetPos = stateMachine.targetPosition;
                _forgetTicker = 0;
            }
            stateMachine.entityController.SimpleMove(stateMachine.GetCurrentMovementSpeed(targetPos));
        }

        public override void ExitState() {
            stateMachine.LoseAgro();
        }

        public override EntityStates GetNextState() {
            if(stateMachine.ForgetTimer < _forgetTicker && !stateMachine.DoesFollowToCorner ||
               Vector3.Distance(stateMachine.hostileEntity.Position, stateMachine.lastSeenLocation) < 0.5f) {
                if(stateMachine.LooksForPlayer) {
                    return EntityStates.SEARCH;
                } else {
                    if(!stateMachine.DoesPermaAgro) {
                        return EntityStates.IDLE;
                    }
                }
            }
            return EntityStates.CHASE;
        }
    }
}