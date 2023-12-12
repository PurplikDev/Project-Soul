using System.Collections;
using System.Collections.Generic;
using roguelike.enviroment.entity.statsystem;
using UnityEngine;

namespace roguelike.core.item {
    public class WeaponItem : EquipmentItem {

        public int WeaponTier { get; private set; }

        public WeaponItem(string id, EquipmentType type, int weaponTier, params StatModifier[] modifiers) : base(id, type, modifiers) {
            WeaponTier = weaponTier;
        }
    }
}
