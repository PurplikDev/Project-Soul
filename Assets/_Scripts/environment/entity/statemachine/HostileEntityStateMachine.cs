using roguelike.core.statemachine;
using roguelike.environment.entity.player;
using UnityEngine;

namespace roguelike.environment.entity.statemachine {
    public class HostileEntityStateMachine : StateManager<EntityStates> {

        internal HostileEntity hostileEntity { get; private set; }
        internal Animator animator { get; private set; }
        internal CharacterController entityController { get; private set; }
        internal bool isPlayerInRange;
        internal bool canSeePlayer;
        internal Player targetCache;
        internal Vector3 targetPosition;
        internal Vector3 targetDirection;
        internal Vector3 lastSeenLocation;
        internal Vector3 originalEntityPosition;

        [Header("Behavior Options")]
        public bool DoesPermaAgro;
        public bool DoesFollowToCorner;
        public bool LooksForPlayer;
        public bool NeedToSeePlayer;
        [Space]
        [Header("Behavior Properties")]
        public float Range;
        [Range(0, 360)]
        public float Angle;
        public float ForgetTimer;

        public Vector3 GetCurrentMovementSpeed { get { return new Vector3(targetDirection.x, 0, targetDirection.z) * hostileEntity.Speed.Value * -1; } }

        public void Awake() {
            hostileEntity = GetComponent<HostileEntity>();
            animator = GetComponent<Animator>();
            entityController = GetComponent<CharacterController>();

            states.Add(EntityStates.IDLE, new HostileEntityIdleState(this));
            states.Add(EntityStates.CHASE, new HostileEntityChaseState(this));
            //states.Add(EntityStates.SEARCH, new PlayerRunState(this));
            //states.Add(EntityStates.ATTACK, new PlayerAttackState(this));

            currentState = states[EntityStates.IDLE];
        }

        protected override void Start() {
            base.Start();
            InvokeRepeating(nameof(CheckFieldOfVision), 0f, 0.25f);
        }

        private void CheckFieldOfVision() {
            if(DoesPermaAgro && targetCache != null && !targetCache.IsDead) { return; } // no need to recheck if it's perma agro and the entity is already mad

            Collider[] colliders = Physics.OverlapSphere(hostileEntity.Position, Range, LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore);

            if(colliders.Length == 0) {
                isPlayerInRange = false;
                return;
            }

            foreach(var collider in colliders) {
                var player = collider.GetComponent<Player>();
                if(player == null) continue;

                targetPosition = player.Position;
                targetDirection = (hostileEntity.Position - player.Position).normalized;

                isPlayerInRange = true;

                var targetAngle = Vector3.Angle(hostileEntity.LookDirection, targetDirection);

                if (targetAngle < Angle) {
                    if (Physics.Raycast(hostileEntity.Position, targetDirection * -1, out var hitInfo, Range)) {
                        if(hitInfo.transform.GetComponent<Player>() != null) {
                            canSeePlayer = true;
                        } else {
                            canSeePlayer = false;
                        }
                    } else {
                        canSeePlayer = false;
                    }
                } else {
                    canSeePlayer = false;
                }

                

                if(NeedToSeePlayer && canSeePlayer || !NeedToSeePlayer && isPlayerInRange) {
                    targetCache = player;
                    lastSeenLocation = targetPosition;
                    hostileEntity.LookDirection = targetDirection;
                }
            }
        }
    }

    public enum EntityStates {
        IDLE,
        CHASE,
        SEARCH,
        ATTACK
    }
}