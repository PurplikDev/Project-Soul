namespace roguelike.core.utils {
    public static class Mathematicus {
        public static int OverflowFromAddition(int baseValue, int amountToAdd, int limit) {
            int value = baseValue + amountToAdd;
            if(value > limit) {
                return value - limit;
            }
            return 0;
        }
    }
}