using Godot;
using ProjectEteos.Scripts.Objects;

namespace ProjectEteos.Scripts.Physics;

public partial class PhysicsSystem : Node
{
    public Vector3 Gravity { get; set; } = new(0, -9.81f, 0); // 9.81 is earths gravity and negative since its a downwards force
    
    public void Step(SimulationObject body, float delta)
    {
        Vector3 gravityForce = Gravity * body.Mass;

        body.Force += gravityForce;

        Vector3 acceleration = body.Force / body.Mass;

        body.Velocity += acceleration * delta;
        body.Position += body.Velocity * delta;

        body.Force = Vector3.Zero;
    }
}
