using Terminal.Gui;
using TuiRoguelike.Entities;
using TuiRoguelike.World;

namespace TuiRoguelike;

internal interface IAction
{
    public void Perform(GameMap map, Entity entity);
}

internal class EscapeAction : IAction
{
    public void Perform(GameMap map, Entity entity)
    {
        Application.RequestStop();
    }
}

internal class MovementAction : IAction
{
    private readonly int _dx;
    private readonly int _dy;

    public MovementAction(int dx, int dy)
    {
        _dx = dx;
        _dy = dy;
    }

    public void Perform(GameMap map, Entity entity)
    {
        var destinationX = entity.X + _dx;
        var destinationY = entity.Y + _dy;

        // don't move if out of bounds
        // don't move if not walkable
        if (!map.InBounds(destinationX, destinationY) ||
            !map.IsWalkable(destinationX, destinationY))
        {
            return;
        }

        entity.Move(_dx, _dy);
    }
}
