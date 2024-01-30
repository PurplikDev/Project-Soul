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
            _isAttacking = true;
            playerStateMachine.animator.SetTrigger("AttackTrigger");
            playerStateMachine.player.Speed.AddModifier(_attackSlowdown);
        }

        public override void UpdateState() {}

        public override void ExitState() {
            playerStateMachine.player.Speed.RemoveModifier(_attackSlowdown);
        }

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
