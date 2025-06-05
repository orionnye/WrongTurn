using Godot;
using System;

public partial class Rails : BaseScene
{
    public override void _Ready() {
        base._Ready();
        // RAILS does not currently reset on scene change, this scene and the shop should reset
    }

    public override void _Process(double delta) {
        base._Process(delta);
    }

    public void _on_button_pressed() {
        GetTree().Root.GetNode<Game>("Game").changeSceneTo(GetTree().Root.GetNode<Game>("Game").shopScene);
        // GetTree().Root.GetNode<Game>("Game").user.UserScore += 10;
    }
    public void _on_tree_entered() {
        // Removed: GetNode<Controls>("User").isActive = true;
        // Removed: GetNode<Controls>("User").GetNode<GpuParticles3D>("GPUParticles3D").Emitting = true;
        // Removed: GetNode<Controls>("User").Position = new Vector3(0, 0, 0);
    }
}
