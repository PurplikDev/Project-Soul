namespace roguelike.environment.entity.statuseffect {
    public class StatusEffect {
        // will do this later :]
        float value;
        EffectType type;
        public int Duration;

        public StatusEffect(float value, EffectType type, int duration) {
            this.value = value;
            this.type = type;
            Duration = duration;
        }

        public void Apply(Entity entity) {
            switch (type) {
                case EffectType.HEALTH:
                    entity.Heal(value);
                    Duration--;
                    break;
            }
        }
    }

    public enum EffectType {
        HEALTH
    }
}
