using System.Collections;
using UnityEngine;

namespace roguelike.environment.entity.statemachine {
    public class HostileEntityAttackState : HostileEntityBaseState {
        public HostileEntityAttackState(HostileEntityStateMachine stateMachine) : base(stateMachine, EntityStates.ATTACK) {}

        public override void EnterState() {
            stateMachine.isAttacking = true;
            stateMachine.hostileEntity.AttackEvent.Invoke();
        }

        public override void UpdateState() { }

        public override void ExitState() {
            stateMachine.SetLookRotation(stateMachine.targetPosition);
        }

        public override EntityStates GetNextState() {
            if(stateMachine.isAttacking) {
                return EntityStates.ATTACK;
            }
            return EntityStates.CHASE;
        }

        public IEnumerator EntityAttack() {
            yield return new WaitForSeconds(stateMachine.hostileEntity.AttackSpeed.Value);
            stateMachine.hostileEntity.Attack();
            yield return new WaitForSeconds(stateMachine.hostileEntity.AttackCooldown.Value);
            stateMachine.isAttacking = false;
        }
    }
}