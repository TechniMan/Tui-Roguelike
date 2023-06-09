using Terminal.Gui;

namespace MyFirstTuiProject;

internal interface IInputHandler
{
    public IAction? Dispatch(Key key);
}

internal class EventHandler: IInputHandler
{
    public IAction? Dispatch(Key key)
    {
        return key switch
        {
            Key.CursorUp => new MovementAction(0, -1),
            Key.CursorDown => new MovementAction(0, 1),
            Key.CursorLeft => new MovementAction(-1, 0),
            Key.CursorRight => new MovementAction(1, 0),
            Key.Esc => new EscapeAction(),
            _ => null
        };
    }
}
