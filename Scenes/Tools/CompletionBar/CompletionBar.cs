using Godot;
using System;

public partial class CompletionBar : Control
{
    [Export] public ProgressBar progressBar;
    [Export] public SpeedoMeter speedoMeter;
    private float updateDelay = 1f;
    [Export] public float delayOffset = 1f;
    [Export] public float incrementValue = 0.1f;

    private float _timer = 0f;

    public void updateBar(float value) {
        progressBar.Value = value;
    }

    public override void _Ready() {
        // This class will function as a timer, that speeds up with the speedometer
        base._Ready();
        updateDelay = delayOffset;
    }

    public override void _Process(double delta) {
        base._Process(delta);

        _timer += (float)delta;
        if (_timer >= updateDelay) {
            _timer = 0f;
            if (speedoMeter != null) {

                updateDelay = delayOffset - (delayOffset * (float)speedoMeter.progressBar.Value);
                GD.Print("Speedometer Value: ", speedoMeter.progressBar.Value);
                GD.Print("Update Delay: ", updateDelay);
            }
            updateBar((float)progressBar.Value + incrementValue);
        }
    }
}
