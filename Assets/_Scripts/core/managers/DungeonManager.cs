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

        public Room[,] dungeon; // [y,x] WHYYYY?!?!? why isn't it [x,y]?????
        public List<Room> unvisitedRooms;

        private DungeonLength length = DungeonLength.LONG;

        protected override void Awake() {
            for(int i = 0; i < 1; i++) {
                do {
                    GenerateMain();
                } while(dungeon == null);
                LogDungeon();
            }

            base.Awake();
        }

        public void GenerateMain() {
            int size = (int)length;
            int limit = (int)Math.Floor(size / 1.5f);

            Room starterRoom;
            Room finalRoom;

            dungeon = new Room[size, size];
            unvisitedRooms = new List<Room>();

            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    if(Random.Range(0, 100) < 10) {
                        dungeon[i, j] = new Room(RoomType.OBSTACLE, i ,j);
                    } else {
                        dungeon[i, j] = new Room(RoomType.EMPTY, i ,j);
                    }
                }
            }

            starterRoom = GenerateRoom(Random.Range(3, size - 3), Random.Range(2, size - 2), RoomType.STARTER);
            finalRoom = GenerateRoom(Random.Range(1, size - 1), Random.Range(2, size - 2), RoomType.FINAL);

            List<Room> rooms = new List<Room>();
            rooms.Add(starterRoom);

            while(rooms.Count > 0) {
                var currentRoom = rooms.First();
                rooms.Remove(currentRoom);

                if(currentRoom.x == finalRoom.x && currentRoom.y == finalRoom.y) {
                    Debug.Log("Final room reached!");
                    break;
                }

                // todo: logic, don't have enough time for it rn
            }
        }

        public Room GenerateRoom(int y, int x, RoomType type, bool isUnvisited = true) {
            var room = new Room(type, y, x);
            dungeon[y, x] = room;
            if(isUnvisited) {
                unvisitedRooms.Add(room);
            }
            return room;
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

            for (int i = 0; i < dungeon.GetLength(0); i++) {
                for (int j = 0; j < dungeon.GetLength(1); j++) {
                    builder.Append("[" + dungeon[i, j].ToString() + "]");
                }
                builder.AppendLine();
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

            public int value, y, x;

            public Room(RoomType type, int y, int x) {
                Type = type;
                value = 0;
                this.y = y;
                this.x = x;
            }

            public override string ToString() {
                return ((int)Type).ToString();
            }

            public enum RoomType {
                OBSTACLE = -1,
                EMPTY = 0,
                STARTER = 1,
                NORMAL = 2,
                FINAL = 3,
                SPECIAL = 4
            }
        }
    }
}

