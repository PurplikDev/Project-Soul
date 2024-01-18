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
        internal bool hasLineOfSight;
        internal Player targetCache { get; private set; }
        internal Vector3 targetPosition { get; private set; }
        internal Vector3 targetDirection { get; private set; }
        internal Vector3 lastSeenLocation { get; private set; }
        internal Vector3 originalEntityPosition { get; private set; }

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
        public int ForgetTimer;

        public void Awake() {
            hostileEntity = GetComponent<HostileEntity>();
            animator = GetComponent<Animator>();
            entityController = GetComponent<CharacterController>();

            states.Add(EntityStates.IDLE, new HostileEntityIdleState(this));
            states.Add(EntityStates.CHASE, new HostileEntityChaseState(this));
            states.Add(EntityStates.SEARCH, new HostileEntitySearchState(this));
            //states.Add(EntityStates.ATTACK, new PlayerAttackState(this));

            currentState = states[EntityStates.IDLE];
        }

        protected override void Start() {
            base.Start();
            // why invoke repeating? no idea, just didn't want the check to happen every tick/frame
            InvokeRepeating(nameof(CheckForTarget), 0f, 0.25f);
        }

        private void CheckForTarget() {
            // no need to recheck if it's perma agro and the entity is already mad
            if(DoesPermaAgro && targetCache != null && !targetCache.IsDead) { return; }

            var target = CheckRange();
            if(target == null) { return; }
            SetTarget(target);

            // the difference between these two is that FOV is used to check if the player is infront
            // of the creature, while line of sight prevents the entity from tracking players
            // that are behind a wall without being seen first
            hasLineOfSight = CheckLineOfSight();
            canSeePlayer = CheckFieldOfVision();

            if(hasLineOfSight) {
                lastSeenLocation = target.Position;
            }

            if(canSeePlayer || (!NeedToSeePlayer && isPlayerInRange)) {
                isTargetting = true;
            }

            if(isTargetting) {
                SetLookRotation(lastSeenLocation);
            }
        }

        private Player CheckRange() {

            Collider[] colliders = Physics.OverlapSphere(hostileEntity.Position, Range, LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore);

            if(colliders.Length == 0) {
                isPlayerInRange = false;
                return null;
            }

            foreach(var collider in colliders) {
                var player = collider.GetComponent<Player>();
                if(player == null) continue;
                return player;
            }

            return null;
        }


        private bool CheckFieldOfVision() {
            var targetAngle = Vector3.Angle(hostileEntity.LookDirection, targetDirection);
            if(targetAngle < Angle / 2 && Physics.Raycast(hostileEntity.Position, targetDirection, out var hitInfo, Range) && hitInfo.transform.GetComponent<Player>() != null) {
                return true;
            } else {
                return false;
            }
        }

        private bool CheckLineOfSight() {
            if(Physics.Raycast(hostileEntity.Position, targetDirection, out var hitInfo, Range)) {
                return hitInfo.transform.GetComponent<Player>() != null;
            }

            return false;
        }

        private void SetTarget(Player target) {
            targetCache = target;
            targetPosition = target.Position;
            targetDirection = GetDirection(target.Position);
        }

        internal Vector3 GetDirection(Vector3 targetPosition) {
            // to this day i still have no idea why it needs to be multiplied by -1
            return (hostileEntity.Position - targetPosition).normalized * -1;
        }

        /// <param name="targetPosition">Position to target at.</param>
        /// <returns>Vector directed at provided position with entity's speed stat applied.</returns>
        public Vector3 GetCurrentMovementSpeed(Vector3 targetPosition) {
            Vector3 direction = GetDirection(targetPosition);
            return new Vector3(direction.x, 0, direction.z) * hostileEntity.Speed.Value;
        }

        internal void SetLookRotation(Vector3 targetPosition) {
            Vector3 direction = GetDirection(targetPosition);
            hostileEntity.LookDirection = new Vector3(direction.x, 0, direction.z);
        }
    }

    public enum EntityStates {
        IDLE,
        CHASE,
        SEARCH,
        ATTACK
    }
}