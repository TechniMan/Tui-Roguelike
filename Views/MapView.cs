using Terminal.Gui;
using TuiRoguelike.Entities;
using TuiRoguelike.World;

namespace TuiRoguelike.Views;

internal class MapView : View
{
    public MapView()
        : base()
    {
    }

    /// <summary>
    /// Renders all the map's tiles to the view
    /// </summary>
    public void DrawGameMap(GameMap map)
    {
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                //if (map.IsVisible(x, y))
                //{
                //    AddRune(x, y, map.GetTile(x, y).Light.Char);
                //}
                //else if (map.IsExplored(x, y))
                //{
                //    AddRune(x, y, map.GetTile(x, y).Dark.Char);
                //}
                //else
                //{
                //    AddRune(x, y, TileGraphics.Fog.Char);
                //}
                AddRune(x, y, map.GetTile(x, y).Light.Char);
            }
        }
    }

    /// <summary>
    /// Renders all the entities to the view
    /// </summary>
    public void DrawEntities(List<Entity> entities)
    {
        foreach (var e in entities)
        {
            AddRune(e.X, e.Y, e.C);
        }
    }
}
