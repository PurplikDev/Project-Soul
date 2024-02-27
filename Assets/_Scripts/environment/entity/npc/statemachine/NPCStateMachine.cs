using roguelike.core.statemachine;
using roguelike.environment.entity.npc.statemachine;
using UnityEngine;

namespace roguelike.environment.entity.npc.statemachine {
    public class NPCStateMachine : StateManager<NPCState> {
        internal NPC npc { get; private set; }
        internal Animator animator { get; private set; }
        internal CharacterController entityController { get; private set; }

        internal float distanceToTarget;
        internal Vector3 targetPosition { get; private set; }
        internal Vector3 targetDirection { get; private set; }
        internal Vector3 originalEntityPosition { get; private set; }

        [Header("Behavior Options")]
        /// <summary> Does the NPC have a position to target towards to? 
        /// (Like a trader when the player comes close to it's market stand)</summary>
        public bool DoesGoToPosition;

        public void Awake() {
            npc = GetComponent<NPC>();
            animator = GetComponent<Animator>();
            entityController = GetComponent<CharacterController>();

            SetLookRotation(GetRandomPosition());

            states.Add(NPCState.IDLE, new NPCIdleState(this));

            currentState = states[NPCState.IDLE];
        }

        protected override void Update() {
            base.Update();
        }
        
        internal Vector3 GetDirection(Vector3 targetPosition) {
            // to this day i still have no idea why it needs to be multiplied by -1
            return (npc.Position - targetPosition).normalized * -1;
        }

        /// <param name="targetPosition">Position to target at.</param>
        /// <returns>Vector directed at provided position with entity's speed stat applied.</returns>
        public Vector3 GetCurrentMovementSpeed(Vector3 targetPosition) {
            Vector3 direction = GetDirection(targetPosition);
            return new Vector3(direction.x, 0, direction.z) * npc.Speed.Value;
        }

        internal void SetLookRotation(Vector3 targetPosition) {
            Vector3 direction = GetDirection(targetPosition);
            npc.LookDirection = new Vector3(direction.x, 0, direction.z);
        }

        internal Vector3 GetRandomPosition() {
            return new Vector3(
                Random.Range(npc.Position.x - 15, npc.Position.x + 15),
                npc.Position.y,
                Random.Range(npc.Position.z - 15, npc.Position.z + 15));
        }
    }

    public enum NPCState {
        IDLE,
        WANDER,
        GO_TO_POSITION
    }
}