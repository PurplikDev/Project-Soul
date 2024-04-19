using System.Collections;
using roguelike.core.item;
using roguelike.environment.entity.statsystem;
using UnityEngine;

namespace roguelike.environment.entity.player.statemachine {
    public class PlayerAttackState : PlayerBaseState {
        private bool _isAttacking = false;
        private StatModifier _attackSlowdown = new StatModifier(-0.5f, StatModifierType.FLAT, StatType.SPEED);

        public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine, PlayerStates.WALK) { }

        public override void EnterState() {
            if(_isAttacking) { return; }
            _isAttacking = true;
            playerStateMachine.animator.SetBool("IsAttacking", true);
        }

        public override void UpdateState() {
            playerStateMachine.CharacterController.SimpleMove(playerStateMachine.GetCurrentMovementSpeed);
        }

        public override void ExitState() {
            playerStateMachine.player.Speed.RemoveModifier(_attackSlowdown);
            playerStateMachine.animator.SetBool("IsAttacking", false);
        }

        public override PlayerStates GetNextState() {
            if(_isAttacking) {
                return PlayerStates.ATTACK;
            }
            return PlayerStates.IDLE;
        }

        public IEnumerator PlayerAttack(WeaponItem item) {
            playerStateMachine.animator.SetTrigger("AttackStartTrigger");
            playerStateMachine.animator.SetFloat("AttackStartModifier", ((1 / item.SwingSpeed) + (1 % item.SwingSpeed)));
            yield return new WaitForSeconds(item.SwingSpeed);
            playerStateMachine.player.PrimaryAction();
            playerStateMachine.player.AttackEvent.Invoke();
            playerStateMachine.animator.SetTrigger("AttackEndTrigger");
            playerStateMachine.animator.SetFloat("AttackEndModifier", ((1 / item.AttackCooldown) + (1 % item.AttackCooldown)));
            yield return new WaitForSeconds(item.AttackCooldown);
            _isAttacking = false;
        }
    }
}
