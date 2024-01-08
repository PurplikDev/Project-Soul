using System.Collections;
using roguelike.core.item;
using UnityEngine;
using static roguelike.environment.entity.player.statemachine.PlayerStateMachine;

namespace roguelike.environment.entity.player.statemachine {
    public class PlayerAttackState : PlayerBaseState {
        private bool _isAttacking = false;

        public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine, PlayerStates.WALK) { }

        public override void EnterState() {
            _isAttacking = true;
            playerStateMachine.Animator.SetTrigger("AttackTrigger");
        }

        public override void UpdateState() { }

        public override void ExitState() { }

        public override PlayerStates GetNextState() {
            if(_isAttacking) {
                return PlayerStates.ATTACK;
            }
            return base.GetNextState();
        }

        public IEnumerator PlayerAttack(WeaponItem item) {
            yield return new WaitForSeconds(item.SwingSpeed);
            // do attack
            yield return new WaitForSeconds(item.AttackCooldown);
            _isAttacking = false;
        }
    }
}
