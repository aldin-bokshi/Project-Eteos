using Godot;
using ProjectEteos.Game.World;

namespace ProjectEteos.Game.World;

public partial class SimulationWorldView : Node
{
    public SimulationWorld Simulation { get; private set; } = new();

    public override void _Ready()
    {
        if (Simulation is null)
        {
            Simulation = new SimulationWorld();
        }
    }
}
