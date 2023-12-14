using roguelike.enviroment.entity;
using roguelike.enviroment.entity.statsystem;
using UnityEngine;

namespace roguelike.core.item {
    public class WeaponItem : EquipmentItem {

        public int WeaponTier { get; private set; }
        public bool IsTwoHanded { get; private set; }
        public float Damage { get; private set; }

        public WeaponItem(string id, float damage, EquipmentType type, bool isTwoHanded, int weaponTier, params StatModifier[] modifiers) : base(id, type, modifiers) {
            Damage = damage;
            IsTwoHanded = isTwoHanded;
            WeaponTier = weaponTier;
        }

        public virtual void WeaponAction(Entity entityAttacker) {
            Debug.Log(entityAttacker.name + " is using " + Name);
        }
    }
}
