using System.Numerics;

namespace ProjectEteos.Game.Mechanics.Simulation;

public class PhysicsSystem
{
    public Vector3 Gravity { get; set; } = new(0f, -9.81f, 0f);

    public void ApplyForce(SimulationObject body, Vector3 force)
    {
        body.AccumulatedForce += force;
    }

    public void ApplyImpulse(SimulationObject body, Vector3 impulse)
    {
        body.Velocity += impulse / body.Mass;
    }

    public void Step(SimulationObject body, float delta)
    {
        Vector3 gravityForce = Gravity * body.Mass;
        ApplyForce(body, gravityForce);

        Vector3 acceleration = body.AccumulatedForce / body.Mass;
        body.Velocity += acceleration * delta;
        body.Position += body.Velocity * delta;
        body.AccumulatedForce = Vector3.Zero;
    }
}