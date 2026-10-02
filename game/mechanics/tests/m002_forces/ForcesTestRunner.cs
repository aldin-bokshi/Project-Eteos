using Godot;

namespace ProjectEteos.Tests.M002_ForceTest;

public partial class ForcesTestRunner : Node
{
    public override void _Ready()
    {
        var test = new ForceTest();

        test.Run();

        GetTree().Quit();
    }
}