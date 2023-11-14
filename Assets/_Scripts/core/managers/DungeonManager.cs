using roguelike.core.utils;
using roguelike.system.singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static roguelike.system.manager.DungeonManager.Room;
using Random = UnityEngine.Random;
using static roguelike.core.utils.DirectionUtils;

namespace roguelike.system.manager {
    public class DungeonManager : Singleton<DungeonManager> {

        public Transform platform;
        public Transform cube;

        public Room[,] dungeon; // [y,x] WHYYYY?!?!? why isn't it [x,y]?????
        public List<Room> rooms;
        public List<Room> generatedRooms;
        public List<Room> sideRooms;

        public List<Room> ALLROOMS = new List<Room>();

        private DungeonLength length = DungeonLength.VERYLONG;

        protected override void Awake() {
            for(int i = 0; i < 5; i++) {
                do {
                    GenerateMain();
                } while(dungeon == null);
                LogDungeon();
            }

            base.Awake();
        }

        public void GenerateMain() {
            int size = (int)length;
            int limit = size / 2;

            int failsafe = 0;
            Room possibleRoom = null;

            dungeon = new Room[size, size];
            rooms = new List<Room>();
            generatedRooms = new List<Room>();

            for (int i = 0; i < (int)length; i++) {
                for (int j = 0; j < (int)length; j++) {
                    dungeon[i, j] = new Room(RoomType.EMPTY, i, j);
                }
            }

            GenerateRoom(Random.Range(2, size - 2), Random.Range(2, size - 2), RoomType.STARTER, false);

            while(rooms.Count > 0) {
                var currentRoom = rooms.First();
                rooms.Remove(currentRoom);
                bool reroll = false;

                if(rooms.Count > limit) {
                    if(Random.Range(0, 10) > rooms.Count - limit) {
                        Debug.LogWarning("done xd");
                        break;
                    }
                }
                do {
                    try {
                        possibleRoom = GetRelativeRoom(currentRoom.y, currentRoom.x, (Direction) Random.Range(0, 4));
                    } catch(IndexOutOfRangeException) {
                        possibleRoom = null;
                        reroll = true;
                        failsafe++;
                        if(failsafe > 75) {
                            break;
                        }
                    }
                } while(reroll || possibleRoom.Type != RoomType.EMPTY);

                if(possibleRoom == null) {
                    Debug.LogWarning("possible room was null");
                    break;
                }

                if(possibleRoom.Type == RoomType.EMPTY) {
                    GenerateRoom(possibleRoom.y, possibleRoom.x, RoomType.NORMAL, false);
                }
            }
        }

        public void GenerateRoom(int y, int x, RoomType type, bool isSpecial) {
            var room = new Room(type, y, x);
            dungeon[y, x] = room;
            if(!isSpecial) {
                rooms.Add(room);
            }
            generatedRooms.Add(room);
        }

        public Room GetRelativeRoom(int y, int x, Direction direction) {
            switch(direction) {
                case Direction.UP:
                    return dungeon[y - 1, x];
                case Direction.DOWN:
                    return dungeon[y + 1, x];
                case Direction.LEFT:
                    return dungeon[y, x - 1];
                default: // technically this is Direction.RIGHT, i just wanted the warning to shut up
                    return dungeon[y, x + 1];
            }
        }

        private void LogDungeon() {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < (int)length; i++) {
                builder.Append("|");
                for (int j = 0; j < (int)length; j++) {
                    builder.Append("[" + dungeon[i, j].ToString() + "]");
                }
                builder.AppendLine("|");
            }
            Debug.Log(builder);
        }


        public enum DungeonHazard {

        }

        public enum DungeonLength {
            SHORT = 5,
            MEDIUM = 9,
            LONG = 11,
            VERYLONG = 15
        }

        public class Room {
            public RoomType Type;

            public List<Direction> walls = new List<Direction>();

            public int x, y;

            public Room(RoomType type, int y, int x) {
                Type = type;

                this.y = y;
                this.x = x; 
            }

            public override string ToString() {
                return ((int)Type).ToString();
            }

            public enum RoomType {
                EMPTY = 0,
                STARTER = 1,
                NORMAL = 2,
                FINAL = 3,
                SPECIAL = 4
            }
        }
    }
}

