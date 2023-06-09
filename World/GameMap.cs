using Terminal.Gui;

namespace MyFirstTuiProject.World;

internal class GameMap
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public List<Tile> Tiles { get; init; }

    /// <summary>
    /// Initialises a map with width*height wall tiles
    /// </summary>
    public GameMap(int width, int height)
    {
        Width = width;
        Height = height;
        Tiles = new List<Tile>(width * height);

        // it's all walls, walls everywhere
        for (int t = 0; t < Width * Height; t++)
        {
            Tiles.Add(World.Tiles.Wall);
        }
    }

    public Tile GetTile(int x, int y)
    {
        return Tiles[y * Width + x];
    }

    /// <summary>
    /// Set given tile
    /// </summary>
    public void SetTile(int x, int y, Tile tile)
    {
        Tiles[y * Width + x] = tile;
    }

    /// <summary>
    /// Set given area of tiles to given value
    /// </summary>
    public void SetTiles(Rect area, Tile tile)
    {
        for (int y = 0; y < area.Height; y++)
        {
            for (int x = 0; x < area.Width; x++)
            {
                SetTile(area.X + x, area.Y + y, tile);
            }
        }
    }

    /// <summary>
    /// Returns true if x and y are inside the bounds of this map.
    /// </summary>
    public bool InBounds(int x, int y)
    {
        return 0 <= x && x < Width &&
               0 <= y && y < Height;
    }

    public bool IsWalkable(int x, int y)
    {
        return Tiles[y * Width + x].Walkable;
    }

    public bool IsTransparent(int x, int y)
    {
        return Tiles[y * Width + x].Transparent;
    }

    public bool IsVisible(int x, int y)
    {
        return Tiles[y * Width + x].IsVisible;
    }

    public bool IsExplored(int x, int y)
    {
        return Tiles[y * Width + x].IsExplored;
    }
}
