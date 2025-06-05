using Godot;
using System;

public partial class Asteroid : RigidBody3D
{
    [Export] public Vector3 impulseDir = new Vector3(0, 0, -1);
    [Export] public float impulseStrength = 10.0f;
    [Export] public float spinScale = 1.0f;

    public override void _Ready() {
        // Apply an impulse to the asteroid in the specified direction
        applyForce();
        randRot();
        randSpin();
    }
    
    public void applyForce()
    {
        // Apply a force to the asteroid in the specified direction
        Vector3 force = impulseDir.Normalized() * impulseStrength;
        ApplyCentralImpulse(force);
    }
    
    public void randRot()
    {
        // Apply a random rotation to the asteroid
        Vector3 rotation = new Vector3(
            GD.Randf() * Mathf.Pi,
            GD.Randf() * Mathf.Pi,
            GD.Randf() * Mathf.Pi
        );
        Rotation = rotation;
    }
    
    public void randSpin()
    {
        // Apply a random spin to the asteroid in both directions
        Vector3 spin = new Vector3(
            (float)(GD.RandRange(-1.0f, 1.0f) * spinScale),
            (float)(GD.RandRange(-1.0f, 1.0f) * spinScale),
            (float)(GD.RandRange(-1.0f, 1.0f) * spinScale)
        );
        ApplyTorqueImpulse(spin);
    }
}
