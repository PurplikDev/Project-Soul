using UnityEngine;

namespace roguelike.environment.entity {
    public class EntityAudioManager : MonoBehaviour {

        public AudioSource HurtSource;
        public AudioSource HealSource;
        public AudioSource AttackSource;
        public AudioSource MovementSource;

        public AudioClip HurtSound { get {
                var sound = Resources.Load<AudioClip>($"audio/sfx/{entity.EntityName}/hurt_sound");
                return sound != null ? sound : Resources.Load<AudioClip>($"audio/sfx/missing_sound");
            } }
        public AudioClip HealSound { get {
                var sound = Resources.Load<AudioClip>($"audio/sfx/{entity.EntityName}/heal_sound");
                return sound != null ? sound : Resources.Load<AudioClip>($"audio/sfx/missing_sound");
            } }
        public AudioClip AttackSound { get {
                var sound = Resources.Load<AudioClip>($"audio/sfx/{entity.EntityName}/heal_sound");
                return sound != null ? sound : Resources.Load<AudioClip>($"audio/sfx/missing_sound");
            } }
        public AudioClip MovementSound { get {
                var sound = Resources.Load<AudioClip>($"audio/sfx/{entity.EntityName}/movement_sound");
                return sound != null ? sound : Resources.Load<AudioClip>($"audio/sfx/missing_sound");
            } }

        internal Entity entity;

        private void Awake() {
            entity = transform.parent.GetComponent<Entity>();

            entity.HurtEvent += HurtSource.Play;
            entity.HealEvent += HealSource.Play;
            entity.MovementStartEvent += MovementSource.Play;
            entity.MovementStopEvent += MovementSource.Stop;
            entity.AttackEvent += AttackSource.Play;

            HurtSource.clip = HurtSound;
            HealSource.clip = HealSound;
            AttackSource.clip = AttackSound;
            MovementSource.clip = MovementSound;
        }
    }
}