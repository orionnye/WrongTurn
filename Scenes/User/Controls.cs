using Godot;
using System;

public partial class Controls : RigidBody3D
{
    // [Export] public ProgressBar fuelMeter;
    [Export] public float fuel = 500;
    [Export] public float fuelMax = 500;
    [Export] public float force = 100;
    [Export] public float decay = 0.9f;

    [Export] public Node3D inputNode;
    public bool isActive = true;
    // private Vector3 userAcceleration = Vector3.Zero;

    public override void _Ready() {
        base._Ready();
        fuel = fuelMax;
        // Removed dependency on Game node. Force and decay are set via Inspector or other means.
    }
    private Vector3 userInput() {
        Vector3 input = Vector3.Zero;
        if (isActive) {
            if (Input.IsActionPressed("w")) {
                // This check shouldn't be needed here. Fuel checks are an extra layer.
                if (fuel > 0) {
                    input.Z -= 1.0f;
                }
            }
            if (Input.IsActionPressed("s")) {
                input.Z += 1.0f;
            }
            if (Input.IsActionPressed("d")) {
                input.X += 1.0f;
            }
            if (Input.IsActionPressed("a")) {
                input.X -= 1.0f;
            }
        }
        return input;
    }
    private void displayTool() {
        // Use this function to perform visual effects or mutations to the user that don't actually 
        // affect gameplay
        
        // // first, we want the user to rotate to face its -Y side to face
        // // any nearby nodes in the "ramp" group
        // var rampNodes = GetTree().GetNodesInGroup("Ramp");
        // if (rampNodes.Count > 0) {
        //     foreach (Node node in rampNodes) {
        //         if (node is Node3D rampNode) {
        //             Vector3 directionToRamp = (rampNode.GlobalPosition - GlobalPosition).Normalized();
        //             LookAt(rampNode.GlobalPosition, Vector3.Up);
        //             break; // Exit after finding the first ramp node
        //         }
        //     }
        // }
        // This should be finished at a later date. We need to focus on the state machine
    }

    public override void _Process(double delta) {
        base._Process(delta);
        // if (Input.IsActionPressed("w")) {
        //     fuel -= 0.1f;
        // }
        if (fuel > 0 && Input.IsActionPressed("w")) {

        }
        inputNode.Position = inputNode.Position.Lerp(userInput(), 0.1f);
    }
    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        // Apply the force to the RigidBody3D
        Vector3 appliedForce = userInput() * force;
        // GD.Print("Applied Force: ", appliedForce);
        ApplyCentralForce(appliedForce);

        // Apply passive decay
        LinearVelocity *= decay;
    }
}
