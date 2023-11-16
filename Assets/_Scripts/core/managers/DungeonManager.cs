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
        public Transform otherPlatform;

        public Room[,] dungeon; // [y,x] WHYYYY?!?!? why isn't it [x,y]?????

        List<Room> path;
        List<Room> visitedRooms;
        List<Room> rooms;
        List<Room> finalRooms;

        private DungeonLength length = DungeonLength.LONG;

        protected override void Awake() {
            do {
                GenerateMain();
            } while(dungeon == null);
            GenerateSide();
            SpawnDungeon();
            base.Awake();
        }



        // Main methods

        /// <summary>
        /// Method for generating the main hallway of the dungeon.
        /// </summary>
        public void GenerateMain() {

            rooms = new List<Room>();
            visitedRooms = new List<Room>();
            path = new List<Room>();

            Room starterRoom;
            Room finalRoom;
            Room currentRoom;

            int size = (int)length;
            int center = (int)Math.Floor(size / 2f) + 1;
            int failsafe = 0;

            bool pathFound = false;

            dungeon = new Room[size, size];

            for(int i = 0; i < size; i++) {
                for(int j = 0; j < size; j++) {
                    if(Random.Range(0, 100) < 15) {
                        dungeon[i, j] = new Room(RoomType.OBSTACLE, i, j);
                    } else {
                        dungeon[i, j] = new Room(RoomType.EMPTY, i, j);
                    }
                }
            }

            starterRoom = GenerateRoom(Random.Range(1, center), Random.Range(1, center - 1), RoomType.STARTER, true);
            finalRoom = GenerateRoom(Random.Range(center, size - 1), Random.Range(center, size - 1), RoomType.FINAL);           

            rooms.Add(starterRoom);
            path.Add(starterRoom);
            path.Add(finalRoom);            

            while(rooms.Count > 0) {
                currentRoom = rooms.First();
                rooms.Remove(currentRoom);

                if(currentRoom.x == finalRoom.x && currentRoom.y == finalRoom.y) {
                    break;
                }
                try {
                    for(int i = 0; i < 4; i++) {
                        Room tempRoom = GetRelativeRoom(currentRoom.y, currentRoom.x, (Direction)i);
                        if(!tempRoom.visited && tempRoom.Type != RoomType.OBSTACLE) {
                            tempRoom.visited = true;
                            rooms.Add(tempRoom);
                            visitedRooms.Add(tempRoom);
                            tempRoom.value = currentRoom.value + 1;
                            failsafe = 0;
                        }
                    }
                } catch(IndexOutOfRangeException) {
                    continue;
                }
                failsafe++;
                if(failsafe > 100) {
                    return;
                }
            }

            currentRoom = finalRoom;

            while(!pathFound) {
                for(int i = 0; i < 4; i++) {
                    var tempRoom = GetRelativeRoom(currentRoom.y, currentRoom.x, (Direction)i);

                    if(tempRoom.x == starterRoom.x && tempRoom.y == starterRoom.y) {
                        pathFound = true;
                        break;
                    }

                    if(tempRoom.visited && tempRoom.value < currentRoom.value) {
                        currentRoom = tempRoom;
                        path.Add(tempRoom);
                        break;
                    }
                }
            }

            foreach(Room room in path) {
                if(room.Type == RoomType.EMPTY) {
                    dungeon[room.y, room.x] = new Room(RoomType.NORMAL, room.y, room.x);
                }
            }
        }

        /// <summary>
        /// Method for generating side branches of the main hallway.
        /// </summary>
        public void GenerateSide() {
            finalRooms = new List<Room>();

            foreach(Room room in path) {
                int neighbors = 0;

                List<Direction> directionCache = new List<Direction>();

                for(int i = 0; i < 4; i++) {
                    var relativeRoom = GetRelativeRoom(room.y, room.x, (Direction) i);
                    if(!(relativeRoom.Type == RoomType.EMPTY || relativeRoom.Type == RoomType.OBSTACLE)) {
                        neighbors++;
                        directionCache.Add((Direction)i);
                    }
                }
                
                if(neighbors == 2) {
                    if(Random.Range(0, 4) == (int) directionCache.First()) {
                        var relativeRoom = GetRelativeRoom(room.y, room.x, directionCache.First());
                        var newRoom = GenerateRoom(relativeRoom.y, relativeRoom.x, RoomType.NORMAL);
                        finalRooms.Add(newRoom);
                    }
                }
            }
        }

        /// <summary>
        /// Method for spawning tiles of the dungeon int the scene.
        /// </summary>
        public void SpawnDungeon() {
            foreach(Room room in finalRooms) {
                Instantiate(platform, new Vector3(room.y * 10, 0, room.x * 10), new Quaternion(0, 0, 0, 0));
            }
        }



        // Util methods

        public Room GenerateRoom(int y, int x, RoomType type, bool visited = false) {
            var room = new Room(type, y, x);
            room.visited = visited;
            dungeon[y, x] = room;
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

        /// <summary>
        /// Method that logs a visualisation in the console. 
        /// </summary>
        private void LogDungeon() {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < dungeon.GetLength(0); i++) {
                for (int j = 0; j < dungeon.GetLength(1); j++) {
                    var room = dungeon[i, j];

                    switch(room.Type) {
                        case RoomType.STARTER:
                            builder.Append("[S]");
                            break;

                        case RoomType.FINAL:
                            builder.Append("[F]");
                            break;

                        case RoomType.NORMAL:
                            builder.Append("[N]");
                            break;

                        case RoomType.EMPTY:
                            builder.Append("[" + dungeon[i, j].value + "]");
                            break;

                        case RoomType.OBSTACLE:
                            builder.Append("[O]");
                            break;
                    }
                }
                builder.AppendLine();
            }
            Debug.Log(builder);
        }



        // Misc

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
            public bool visited;

            public Room(RoomType type, int y, int x) {
                Type = type;
                value = 1;
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

