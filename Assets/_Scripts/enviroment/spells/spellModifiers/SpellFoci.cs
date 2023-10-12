namespace roguelike.enviroment.spellcasting {
    public class SpellFoci {
        private Spell _storedSpell;
        public Spell GetSpell { get { return _storedSpell; } }

        public SpellFoci(params ISpellModifier[] spellModifiers) {
            
        }
    }
}
