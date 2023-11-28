using roguelike.core.utils.mathematicus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static roguelike.core.utils.DirectionUtils;
using Random = UnityEngine.Random;

public class DungeonGenerator : MonoBehaviour
{
    private int dungeonSize = 11;
    private Room[,] dungeon;
    private List<Room> rooms;
    private List<Room> generatedRooms;
    private int amountOfrooms;

    void Start() {
        dungeon = new Room[dungeonSize, dungeonSize];
        for(int i = 0; i < 50; i++) {
            do {
                GenerateDungeon();
            } while(amountOfrooms < 8);
            FilloutDungeon();
            LogDungeon();
        }
    }

    private void GenerateDungeon() {
        rooms = new List<Room>(); // just to make sure that the list is empty
        generatedRooms = new List<Room>(); // just to make sure that the list is empty
        amountOfrooms = 0;

        InstantiateTiles();

        var starterRoom = new Room(4, 4, TileType.ROOM, RoomType.STARTER);
        dungeon[4, 4] = starterRoom;
        rooms.Add(starterRoom);
        while (rooms.Count > 0) {
            Room currentRoom = rooms.First();

            int curX = currentRoom.x;
            int curY = currentRoom.y;

            rooms.Remove(currentRoom);

            for(int i = 0; i < 3; i++) {
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

    private void FilloutDungeon() {
        Room room = generatedRooms[Random.Range(0, generatedRooms.Count())];
        dungeon[room.y, room.x].RoomType = RoomType.FINAL;
    }

    private void CheckAndGenerate(int y, int x, Direction direction) {
        bool doubleRoom = Mathematicus.ChanceIn(65f);
        Direction randomDirection = RandomDirection();
        while(randomDirection == direction) { randomDirection = RandomDirection(); }
        try {
            if (CheckAroundRoom(y, x, doubleRoom, randomDirection)) {
                CreateRoom(y, x, doubleRoom, direction, getOpposite(randomDirection));
            }
        } catch (IndexOutOfRangeException) { }
    }

    private void InstantiateTiles() {
        for (int i = 0; i < dungeonSize; i++) {
            for (int j = 0; j < dungeonSize; j++) {
                dungeon[i, j] = new Room(i, j, TileType.EMPTY, RoomType.NONE);
            }
        }
    }

    private void LogDungeon() {
        StringBuilder builder = new StringBuilder();
        builder.Append("   ");
        for(int i = 0; i < dungeon.GetLength(0); i++) {
            string space = i < 10 ? " " : "";
            builder.Append(space + i + " ");
        }
        builder.AppendLine("\n   ------------------------------------------------------");

        for (int i = 0; i < dungeon.GetLength(0); i++) {
            string separator = i < 10 ? " |" : "|";
            builder.Append(i + separator);
            for (int j = 0; j < dungeon.GetLength(1); j++) {
                builder.Append(dungeon[i, j].ToString());
            }
            builder.AppendLine();
        }
        Debug.Log(builder);
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
            for(int j = startX; j < limitX; j++) {
                builder.AppendLine("Checking in: y" + (y + i) + " | x" + (x + j));

                try {
                    if (dungeon[y + i, x + j].TileType != TileType.EMPTY) {
                        return false;
                    }
                } catch(IndexOutOfRangeException) {
                    failsafe++;
                    continue;
                }
                
            }
        }
        if(doubleRoom) { Debug.LogWarning(builder); }
        return failsafe > 3 ? false : true;
    }

    private void CreateRoom(int y, int x, bool doubleRoom, Direction sourceDirection, Direction roomDirection) {
        if (Mathematicus.ChanceIn(5f)) { return; }
        switch (getOpposite(sourceDirection)) {
            case Direction.UP: GenerateRoom(y - 1, x, TileType.CORRIDOR); break;
            case Direction.DOWN: GenerateRoom(y + 1, x, TileType.CORRIDOR); break;
            case Direction.LEFT: GenerateRoom(y, x - 1, TileType.CORRIDOR); break;
            case Direction.RIGHT: GenerateRoom(y, x + 1, TileType.CORRIDOR); break;
            default: Debug.LogWarning("Invalid input direction"); return;
        }
        GenerateRoom(y, x);
        if(doubleRoom) {
            switch (roomDirection) {
                case Direction.UP: GenerateRoom(y - 1, x); break;
                case Direction.DOWN: GenerateRoom(y - 1, x); break;
                case Direction.LEFT: GenerateRoom(y, x - 1); break;
                case Direction.RIGHT: GenerateRoom(y, x + 1); break;
                default: Debug.LogWarning("Invalid input direction"); return;
            }
        }
    }

    private void GenerateRoom(int y, int x, TileType type = TileType.ROOM) {
        Room newRoom = new Room(y, x, type);
        dungeon[y, x] = newRoom;
        if(type == TileType.ROOM) { 
            rooms.Add(newRoom);
            generatedRooms.Add(newRoom);
            amountOfrooms++;
        }
    }

    struct Room {
        public int y, x;
        public TileType TileType { get; private set; }
        public RoomType RoomType;
        public bool isDoubleRoom;

        public Room(int y, int x, TileType type, RoomType roomType = RoomType.NORMAL) {
            this.y = y;
            this.x = x;
            TileType = type;
            RoomType = roomType;
            isDoubleRoom = false;
        }

        public override string ToString() {
            switch (RoomType) {
                case RoomType.NONE:
                    return " " + TileType.ToString()[0] + " ";
                case RoomType.NORMAL:
                    return "[" + TileType.ToString()[0] + "]";
                case RoomType.FINAL:
                    return "(" + TileType.ToString()[0] + ")";
                case RoomType.TREASURE:
                    return "{" + TileType.ToString()[0] + "}";
                default:
                    return "|" + TileType.ToString()[0] + "|";
            }
        }
    }

    public enum TileType {
        EMPTY,
        ROOM,
        CORRIDOR
    }

    public enum RoomType {
        NONE,
        NORMAL,
        STARTER,
        FINAL,
        TREASURE
    }
}
