namespace io.purplik.ProjectSoul.Entity.Stats
{

    public enum StatType
    {
        FLAT = 100,
        PERCENTAGEADD = 200,
        PERCENTAGEMULT = 300
        
    }

    public class StatModifier
    {
        public readonly float value;
        public readonly StatType type;
        public readonly int order;
        public readonly object Source;

        public StatModifier(float value, StatType type, int order, object source)
        {
            this.value = value;
            this.type = type;
            this.order = order;
            Source = source;
        }

        public StatModifier(float value, StatType type) : this(value, type, (int)type, null) { }
        public StatModifier(float value, StatType type, int order) : this(value, type, order, null) { }
        public StatModifier(float value, StatType type, object source) : this(value, type, (int)type, source) { }
    }
}
