using UnityEngine;

namespace roguelike.core.utils {
    public static class DirectionUtils {
        public static Direction getOpposite(Direction direction) {
            switch(direction) { // i hate this piece of code, too lazy to think of a proper solution
                case Direction.UP: return Direction.DOWN;
                case Direction.DOWN: return Direction.UP;
                case Direction.LEFT: return Direction.RIGHT;
                case Direction.RIGHT: return Direction.LEFT;
            }
            return Direction.UP;
        }

        public static Direction RandomDirection() {
            return (Direction) Random.Range(0, 4);
        }

        public enum Direction {
            UP = 0,
            DOWN = 1,
            LEFT = 2,
            RIGHT = 3
        }
    }
}