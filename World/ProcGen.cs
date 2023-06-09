using Terminal.Gui;
using TuiRoguelike.Entities;

namespace TuiRoguelike.World;

internal class RectangularRoom
{
    public int X1 { get; private set; }
    public int Y1 { get; private set; }
    public int X2 { get; private set; }
    public int Y2 { get; private set; }
    public int Width => X2 - X1;
    public int Height => Y2 - Y1;

    public RectangularRoom(int x, int y, int w, int h)
    {
        X1 = x;
        Y1 = y;
        X2 = x + w;
        Y2 = y + h;
    }

    public Point Centre => new((X1 + X2) / 2, (Y1 + Y2) / 2);

    /// <summary>
    /// The inner area of this room
    /// </summary>
    public Rect Inner => new(X1 + 1, Y1 + 1, Width - 2, Height - 2);

    /// <summary>
    /// Returns true if this room overlaps with the other room
    /// </summary>
    public bool Intersects(RectangularRoom other)
    {
        return X1 <= other.X2 &&
            X2 >= other.X1 &&
            Y1 <= other.Y2 &&
            Y2 >= other.Y1;
    }
}



internal class ProcGen
{
    private static IEnumerable<Point> TunnelBetween(Point start, Point end)
    {
        var x1 = start.X;
        var y1 = start.Y;
        var x2 = end.X;
        var y2 = end.Y;

        // randomly go hori, then verti ...
        if (Random.Shared.Next() % 2 == 0)
        {
            int inc = x1 < x2 ? 1 : -1;
            for (int x = x1; x != x2; x += inc)
            {
                yield return new Point(x, y1);
            }
            inc = y1 < y2 ? 1 : -1;
            for (int y = y1; y != y2; y += inc)
            {
                yield return new Point(x2, y);
            }
        }
        // ... or verti, then hori
        else
        {
            int inc = x1 < x2 ? 1 : -1;
            for (int x = x1; x != x2; x += inc)
            {
                yield return new Point(x, y2);
            }
            inc = y1 < y2 ? 1 : -1;
            for (int y = y1; y != y2; y += inc)
            {
                yield return new Point(x1, y);
            }
        }
    }

    public static GameMap GenerateDungeon(int maxRooms, int roomMinSize, int roomMaxSize, int width, int height, Entity player)
    {
        var dungeon = new GameMap(width, height);
        var rooms = new List<RectangularRoom>();

        // attempt to create all the rooms
        for (int r = 0; r < maxRooms; r++)
        {
            // set up the room
            var roomWidth = Random.Shared.Next(roomMinSize, roomMaxSize + 1);
            var roomHeight = Random.Shared.Next(roomMinSize, roomMaxSize + 1);
            var x = Random.Shared.Next(0, dungeon.Width - roomWidth);
            var y = Random.Shared.Next(0, dungeon.Height - roomHeight);
            var newRoom = new RectangularRoom(x, y, roomWidth, roomHeight);

            // if this room intersects with any existing rooms, then discard it
            if (rooms.Any(otherRoom => newRoom.Intersects(otherRoom)))
            {
                continue;
            }

            // dig out the room's inner tiles
            dungeon.SetTiles(newRoom.Inner, Tiles.Floor);

            // dig a tunnel to the previous room
            if (rooms.Count > 0)
            {
                foreach (var p in TunnelBetween(newRoom.Centre, rooms.Last().Centre))
                {
                    dungeon.SetTile(p.X, p.Y, Tiles.Floor);
                }
            }
            // place the player in the first room
            else
            {
                player.X = newRoom.Centre.X;
                player.Y = newRoom.Centre.Y;
            }

            rooms.Add(newRoom);
        }

        foreach (var p in TunnelBetween(rooms.First().Centre, rooms.Last().Centre))
        {
            dungeon.SetTile(p.X, p.Y, Tiles.Floor);
        }

        return dungeon;
    }
}
