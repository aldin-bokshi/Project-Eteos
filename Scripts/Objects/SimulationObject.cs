using Godot;

namespace ProjectEteos.Scripts.Objects;

public class SimulationObject
{
    public float Mass { get; set; }
    public Vector3 Position { get; set; }
    public Vector3 Velocity { get; set; }
    public Vector3 AccumulatedForce { get; set; }
}
