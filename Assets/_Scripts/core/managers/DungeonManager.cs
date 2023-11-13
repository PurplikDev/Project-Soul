using roguelike.system.singleton;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static roguelike.system.manager.DungeonManager.Room;

namespace roguelike.system.manager {
    public class DungeonManager : Singleton<DungeonManager> {

        public Room[,] dungeon;
        public List<Room> rooms;


        protected override void Awake() {
            GenerateDungeon();
            LogDungeon();
            base.Awake();
        }

        public void GenerateDungeon() {
            int size = 9;

            dungeon = new Room[size, size];
            rooms = new List<Room>();

            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    dungeon[i, j] = new Room(RoomType.EMPTY, i, j);
                }
            }

            var starterRoom = new Room(RoomType.SINGLE, 4, 4);
            dungeon[4, 4] = starterRoom;
            rooms.Add(starterRoom);

            InvokeRepeating(nameof(Funny), 1f, 1f);
        }

        private void Funny() {
            if (rooms.Count > 0) {
                Debug.ClearDeveloperConsole();
                LogDungeon();

                var currentRoom = rooms.First();
                rooms.Remove(currentRoom);

                Room newRoom;

                foreach (KeyValuePair<Door, bool> door in currentRoom.doors) {
                    if (door.Value) {
                        switch (door.Key) {
                            case Door.UP:
                                newRoom = new Room(RoomType.SINGLE, currentRoom.x, currentRoom.y + 1);
                                dungeon[currentRoom.x, currentRoom.y + 1] = newRoom;
                                rooms.Add(newRoom);
                                break;

                            case Door.DOWN:
                                newRoom = new Room(RoomType.SINGLE, currentRoom.x, currentRoom.y - 1);
                                dungeon[currentRoom.x, currentRoom.y - 1] = newRoom;
                                rooms.Add(newRoom);
                                break;

                            case Door.LEFT:
                                newRoom = new Room(RoomType.SINGLE, currentRoom.x - 1, currentRoom.y);
                                dungeon[currentRoom.x - 1, currentRoom.y] = newRoom;
                                rooms.Add(newRoom);
                                break;

                            case Door.RIGHT:
                                newRoom = new Room(RoomType.SINGLE, currentRoom.x + 1, currentRoom.y);
                                dungeon[currentRoom.x + 1, currentRoom.y] = newRoom;
                                rooms.Add(newRoom);
                                break;

                        }
                    }
                }
            }
        }

        private void LogDungeon() {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < 9; i++) {
                builder.Append("|");
                for (int j = 0; j < 9; j++) {
                    builder.Append("[" + dungeon[i, j].ToString() + "]");
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
            public bool Closed;
            public RoomType Type;
            public Dictionary<Door, bool> doors = new Dictionary<Door, bool>(4);

            public int x, y;

            public Room(RoomType type, int x, int y) {
                Type = type;

                this.x = x; 
                this.y = y;

                for(int i = 0; i < 4; i++) {
                    doors.Add((Door)i, RandomDoor());
                }
            }

            public override string ToString() {
                return ((int)Type).ToString();
            }

            private bool RandomDoor() {
                if((Random.Range(0, 4) % 2f) == 0) {
                    return true;
                }
                return false;
            }

            public enum RoomType {
                EMPTY = 0,
                SINGLE = 1, // single element in the array
                DOUBLE = 2, // takes up 2 elements
                TRIPPLE = 3 // makes a turn and takes up 3 elements
            }

            public enum Door {
                UP = 0, 
                DOWN = 1, 
                LEFT = 2, 
                RIGHT = 3
            }
        }
    }
}

