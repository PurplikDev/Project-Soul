using UnityEngine;

namespace roguelike.core.utils.mathematicus {
    public static class Mathematicus {
        public static int OverflowFromAddition(int baseValue, int amountToAdd, int limit) {
            int value = baseValue + amountToAdd;
            return value > limit ? baseValue - (value - limit) : 0;
        }

        public static bool ChanceIn(float chance, float range = 100f) {
            if(chance == 1 && range == 1) { return true; }
            return chance > Random.Range(0f, range);
        }
    }
}