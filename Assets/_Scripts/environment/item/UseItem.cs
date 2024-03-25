using roguelike.environment.entity;

namespace roguelike.core.item {
    public class UseItem : Item {

        UseItemType type;
        float potency;

        public UseItem(string id, int itemValue, UseItemType type, float potency) : base(id, itemValue) {
            this.type = type;
            this.potency = potency;
        }

        public void Apply(Entity entity) {
            switch(type) {
                case UseItemType.HEALING:
                    entity.Heal(potency); break;
            }
        }
    }

    public enum UseItemType {
        HEALING
    }
}