using roguelike.system.singleton;
using System.Text;
using UnityEngine;

namespace roguelike.system.manager {
    public class DungeonManager : Singleton<DungeonManager> {

        public Room[,] dungeon;


        protected override void Awake() {
            GenerateDungeon();

            base.Awake();
        }

        public void GenerateDungeon() {
            int size = 9;

            dungeon = new Room[size, size];

            for(int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    dungeon[i, j] = new Room(Room.RoomType.EMPTY);
                }
            }

            dungeon[5, 5] = new Room(Room.RoomType.SINGLE);

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < 9; i++) {
                builder.Append("|");
                for (int j = 0; j < 9; j++) {
                    builder.Append("[" + dungeon[i,j].ToString() + "]");
                }
                builder.AppendLine("|");
            }



            Debug.Log(builder);
        }

        public enum DungeonHazard {

        }

        public enum DungeonLength {
            SHORT,
            MEDIUM,
            LONG,
            VERYLONG
        }

        public class Room {
            public enum RoomType {
                EMPTY = 0,
                SINGLE = 1, // single element in the array
                DOUBLE = 2, // takes up 2 elements
                TRIPPLE = 3 // makes a turn and takes up 3 elements
            }

            public bool Closed;
            public RoomType Type;

            public Room(RoomType type) {
                Type = type;
            }

            public override string ToString() {
                return ((int)Type).ToString();
            }
        }
    }
}

