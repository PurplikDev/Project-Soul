using System.Collections.Generic;
using System.Linq;
using roguelike.enviroment.entity;
using UnityEngine;

namespace roguelike.enviroment.spellcasting {
    public class Spell
    {
        private List<ISpellModifier> _modifiers;
        private int _currectIndex;

        public Spell(params ISpellModifier[] modifiers) {
            _modifiers = modifiers.ToList();
        }

        public void cast(Vector3 castPosition, Quaternion castRotation) {
            var currentModifier = _modifiers[_currectIndex];

            if(!(currentModifier is SpellAugment)) {
                ((SpellType)currentModifier).cast(castPosition, castRotation);
            }
        }

        public void cast(Entity caster) {
            cast(caster.Position, caster.Rotation);
        }
    }
}