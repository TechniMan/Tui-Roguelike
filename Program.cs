using MyFirstTuiProject;
using Terminal.Gui;

Application.Init();

try
{
    // init the game
    var game = new Game()
    {
        X = 0,
        Y = 0,
        Width = Dim.Fill(),
        Height = Dim.Fill()
    };

    Application.Run(game);
}
finally
{
    Application.Shutdown();
}
