using System;
using System.Collections.Generic;
using System.Text;
using static roguelike.core.utils.DirectionUtils;

namespace roguelike.environment.world.dungeon {
    public class Dungeon {        
        Tile[,] dungeonLayout;

        /// <summary>
        /// All non empty tiles...
        /// </summary>
        public List<Tile> AllTiles;
        public List<Room> AllRooms;

        public Dungeon(int dungeonSize) {
            dungeonLayout = new Tile[dungeonSize, dungeonSize];
            InstantiateTiles(dungeonSize);
            AllTiles = new List<Tile>();
            AllRooms = new List<Room>();
        }

        public Tile SetTile(int y, int x, TileType tileType, RoomType roomType = RoomType.NONE) {
            Tile tile = tileType != TileType.ROOM ? new Tile(y, x, tileType) : new Room(y, x, TileType.ROOM, roomType);
            dungeonLayout[y, x] = tile;

            if(tile.TileType == TileType.EMPTY) { return tile; }
            AllTiles.Add(tile);
            if(tile is Room room) { AllRooms.Add(room); }
            return tile;
        }

        public Tile GetTile(int y, int x) {
            return dungeonLayout[y, x];
        }

        private Tile GetRelativeRoom(int y, int x, Direction direction) {
            try {
                switch(direction) {
                    case Direction.UP: return dungeonLayout[y - 1, x];
                    case Direction.DOWN: return dungeonLayout[y + 1, x];
                    case Direction.LEFT: return dungeonLayout[y, x - 1];
                    case Direction.RIGHT: return dungeonLayout[y, x + 1];
                }
            } catch(IndexOutOfRangeException) { }
            return null;
        }

        private void InstantiateTiles(int size) {
            for(int i = 0; i < size; i++) {
                for(int j = 0; j < size; j++) {
                    SetTile(i, j, TileType.EMPTY);
                }
            }
        }

        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            builder.Append("   ");
            for(int i = 0; i < dungeonLayout.GetLength(0); i++) {
                string space = i < 10 ? " " : "";
                builder.Append(space + i + " ");
            }
            builder.Append("\n   ");
            for(int i = 0; i < dungeonLayout.GetLength(0); i++) { builder.Append("---"); }
            builder.Append("\n");
            for(int i = 0; i < dungeonLayout.GetLength(1); i++) {
                string separator = i < 10 ? " |" : "|";
                builder.Append(i + separator);
                for(int j = 0; j < dungeonLayout.GetLength(0); j++) {
                    builder.Append(GetTile(i, j).ToString());
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }

    public class Tile {
        public int y, x;
        public TileType TileType { get; private set; }

        public Tile(int y, int x, TileType type) {
            this.y = y;
            this.x = x;
            TileType = type;
        }

        public override string ToString() {
            return " " + TileType.ToString()[0] + " ";
        }
    }

    public class Room : Tile {

        RoomType _type;
        public Dictionary<Direction, WallType> wallData = new Dictionary<Direction, WallType>();

        public Room(int y, int x, TileType type, RoomType roomType) : base(y, x, type) {
            _type = roomType;
        }

        public override string ToString() {
            switch(_type) {
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
    public enum WallType {
        NONE,
        WALL,
        DOOR
    }
}