using roguelike.system.singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static roguelike.system.manager.DungeonManager.Room;
using Random = UnityEngine.Random;

namespace roguelike.system.manager {
    public class DungeonManager : Singleton<DungeonManager> {

        public Room[,] dungeon;
        public List<Room> rooms;
        public List<Room> generatedRooms;


        protected override void Awake() {
            GenerateMain();
            LogDungeon();
            base.Awake();
        }

        public void GenerateMain() {
            int size = 9;
            int limit = 0;

            dungeon = new Room[size, size];
            rooms = new List<Room>();
            generatedRooms = new List<Room>();

            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    dungeon[i, j] = new Room(RoomType.EMPTY, i, j);
                }
            }

            var starterRoom = new Room(RoomType.STARTER, 4, 4);
            dungeon[4, 4] = starterRoom;
            rooms.Add(starterRoom);

            while(rooms.Count > 0) {
                var currentRoom = rooms.First();
                rooms.Remove(currentRoom);

                Room neighbor = null;
                bool reroll = false;
                do {
                    reroll = false;
                    try {
                        switch ((Direction)Random.Range(0, 3)) {
                            case Direction.UP:
                                neighbor = dungeon[currentRoom.x, currentRoom.y + 1];
                                break;

                            case Direction.DOWN:
                                neighbor = dungeon[currentRoom.x, currentRoom.y - 1];
                                break;

                            case Direction.LEFT:
                                neighbor = dungeon[currentRoom.x - 1, currentRoom.y];
                                break;

                            case Direction.RIGHT:
                                neighbor = dungeon[currentRoom.x + 1, currentRoom.y];
                                break;
                        }
                    } catch(IndexOutOfRangeException) {
                        reroll = true;
                        continue; // ?\_(?)_/?
                    }
                } while (reroll || neighbor.Type != 0);

                if (limit < 9) {
                    var newRoom = new Room(RoomType.NORMAL, neighbor.x, neighbor.y);
                    dungeon[neighbor.x, neighbor.y] = newRoom;
                    rooms.Add(newRoom);
                    generatedRooms.Add(newRoom);
                    limit++;
                } else {
                    dungeon[neighbor.x, neighbor.y] = new Room(RoomType.FINAL, neighbor.x, neighbor.y);
                }
            }
        }

        public void GenerateSide() {
            // todo: add generation of side rooms
            // don't forget about doors maggot
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
            public RoomType Type;

            public Direction[] doors;

            public int x, y;

            public Room(RoomType type, int x, int y) {
                Type = type;

                this.x = x; 
                this.y = y;
            }

            public override string ToString() {
                return ((int)Type).ToString();
            }

            public enum RoomType {
                EMPTY = 0,
                STARTER = 1,
                NORMAL = 2,
                FINAL = 3
            }

            public enum Direction {
                UP = 0,
                DOWN = 1,
                LEFT = 2,
                RIGHT = 3
            }
        }
    }
}

