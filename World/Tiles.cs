using Terminal.Gui;

namespace TuiRoguelike.World;

internal class TileGraphics
{
    public char Char { get; private set; }
    public Color Foreground { get; private set; }
    public Color Background { get; private set; }

    public TileGraphics(char @char, Color foreground = Color.White, Color background = Color.Black)
    {
        Char = @char;
        Foreground = foreground;
        Background = background;
    }

    public static TileGraphics Fog => new(' ');
}

internal class Tile
{
    /// <summary>
    /// Can an entity move into this tile
    /// </summary>
    public bool Walkable { get; private set; }
    /// <summary>
    /// Can an entity see to/through this tile
    /// </summary>
    public bool Transparent { get; private set; }
    /// <summary>
    /// Can the player see this tile
    /// </summary>
    public bool IsVisible { get; set; }
    /// <summary>
    /// Has the player seen this tile
    /// </summary>
    public bool IsExplored { get; set; }
    /// <summary>
    /// What this tile looks like when not currently visible
    /// </summary>
    public TileGraphics Dark { get; private set; }
    /// <summary>
    /// What this tile looks like when visible
    /// </summary>
    public TileGraphics Light { get; private set; }

    public Tile(bool walkable, bool transparent, TileGraphics dark, TileGraphics light)
    {
        Walkable = walkable;
        Transparent = transparent;
        IsVisible = false;
        IsExplored = false;
        Dark = dark;
        Light = light;
    }
}

internal class Tiles
{
    public static Tile Floor => new(true, true, new TileGraphics('.'), new TileGraphics(' '));
    public static Tile Wall => new(false, false, new TileGraphics('#'), new TileGraphics('#'));
}
