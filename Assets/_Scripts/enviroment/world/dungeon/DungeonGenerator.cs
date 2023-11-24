using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonGenerator : MonoBehaviour
{

    private Room[,] dungeon = new Room[9,9];
    private List<Room> rooms;

    void Start() {
        GenerateDungeon();
        LogDungeon();
    }

    void GenerateDungeon() {
        rooms = new List<Room>(); // just to make sure that the list is empty

        InstantiateTiles();

        var starterRoom = new Room(4, 4, TileType.ROOM);
        dungeon[4, 4] = starterRoom;
        rooms.Add(starterRoom);
        while (rooms.Count > 0) {

            Room currentRoom = rooms.First();

            int curX = currentRoom.x;
            int curY = currentRoom.y;

            rooms.Remove(currentRoom);

            for(int i = 0; i < 3; i++) {
                Direction randomDirection = (Direction)Random.Range(1, 5);
                if(CheckForBranch(curY, curX, randomDirection)) {
                    CreateBranch(curY, curX, randomDirection);
                }
            }
        }
    }

    void InstantiateTiles() {
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                dungeon[i, j] = new Room(i, j, TileType.EMPTY);
            }
        }
    }

    private void LogDungeon() {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < dungeon.GetLength(0); i++) {
            for (int j = 0; j < dungeon.GetLength(1); j++) {
                builder.Append(dungeon[i, j].ToString());
            }
            builder.AppendLine();
        }
        Debug.Log(builder);
    }
    /// <summary>
    /// Method that checks if there is enough space to spawn a room and a corridor in an area.
    /// </summary>
    /// <param name="y">Y cordinate of the room you are checking from.</param>
    /// <param name="x">X cordinate of the room you are checking from.</param>
    /// <param name="direction">Direction you want to check in.</param>
    private bool CheckForBranch(int y, int x, Direction direction) {
        switch(direction) {
            case Direction.UP:
                return CheckAroundRoom(y - 2, x);

            case Direction.DOWN:
                return CheckAroundRoom(y + 2, x);

            case Direction.LEFT:
                return CheckAroundRoom(y, x - 2);

            case Direction.RIGHT:
                return CheckAroundRoom(y, x + 2);

            default:
                Debug.LogWarning("Invalid input direction");
                return false;
        }
    }
    private void CreateBranch(int y, int x, Direction direction) {
        switch (direction) {
            case Direction.UP:
                dungeon[y - 1, x] = new Room(y - 1, x, TileType.CORRIDOR);
                CreateRoom(y - 2, x);
                break;

            case Direction.DOWN:
                dungeon[y + 1, x] = new Room(y + 1, x, TileType.CORRIDOR);
                CreateRoom(y + 2, x);
                break;

            case Direction.LEFT:
                dungeon[y, x - 1] = new Room(y, x - 1, TileType.CORRIDOR);
                CreateRoom(y, x - 2);
                break;

            case Direction.RIGHT:
                dungeon[y, x + 1] = new Room(y, x + 1, TileType.CORRIDOR);
                CreateRoom(y, x + 2);
                break;

            default:
                Debug.LogWarning("Invalid input direction");
                return;
        }
    }
    private bool CheckAroundRoom(int y, int x) {
        int failsafe = 0;
        for(int i = -1; i < 2; i++) {
            for(int j = -1; j < 2; j++) {
                try {
                    if (dungeon[y - i, x - j].Type != TileType.EMPTY) {
                        return false;
                    }
                } catch(IndexOutOfRangeException) {
                    failsafe++;
                    continue;
                }
                
            }
        }
        return failsafe > 3 ? false : true;
    }
    private void CreateRoom(int y, int x) {
        GenerateRoom(y, x);
        try {
            if (Random.Range(1, 11) % 2 == 0) {
                Direction randomDirection = (Direction)Random.Range(1, 5);
                switch (randomDirection) {
                    case Direction.UP: GenerateRoom(y - 1, x); break;
                    case Direction.DOWN: GenerateRoom(y - 1, x); break;
                    case Direction.LEFT: CreateRoom(y, x - 1); break;
                    case Direction.RIGHT: CreateRoom(y, x + 1); break;
                    default: Debug.LogWarning("Invalid input direction"); return;
                }
            }
        } catch { }
        
    }
    private void GenerateRoom(int y, int x) {
        Room newRoom = new Room(y, x, TileType.ROOM);
        dungeon[y, x] = newRoom;
        rooms.Add(newRoom);
    }

    class Room {
        public int y, x;
        public TileType Type { get; private set; }

        public Room(int y, int x, TileType type) {
            this.y = y;
            this.x = x;
            Type = type;
        }

        public override string ToString() {
            return "[" + Type.ToString()[0] + "]";
        }
    }

    public enum TileType {
        EMPTY,
        ROOM,
        CORRIDOR
    }

    public enum Direction {
        UP = 1, DOWN = 2, LEFT = 3, RIGHT = 4
    }
}
