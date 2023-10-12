using UnityEngine;

namespace roguelike.enviroment.spellcasting {
    public abstract class SpellEffect : ISpellModifier {
        public abstract void CastEffect(Vector3 castPosition, Quaternion castRotation);
    }
}