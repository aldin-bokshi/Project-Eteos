using Godot;

// namespace ProjectEteos.Game.Mechanics.Tests.M002_ForceTest;
namespace ProjectEteos.game.mechanics.tests.m002_forces;

public partial class ForcesTestRunner : Node
{
    public override void _Ready()
    {
        var test = new ForceTest();

        test.Run();

        GetTree().Quit();
    }
}