using Godot;
using ProjectEteos.Game.Mechanics.Simulation;
using Vector3 = System.Numerics.Vector3;

namespace ProjectEteos.game.mechanics.tests.m001_gravity;

public class GravityTest
{
    private const float ExpectedGravity = -9.81f;
    private const float Tolerance = 0.0001f;

    public void Run()
    {
        GD.Print("=== M001 Gravity Tests ===");

        TestFinalVelocity();
        TestFinalPosition();
        TestMassIndependence();

        GD.Print("=== ALL TESTS PASSED ===");
    }

    private void TestFinalVelocity()
    {
        var body = CreateBody(1.0f);
        var physics = new PhysicsSystem();

        Simulate(physics, body, 60, 1f / 60f);

        float expected = ExpectedGravity;
        float actual = body.Velocity.Y;

        AssertApproximately(
            actual,
            expected,
            Tolerance,
            "Final velocity"
        );
    }

    private void TestFinalPosition()
    {
        var body = CreateBody(1.0f);
        var physics = new PhysicsSystem();

        Simulate(physics, body, 60, 1f / 60f);

        // Expected result for the current semi-implicit Euler integrator.
        float expected = 5.01325f;
        float actual = body.Position.Y;

        AssertApproximately(
            actual,
            expected,
            Tolerance,
            "Final position"
        );
    }

    private void TestMassIndependence()
    {
        var lightBody = CreateBody(1.0f);
        var heavyBody = CreateBody(10.0f);

        var physics = new PhysicsSystem();

        Simulate(physics, lightBody, 60, 1f / 60f);
        Simulate(physics, heavyBody, 60, 1f / 60f);

        AssertApproximately(
            lightBody.Velocity.Y,
            heavyBody.Velocity.Y,
            Tolerance,
            "Mass independence - velocity"
        );

        AssertApproximately(
            lightBody.Position.Y,
            heavyBody.Position.Y,
            Tolerance,
            "Mass independence - position"
        );
    }

    private static SimulationObject CreateBody(float mass)
    {
        return new SimulationObject
        {
            Mass = mass,
            Position = new Vector3(0, 10, 0),
            Velocity = Vector3.Zero,
            AccumulatedForce = Vector3.Zero
        };
    }

    private static void Simulate(
        PhysicsSystem physics,
        SimulationObject body,
        int steps,
        float delta)
    {
        for (int i = 0; i < steps; i++)
        {
            physics.Step(body, delta);
        }
    }

    private static void AssertApproximately(
        float actual,
        float expected,
        float tolerance,
        string testName)
    {
        float error = Mathf.Abs(actual - expected);

        if (error > tolerance)
        {
            GD.PrintErr(
                $"FAIL: {testName} | Expected: {expected} | Actual: {actual} | Error: {error}"
            );

            throw new System.Exception($"Test failed: {testName}");
        }

        GD.Print(
            $"PASS: {testName} | Expected: {expected} | Actual: {actual}"
        );
    }
}