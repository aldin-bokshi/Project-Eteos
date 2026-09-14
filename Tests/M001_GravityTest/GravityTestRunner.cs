using Godot;

namespace ProjectEteos.Tests.M001_GravityTest;

public partial class GravityTestRunner : Node
{
    public override void _Ready()
    {
        var test = new GravityTest();

        test.Run();

        GetTree().Quit();
    }
}