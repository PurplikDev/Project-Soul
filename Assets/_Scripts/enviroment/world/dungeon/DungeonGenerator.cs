using roguelike.core.utils.mathematicus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static roguelike.core.utils.DirectionUtils;

namespace roguelike.enviroment.world.dungeon {
    public class DungeonGenerator {
        private int _dungeonSize;
        private Dungeon dungeon;
        private int amountOfrooms;
        private List<Room> rooms;

        public Dungeon GenerateDungeon(int dungeonSize) {
            _dungeonSize = dungeonSize;

            dungeon = new Dungeon(_dungeonSize);
            GenerateDungeon();
            //do { GenerateDungeon(); } while (amountOfrooms < 10);
            Debug.Log(dungeon.ToString());

            return dungeon;
        }

        /// <summary>
        /// Method for generating the dungeon layout.
        /// </summary>
        private void GenerateDungeon() {
            rooms = new List<Room>();
            var starterRoom = (Room) dungeon.SetTile(5, 5, TileType.ROOM, RoomType.STARTER);

            rooms.Add(starterRoom);

            while(rooms.Count > 0) {
                Room currentRoom = rooms.First();
                rooms.Remove(currentRoom);

                int curX = currentRoom.x;
                int curY = currentRoom.y;

                for (int i = 0; i < 3; i++) {
                    Direction sourceDirection = RandomDirection();
                    switch (sourceDirection) {
                        case Direction.UP: CheckAndGenerate(curY - 2, curX, sourceDirection); break;
                        case Direction.DOWN: CheckAndGenerate(curY + 2, curX, sourceDirection); break;
                        case Direction.LEFT: CheckAndGenerate(curY, curX - 2, sourceDirection); break;
                        case Direction.RIGHT: CheckAndGenerate(curY, curX + 2, sourceDirection); break;
                    }
                }
            }
        }

        

        // UTIL METHODS

        private void CheckAndGenerate(int y, int x, Direction direction) {
            bool doubleRoom = Mathematicus.ChanceIn(65f);
            Direction randomDirection = RandomDirection();
            while (randomDirection == direction) { randomDirection = RandomDirection(); }
            try {
                if (CheckAroundRoom(y, x, doubleRoom, randomDirection)) {
                    CreateRoom(y, x, doubleRoom, direction, getOpposite(randomDirection));
                }
            } catch (IndexOutOfRangeException) { }
        }

        private bool CheckAroundRoom(int y, int x, bool doubleRoom, Direction direction) {
            int failsafe = 0;
            int startY = -1;
            int startX = -1;
            int limitY = 2;
            int limitX = 2;

            if (doubleRoom) {
                startY = direction == Direction.UP ? -2 : -1;
                startX = direction == Direction.LEFT ? -2 : -1;
                limitY = direction == Direction.DOWN ? 3 : 2;
                limitX = direction == Direction.RIGHT ? 3 : 2;
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Center of check: y" + y + " | x" + x);
            for (int i = startY; i < limitY; i++) {
                for (int j = startX; j < limitX; j++) {
                    builder.AppendLine("Checking in: y" + (y + i) + " | x" + (x + j));
                    try {
                        if (dungeon.GetTile(y + i, x + j).TileType != TileType.EMPTY) {
                            return false;
                        }
                    } catch (IndexOutOfRangeException) {
                        failsafe++;
                        continue;
                    }

                }
            }
            //if (doubleRoom) { Debug.LogWarning(builder); }
            return failsafe > 3 ? false : true;
        }

        private void CreateRoom(int y, int x, bool doubleRoom, Direction sourceDirection, Direction roomDirection) {
            if(Mathematicus.ChanceIn(1f)) { return; }
            switch(getOpposite(sourceDirection)) {
                case Direction.UP: dungeon.SetTile(y - 1, x, TileType.CORRIDOR); break;
                case Direction.DOWN: dungeon.SetTile(y + 1, x, TileType.CORRIDOR); break;
                case Direction.LEFT: dungeon.SetTile(y, x - 1, TileType.CORRIDOR); break;
                case Direction.RIGHT: dungeon.SetTile(y, x + 1, TileType.CORRIDOR); break;
                default: Debug.LogWarning("Invalid input direction"); return;
            }
            
            dungeon.SetTile(y, x, TileType.ROOM, RoomType.NORMAL);
            if(doubleRoom) {
                int y2, x2;

                switch(roomDirection) {
                    case Direction.UP:
                        y2 = y - 1;
                        x2 = x;
                        break;
                    case Direction.DOWN:
                        y2 = y + 1;
                        x2 = x;
                        break;
                    case Direction.LEFT:
                        y2 = y;
                        x2 = x - 1;
                        break;
                    case Direction.RIGHT:
                        y2 = y;
                        x2 = x + 1;
                        break;
                    default: Debug.LogWarning("Invalid input direction"); return;
                }
                rooms.Add((Room)dungeon.SetTile(y2, x2, TileType.ROOM, RoomType.NORMAL));
            } else {
                rooms.Add((Room) dungeon.GetTile(y, x));
            }
            
        }
    }
}