using roguelike.system.singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static roguelike.system.manager.DungeonManager.Room;
using Random = UnityEngine.Random;
using static roguelike.core.utils.DirectionUtils;

namespace roguelike.system.manager {
    public class DungeonManager : Singleton<DungeonManager> {

        public Transform platform;
        public Transform otherPlatform;
        public Transform cube;

        public Room[,] dungeon; // [y,x] WHYYYY?!?!? why isn't it [x,y]?????

        List<Room> finalRooms;

        private DungeonLength length = DungeonLength.SHORT;

        protected override void Awake() {
            do {
                GenerateMain();
            } while(dungeon == null);
            SpawnDungeon();
            base.Awake();
        }



        // Main methods

        /// <summary>
        /// Method for generating the main hallway of the dungeon.
        /// </summary>
        public void GenerateMain() {
            finalRooms = new List<Room>();
            List<Room> deadEnds = new List<Room>();

            Room starterRoom;

            int size = (int)length;
            int center = (int)Math.Floor(size / 2f) + 1;

            dungeon = new Room[size, size];

            for(int i = 0; i < size; i++) {
                for(int j = 0; j < size; j++) {
                    if(Random.Range(0, 100) < 35) {
                        if(!CheckAroundForType(i, j, RoomType.OBSTACLE)) {
                            dungeon[i, j] = new Room(RoomType.OBSTACLE, i, j);
                            Instantiate(cube, new Vector3(i * 10, 0, j * 10), new Quaternion(0, 0, 0, 0));
                        } else {
                            dungeon[i, j] = new Room(RoomType.EMPTY, i, j);
                        }
                    } else {
                        dungeon[i, j] = new Room(RoomType.EMPTY, i, j);
                    }
                }
            }

            starterRoom = GenerateRoom(center, center, RoomType.STARTER, true);
            
            for(int i = 0; i < 5; i++) {
                deadEnds.Add(GenerateRoom(Random.Range(1, size - 1), Random.Range(1, size - 1), RoomType.FINAL));
            }

            Debug.Log(deadEnds.Count);

            foreach(Room room in deadEnds) {
                GeneratePath(starterRoom, room);
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

        public void ResetValues() {
            for(int i = 0; i < (int)length; i++) {
                for(int j = 0; j < (int)length; j++) {
                    dungeon[i, j].value = (int) dungeon[i, j].Type;
                    if(dungeon[i, j].Type != RoomType.STARTER) {
                        dungeon[i, j].visited = false;
                    }
                }
            }
        }

        public void GeneratePath(Room startRoom, Room finalRoom) {

            ResetValues();

            List<Room> rooms = new List<Room>();
            List<Room> path = new List<Room>();
            Room currentRoom = startRoom;

            rooms.Add(startRoom);

            int failsafe = 0;
            bool pathFound = false;

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
                            tempRoom.value += currentRoom.value;
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

                    if(tempRoom.x == startRoom.x && tempRoom.y == startRoom.y) {
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

        public bool CheckAroundForType(int y, int x, RoomType type) {
            for(int i = -1; i < 1; i++) {
                for(int j = -1; j < 1; j++) {
                    try {
                        if(dungeon[y + i, x + j] != null && dungeon[y + i, x + j].Type == type) {
                            return true;
                        }
                    } catch(IndexOutOfRangeException) {
                        continue;
                    }
                }
            }
            return false;
        }



        // Misc

        public enum DungeonHazard {

        }

        public enum DungeonLength {
            SHORT = 11,
            MEDIUM = 15,
            LONG = 21,
            VERYLONG = 31
        }

        public class Room {
            public RoomType Type;

            public List<Direction> walls = new List<Direction>();

            public int value, y, x;
            public bool visited;

            public Room(RoomType type, int y, int x) {
                Type = type;
                value = (int)type;
                this.y = y;
                this.x = x;
            }

            public override string ToString() {
                return ((int)Type).ToString();
            }

            public enum RoomType {
                STARTER = 1,
                NORMAL = 1,
                FINAL = 1,
                SPECIAL = 2,
                EMPTY = 2,
                OBSTACLE = 999
            }
        }
    }
}

