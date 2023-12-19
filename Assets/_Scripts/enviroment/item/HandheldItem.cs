using roguelike.enviroment.entity;
using roguelike.enviroment.entity.statsystem;
using UnityEngine;

namespace roguelike.core.item {
    public class HandheldItem : EquipmentItem {
        public bool IsTwoHanded { get; private set; }

        public HandheldItem(string id, EquipmentType type, bool isTwoHanded ,params StatModifier[] modifiers)
            : base(id, type, modifiers) {
            IsTwoHanded = isTwoHanded;
        }

        public virtual void ItemAction(Entity entityAttacker) {
            Debug.Log(entityAttacker.name + " is using " + Name);
        }
    }
}