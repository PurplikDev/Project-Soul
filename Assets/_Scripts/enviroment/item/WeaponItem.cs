using System.Collections;
using System.Collections.Generic;
using roguelike.enviroment.entity.StatSystem;
using UnityEngine;

namespace roguelike.core.item {
    public class WeaponItem : EquipmentItem {

        public WeaponItem(string id, params StatModifier[] modifiers) : base(id, EquipmentType.MAIN_HAND, modifiers) {
        }
    }
}
