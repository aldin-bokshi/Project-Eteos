using System;
using System.Collections.Generic;
using ProjectEteos.Game.Mechanics.Simulation;

namespace ProjectEteos.Game.World;

public class SimulationWorld
{
    private readonly List<SimulationObject> _objects = new();

    public IReadOnlyCollection<SimulationObject> Objects => _objects;

    public void AddObject(SimulationObject body)
    {
        if (body is null)
        {
            throw new ArgumentNullException(nameof(body));
        }

        _objects.Add(body);
    }

    public void RemoveObject(SimulationObject body)
    {
        if (body is null)
        {
            throw new ArgumentNullException(nameof(body));
        }

        _objects.Remove(body);
    }

    public void Step(float delta, PhysicsSystem physics)
    {
        if (physics is null)
        {
            throw new ArgumentNullException(nameof(physics));
        }

        foreach (var body in _objects)
        {
            physics.Step(body, delta);
        }
    }
}
