using roguelike.core.utils;
using roguelike.core.utils.mathematicus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static roguelike.core.utils.DirectionUtils;

public class DungeonGenerator : MonoBehaviour
{

    private Room[,] dungeon = new Room[9,9];
    private List<Room> rooms;
    private List<Room> generatedRooms;
    private int amountOfrooms;

    void Start() {
        do {
            GenerateDungeon();
        } while (amountOfrooms < 8);
        LogDungeon();
    }

    void GenerateDungeon() {
        rooms = new List<Room>(); // just to make sure that the list is empty
        generatedRooms = new List<Room>(); // just to make sure that the list is empty
        amountOfrooms = 0;

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

    private void CheckAndGenerate(int y, int x, Direction direction) {
        bool doubleRoom = Mathematicus.ChanceIn(50f);
        Direction randomDirection = RandomDirection();
        while(randomDirection == direction) { randomDirection = RandomDirection(); }
        try {
            if (CheckAroundRoom(y, x, doubleRoom, randomDirection)) {
                CreateRoom(y, x, doubleRoom, direction, randomDirection);
            }
        } catch (IndexOutOfRangeException) {
            Debug.LogWarning("Coordinates out of range");
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
    
    private bool CheckAroundRoom(int y, int x, bool doubleRoom, Direction direction) {
        int failsafe = 0;
        int limitI = doubleRoom == true && direction == Direction.UP || direction == Direction.DOWN ? 3 : 2;
        int limitJ = doubleRoom == true && direction == Direction.LEFT || direction == Direction.RIGHT ? 3 : 2;

        for (int i = -1; i < limitI; i++) {
            for(int j = -1; j < limitJ; j++) {
                try {
                    if (dungeon[y + i, x + j].Type != TileType.EMPTY) {
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
}
