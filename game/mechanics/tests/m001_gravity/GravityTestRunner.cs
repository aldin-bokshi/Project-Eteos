using Godot;

namespace ProjectEteos.game.mechanics.tests.m001_gravity;

public partial class GravityTestRunner : Node
{
    public override void _Ready()
    {
        var test = new GravityTest();

        test.Run();

        GetTree().Quit();
    }
}