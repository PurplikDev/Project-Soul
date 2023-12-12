public static class CombatUtils {

    // TIERS 0, 1, 2, 3

    //  ATTACK
    // D
    // E
    // F
    // E
    // N

    // percentage from attack entity's max health
    private static readonly float[,] _flatDamageReduction = {
        { 0f, 0f, 0f, 0f },
        {10f, 0f, 0f, 0f },
        {30f,15f, 0f, 0f },
        {45f,20f,10f, 0f }
    };

    // percentage from attack's damage
    private static readonly float[,] _relativeDamageReduction = {
        { 0f, 0f, 0f, 0f },
        {20f,10f, 5f, 5f },
        {40f,30f,15f, 5f },
        {60f,40f,30f,20f }
    };

    public static float GetDamageReduction(int attackTier, int defenceTier) {
        if(defenceTier > attackTier) {
            return _flatDamageReduction[defenceTier, attackTier];
        }
        return _relativeDamageReduction[defenceTier, attackTier];
    }
}
