using System.Collections;
using roguelike.core.item;
using UnityEngine;

namespace roguelike.environment.entity.player.statemachine {
    public class PlayerAttackState : PlayerBaseState {
        private bool _isAttacking = false;

        public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine, PlayerStates.WALK) { }

        public override void EnterState() {
            _isAttacking = true;
            playerStateMachine.animator.SetTrigger("AttackTrigger");
        }

        public override void UpdateState() { }

        public override void ExitState() { }

        public override PlayerStates GetNextState() {
            if(_isAttacking) {
                return PlayerStates.ATTACK;
            }
            return PlayerStates.IDLE;
        }

        public IEnumerator PlayerAttack(WeaponItem item) {
            yield return new WaitForSeconds(item.SwingSpeed);
            playerStateMachine.player.PrimaryAction();
            yield return new WaitForSeconds(item.AttackCooldown);
            _isAttacking = false;
        }
    }
}
