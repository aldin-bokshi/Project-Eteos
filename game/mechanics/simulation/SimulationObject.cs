namespace ProjectEteos.Game.Mechanics.Simulation;

public class SimulationObject
{
    public float Mass { get; set; }
    public System.Numerics.Vector3 Position { get; set; }
    public System.Numerics.Vector3 Velocity { get; set; }
    public System.Numerics.Vector3 AccumulatedForce { get; set; }
}
