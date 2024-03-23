using roguelike.environment.entity;
using roguelike.environment.entity.statuseffect;
using UnityEngine;

namespace roguelike.core.item {
    public class UseItem : Item {

        UseItemType type;
        float potency;

        public UseItem(string id, int itemValue, UseItemType type, float potency) : base(id, itemValue) {
            this.type = type;
            this.potency = potency;
        }

        public void Apply(Entity entity) {
            entity.Heal(potency);
        }
    }

    public enum UseItemType {
        HEALING
    }
}