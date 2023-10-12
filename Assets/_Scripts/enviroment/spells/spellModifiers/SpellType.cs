using UnityEngine;

namespace roguelike.enviroment.spellcasting {
    public abstract class SpellType : ISpellModifier {
        public abstract void cast(Vector3 castPosition, Quaternion castRotation);
    }
}