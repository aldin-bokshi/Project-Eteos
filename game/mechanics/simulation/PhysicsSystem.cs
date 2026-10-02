using Godot;
using ProjectEteos.Scripts.Objects;

namespace ProjectEteos.Scripts.Physics;

public partial class PhysicsSystem : Node
{
    public Vector3 Gravity { get; set; } = new(0, -9.81f, 0);

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
        TestInvaledMass();

        Vector3 gravityForce = Gravity * body.Mass;

        ApplyForce(body, gravityForce);

        Vector3 acceleration = body.AccumulatedForce / body.Mass;

        body.Velocity += acceleration * delta;
        body.Position += body.Velocity * delta;

        body.AccumulatedForce = Vector3.Zero;
    }


}