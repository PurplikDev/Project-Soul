using System;

namespace roguelike.core.utils {
    public static class Mathematicus {
        public static int OverflowFromAddition(int baseValue, int amountToAdd, int limit) {
            int value = baseValue + amountToAdd;
            return value > limit ? baseValue - (value - limit) : 0;
        }
    }
}