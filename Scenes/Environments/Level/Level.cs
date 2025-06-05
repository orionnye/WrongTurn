using Godot;
using System;

public partial class Level : Node3D
{
    [Export] public SpeedoMeter speedoMeter;
    private bool _hasStarted = false;
    private float _reloadTimer = 0f;
    private const float RELOAD_DELAY = 0.5f; // Wait half a second before reloading

    public override void _Ready() {
        base._Ready();
        // Give the speedometer time to initialize before checking values
        _hasStarted = false;
    }

    public override void _Process(double delta) {
        base._Process(delta);
        // Don't check the speedometer for the first few frames
        if (!_hasStarted) {
            _hasStarted = true;
            return;
        }
        
        // Null check the speedoMeter and its progressBar
        if (speedoMeter?.progressBar != null) {
            if (speedoMeter.progressBar.Value <= 0) {
                // Add a small delay before reloading to prevent infinite reload loops
                _reloadTimer += (float)delta;
                if (_reloadTimer >= RELOAD_DELAY) {
                    GetTree().ReloadCurrentScene();
                }
            } else {
                // Reset timer when value is not zero
                _reloadTimer = 0f;
            }
        }
    }
}
