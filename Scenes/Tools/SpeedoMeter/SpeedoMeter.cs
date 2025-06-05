using Godot;
using System;

public partial class SpeedoMeter : Control
{
    [Export] public Node3D minNode;
    [Export] public Node3D maxNode;
    [Export] public Node3D hostNode;
    [Export] public ProgressBar progressBar;

    public override void _Ready() {
        GD.Print("SpeedoMeter _Ready called");
        if (progressBar == null) {
            GD.PrintErr("ProgressBar not assigned to SpeedoMeter!");
            return;
        }
        if (minNode == null || maxNode == null) {
            GD.PrintErr("Min or Max nodes not assigned to SpeedoMeter!");
            return;
        }
        GD.Print("SpeedoMeter ready");
        progressBar.MinValue = 0;
        progressBar.MaxValue = 1; // Setting max to 100 to display percentage
    }

    public float GetRelativeValue(float value) {
        float minPos = minNode.GlobalPosition.Z;
        float maxPos = maxNode.GlobalPosition.Z;
        
        // Handle case where nodes might be flipped (min > max)
        float actualMin = Mathf.Min(minPos, maxPos);
        float actualMax = Mathf.Max(minPos, maxPos);
        float range = actualMax - actualMin;
        
        // Prevent division by zero
        if (range <= 0) return 0;
        
        // Map the input value to a 0-1 range based on min/max
        float result = (value - actualMin) / range;
        
        // Clamp the result between 0 and 1
        result = Mathf.Clamp(result, 0, 1);
        
        // If min/max nodes are flipped, we need to invert the result
        if (minPos > maxPos) {
            result = 1 - result;
        }
        
        // GD.Print($"Value: {value}, Min: {minPos}, Max: {maxPos}, Result: {result}");
        return result;
    }

    public override void _Process(double delta) {
        if (hostNode == null || progressBar == null || minNode == null || maxNode == null) return;

        // Get the position value
        float value = hostNode.GlobalPosition.Z;
        // Get the relative value between 0 and 1
        float relativeValue = GetRelativeValue(value);
        // Set the value of the progressBar (0-100 range for percentage display)
        progressBar.Value = relativeValue;
    }
}
