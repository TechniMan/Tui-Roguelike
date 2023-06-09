using Terminal.Gui;

namespace TuiRoguelike.Entities;

/// <summary>
/// A generic object to represent players, enemies, items, etc.
/// </summary>
internal class Entity
{
    public int X { get; set; }
    public int Y { get; set; }
    public char C { get; set; }

    public Entity(int x, int y, char c, Color col = Color.White)
    {
        X = x;
        Y = y;
        C = c;
    }

    public void Move(int dx, int dy)
    {
        X += dx;
        Y += dy;
    }
}
