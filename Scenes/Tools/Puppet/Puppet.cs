using Godot;
using System;

public partial class Puppet : Node3D {
    [Export] public float MoveSpeed = 10.0f;
    [Export] public RigidBody3D body;
    [Export] public Vector3 Offset = new Vector3(0, 0, -1);
    [Export] public float deadzone = 0.1f;
    [Export] public float lerpRate = 0.1f;
    [Export] public float maxDistance = 5.0f; // Maximum allowed distance from body

    public override void _Ready() {
        // body = GetNode<RigidBody3D>("Body");
    }
    public Vector3 GetInput() {
        Vector3 direction = Vector3.Zero;

        if (Input.IsActionPressed("w"))
            direction.Z -= 1;
        if (Input.IsActionPressed("s"))
            direction.Z += 1;
        if (Input.IsActionPressed("a"))
            direction.X -= 1;
        if (Input.IsActionPressed("d"))
            direction.X += 1;
            
        return direction;
    }

    public override void _Process(double delta)
    {
        // Input Handling
        Vector3 direction = GetInput();
        float speed = MoveSpeed;
        if (Input.IsActionPressed("LeftShift"))
            speed *= 2.0f;

        // Motion Calls, could be broken out into a separate function
        if (direction != Vector3.Zero)
        {
            direction = direction.Normalized();
            GlobalTranslate(direction * speed * (float)delta);
        }
        // Moves the puppetBody
        // MoveBody();
        PullBody();
        CapDistanceFromBody();
    }

    public void MoveBody() {
        if (body != null) {
            // Calculate the target position
            Vector3 targetPosition = GlobalPosition + Offset;
            // Lerp the body's position toward the target
            body.GlobalPosition = body.GlobalPosition.Lerp(targetPosition, lerpRate);
        }
    }

    public void PullBody() {
        if (body != null) {
            // Calculate the target position
            Vector3 targetPosition = GlobalPosition + Offset;
            Vector3 toTarget = targetPosition - body.GlobalPosition;

            // If within deadzone, stop movement
            if (toTarget.Length() < deadzone) {
                body.LinearVelocity = Vector3.Zero;
                return;
            }

            // Calculate desired velocity (like a lerp, but using physics)
            Vector3 desiredVelocity = toTarget / lerpRate; // lerpRate controls "snappiness"
            Vector3 velocityChange = desiredVelocity - body.LinearVelocity;

            // Use friction if available, otherwise default to 1.0f
            float friction = body.PhysicsMaterialOverride != null ? body.PhysicsMaterialOverride.Friction : 1.0f;

            // Apply force to reach the desired velocity
            body.ApplyCentralImpulse(velocityChange * body.Mass * friction);

            // Optional: Clamp velocity to prevent overshooting
            if (body.LinearVelocity.Length() > desiredVelocity.Length()) {
                body.LinearVelocity = desiredVelocity;
            }
        }
    }

    public void CapDistanceFromBody()
    {
        if (body == null)
            return;

        Vector3 desiredPosition = body.GlobalPosition + Offset;
        Vector3 toDesired = GlobalPosition - desiredPosition;
        float distance = toDesired.Length();

        if (distance > maxDistance)
        {
            // Clamp the puppet's position to the max distance from the body (keeping the offset direction)
            Vector3 clampedPosition = desiredPosition + toDesired.Normalized() * maxDistance;
            GlobalPosition = clampedPosition;
        }
    }
}
