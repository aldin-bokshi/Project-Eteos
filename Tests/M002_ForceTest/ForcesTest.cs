using Godot;
using ProjectEteos.Scripts.Objects;
using ProjectEteos.Scripts.Physics;

namespace ProjectEteos.Tests.M002_ForceTest;

public class ForceTest
{
    private const float Tolerance = 0.0001f;

    public void Run()
    {
        GD.Print("=== M002 Force Tests ===");

        TestSingleForce();
        TestMultipleForces();
        TestOpposingForces();
        TestForceAccumulatorResets();
        TestDifferentMasses();
        TestSingleImpulse();
        TestImpulseVsForce();

        GD.Print("=== ALL FORCE TESTS PASSED ===");
    }

    private void TestSingleForce()
    {
        var body = CreateBody(2.0f);
        var physics = new PhysicsSystem();

        physics.Gravity = Vector3.Zero;

        physics.ApplyForce(body, new Vector3(10, 0, 0));

        physics.Step(body, 1.0f);

            // What should the velocity be after 10 N acts
            // on a 2 kg object for 1 second?
        float expected = 5.0f; // 10 N·s / 2 kg = 5 m/s
        float actual = body.Velocity.X;

        AssertApproximately(actual, expected, "Single force");
    }

    private void TestMultipleForces()
    {
        var body = CreateBody(2.0f);
        var physics = new PhysicsSystem();

        physics.Gravity = Vector3.Zero;

        physics.ApplyForce(body, new Vector3(10, 0, 0));
        physics.ApplyForce(body, new Vector3(6, 0, 0));

        physics.Step(body, 1.0f);

        float expected = 8.0f; // (10N + 6N) / 2kg * 1s = 8m/s
        float actual = body.Velocity.X;

        AssertApproximately(actual, expected, "Multiple forces");
    }

    private void TestOpposingForces()
    {
        var body = CreateBody(2.0f);
        var physics = new PhysicsSystem();

        physics.Gravity = Vector3.Zero;

        physics.ApplyForce(body, new Vector3(10, 0, 0));
        physics.ApplyForce(body, new Vector3(-10, 0, 0));

        physics.Step(body, 1.0f);

        float expected = 0.0f; // Opposing forces cancel out
        float actual = body.Velocity.X;

        AssertApproximately(actual, expected, "Opposing forces");
    }

    private void TestForceAccumulatorResets()
    {
        var body = CreateBody(2.0f);
        var physics = new PhysicsSystem();

        physics.Gravity = Vector3.Zero;

        physics.ApplyForce(body, new Vector3(10, 0, 0));

        physics.Step(body, 1.0f);

        float velocityAfterFirstStep = body.Velocity.X;

        // Don't apply another force.
        physics.Step(body, 1.0f);

        float velocityAfterSecondStep = body.Velocity.X;

        // What should happen if the force was correctly cleared?
        AssertApproximately(
            velocityAfterSecondStep,
            velocityAfterFirstStep,
            "Force accumulator reset"
        );
    }

    private void TestDifferentMasses()
    {
        var lightBody = CreateBody(2.0f);
        var heavyBody = CreateBody(10.0f);

        var physics = new PhysicsSystem();

        physics.Gravity = Vector3.Zero;

        physics.ApplyForce(lightBody, new Vector3(10, 0, 0));
        physics.ApplyForce(heavyBody, new Vector3(10, 0, 0));

        physics.Step(lightBody, 1.0f);
        physics.Step(heavyBody, 1.0f);

        // Same force, different mass → different acceleration.
        float expectedLight = 5.0f; // 10N / 2kg * 1s = 5m/s
        float expectedHeavy = 1.0f; // 10N / 10kg * 1s = 1m/s

        AssertApproximately(
            lightBody.Velocity.X,
            expectedLight,
            "Different masses - light"
        );

        AssertApproximately(
            heavyBody.Velocity.X,
            expectedHeavy,
            "Different masses - heavy"
        );
    }

    private void TestSingleImpulse()
    {
        var body = CreateBody(2.0f);
        var physics = new PhysicsSystem();

        physics.Gravity = Vector3.Zero;

        physics.ApplyImpulse(body, new Vector3(10, 0, 0));

        // Unlike a force, the impulse should change velocity immediately.
        float expected = 5.0f; // 10N * 1s / 2kg = 5m/s
        float actual = body.Velocity.X;

        AssertApproximately(actual, expected, "Single impulse");
    }

    private void TestImpulseVsForce()
    {
        var forceBody = CreateBody(2.0f);
        var impulseBody = CreateBody(2.0f);

        var physics = new PhysicsSystem();

        physics.Gravity = Vector3.Zero;

        physics.ApplyForce(forceBody, new Vector3(10, 0, 0));
        physics.ApplyImpulse(impulseBody, new Vector3(10, 0, 0));

        // At this point, the impulse body should have changed velocity,
        // while the force body should not have yet.
        AssertApproximately(
            forceBody.Velocity.X,
            0.0f,
            "Force is not instantaneous"
        );

        AssertApproximately(
            impulseBody.Velocity.X,
            5.0f,
            "Impulse is instantaneous"
        );
    }

    private static SimulationObject CreateBody(float mass)
    {
        return new SimulationObject
        {
            Mass = mass,
            Position = Vector3.Zero,
            Velocity = Vector3.Zero,
            AccumulatedForce = Vector3.Zero
        };
    }

    private static void AssertApproximately(
        float actual,
        float expected,
        string testName)
    {
        float error = Mathf.Abs(actual - expected);

        if (error > Tolerance)
        {
            GD.PrintErr($"FAIL: {testName} | Expected: {expected} | Actual: {actual} | Error: {error}");

            throw new System.Exception($"Test failed: {testName}");
        }

        GD.Print(
            $"PASS: {testName} | Expected: {expected} | Actual: {actual}"
        );
    }

    private void TestInvalidMass()
    {
        var physics = new PhysicsSystem();
        var body = CreateBody(0.0f);

        physics.Gravity = Vector3.Zero;

        try
        {
            physics.Step(body, 1.0f);

            throw new System.Exception(
                "Invalid mass test failed: zero-mass body was accepted."
            );
        }
        catch (System.ArgumentException)
        {
            GD.Print("PASS: Invalid mass is rejected");
        }
    }
}