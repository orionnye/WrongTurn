using Godot;
using System;

public partial class Ui : Control
{
    [Export] public ProgressBar fuelMeter;
    
    // This should be referencing the data store for the user
    public Controls host;
    public override void _Ready() {
        base._Ready();
        // MANUAL ASSIGNMENT OF HOST
        host = GetParent<Controls>();
        // Set the min and max values for the fuel meter
        fuelMeter.MinValue = 0;
        fuelMeter.MaxValue = 1;
    }

    public void SetFuel() {
        float max = host.fuelMax;
        float value = host.fuel / max;
        fuelMeter.Value = value;
    }

    public override void _Process(double delta) {
        base._Process(delta);
        SetFuel();
    }
}
