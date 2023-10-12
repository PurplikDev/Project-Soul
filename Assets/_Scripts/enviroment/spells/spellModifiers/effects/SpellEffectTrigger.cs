using UnityEngine;

namespace roguelike.enviroment.spellcasting {
    public class SpellEffectTrigger : SpellEffect {
        private Spell _spellToTrigger;

        public SpellEffectTrigger(Spell spell) {
            _spellToTrigger = spell;
        }

        public override void CastEffect(Vector3 castPosition, Quaternion castRotation) {
            _spellToTrigger.cast(castPosition, castRotation);
        }
    }
}