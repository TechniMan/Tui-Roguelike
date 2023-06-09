using MyFirstTuiProject.Entities;
using MyFirstTuiProject.Views;
using MyFirstTuiProject.World;
using Terminal.Gui;

namespace MyFirstTuiProject;

internal class Game: Toplevel
{
    // things
    private readonly Entity _player;
    private readonly List<Entity> _entities;
    private readonly GameMap _gameMap;
    private readonly IInputHandler _inputHandler;

    // views
    //private readonly MenuBar _menu;
    private readonly MapView _map;



    /// <summary>
    /// Initial setup
    /// </summary>
    public Game()
    {
        // initialise entities
        _player = new Entity(10, 10, '@');
        _entities = new List<Entity>();
        _entities.AddRange(new Entity[] {
            _player,
            new Entity(20, 10, 'a')
        });

        // new event handler
        _inputHandler = new EventHandler();

        // setup map view
        var mapWidth = 120;
        var mapHeight = 30;
        _map = new MapView()
        {
            X = 0,
            Y = 0,
            Width = mapWidth,
            Height = mapHeight
        };
        Add(_map);
        
        // initialise game map
        var roomMinSize = 6;
        var roomMaxSize = 10;
        var maxRooms = 30;
        _gameMap = ProcGen.GenerateDungeon(maxRooms, roomMinSize, roomMaxSize, mapWidth, mapHeight, _player);
    }



    /// <summary>
    /// Handle input and events
    /// </summary>
    public override bool OnKeyDown(KeyEvent keyEvent)
    {
        var action = _inputHandler.Dispatch(keyEvent.Key);

        // if an action happened, perform it and refresh the view
        if (action != null)
        {
            action.Perform(_gameMap, _player);
            SetNeedsDisplay();
        }

        return action != null;
    }



    /// <summary>
    /// Render the game interface
    /// </summary>
    public override void OnDrawContent(Rect viewport)
    {
        base.OnDrawContent(viewport);

        // draw the map
        _map.DrawGameMap(_gameMap);
        _map.DrawEntities(_entities);
    }
}
