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
        internal bool isTargetting;
        internal Player targetCache;
        internal Vector3 targetPosition;
        internal Vector3 targetDirection;
        internal Vector3 lastSeenLocation;
        internal Vector3 originalEntityPosition;

        [Header("Behavior Options")]
        /// <summary> Does the Entity not lose the player agro? </summary>
        public bool DoesPermaAgro;
        /// <summary> Does the Entity go to a position it saw the player last at? </summary>
        public bool DoesFollowToCorner;
        /// <summary> Does the Entity try to look for the player when it loses it's agro? </summary>
        public bool LooksForPlayer;
        /// <summary> Does the Entity need to have a line of sight in order to follow the player? </summary>
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
            InvokeRepeating(nameof(CheckForTarget), 0f, 0.25f);
        }

        private void CheckForTarget() {
            if(DoesPermaAgro && targetCache != null && !targetCache.IsDead) { return; } // no need to recheck if it's perma agro and the entity is already mad

            CheckRange();

            if(!NeedToSeePlayer) {

            }

            if(NeedToSeePlayer && isPlayerInRange) {
                CheckFieldOfVision();
            }
        }

        private (Player target, Vector3 targetPosition) CheckRange() {

            Collider[] colliders = Physics.OverlapSphere(hostileEntity.Position, Range, LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore);

            if(colliders.Length == 0) {
                isPlayerInRange = false;
                return (null, new Vector3());
            }

            foreach(var collider in colliders) {
                var player = collider.GetComponent<Player>();
                if(player == null) continue;

                targetPosition = player.Position;
                targetDirection = (hostileEntity.Position - player.Position).normalized;
                isPlayerInRange = true;
                break;
            }
        }


        private void CheckFieldOfVision() {
            var targetAngle = Vector3.Angle(hostileEntity.LookDirection, targetDirection);

            if (targetAngle < Angle && Physics.Raycast(hostileEntity.Position, targetDirection * -1, out var hitInfo, Range) && hitInfo.transform.GetComponent<Player>() != null) {
                canSeePlayer = true;
            } else {
                canSeePlayer = false;
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