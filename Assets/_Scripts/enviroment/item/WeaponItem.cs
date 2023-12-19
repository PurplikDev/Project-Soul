using roguelike.enviroment.entity;
using roguelike.enviroment.entity.statsystem;
using UnityEngine;

namespace roguelike.core.item {
    public class WeaponItem : HandheldItem {

        public int WeaponTier { get; private set; }
        public float Damage { get; private set; }

        public WeaponItem(string id, float damage, EquipmentType type, bool isTwoHanded, int weaponTier, params StatModifier[] modifiers)
            : base(id, type, isTwoHanded, modifiers) {
            Damage = damage;
            WeaponTier = weaponTier;
        }
    }
}
