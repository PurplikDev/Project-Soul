using roguelike.enviroment.entity;
using roguelike.enviroment.entity.statsystem;
using UnityEngine;

namespace roguelike.core.item {
    public class WeaponItem : EquipmentItem {

        public int WeaponTier { get; private set; }

        public WeaponItem(string id, EquipmentType type, int weaponTier, params StatModifier[] modifiers) : base(id, type, modifiers) {
            WeaponTier = weaponTier;
        }

        public void Attack(Entity entityAttacker) {
            Debug.Log(entityAttacker.name + " is attacking!");
            //Ray ray = new Ray(entityAttacker.Position, );
        }
    }
}
