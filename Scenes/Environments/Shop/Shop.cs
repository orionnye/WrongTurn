using Godot;
using System;

public partial class Shop : BaseScene
{
    public override void _Ready() {
        base._Ready();
        GetNode<Controls>("User").GetNode<GpuParticles3D>("GPUParticles3D").Emitting = false;
    }

    public override void _Process(double delta) {
        base._Process(delta);
    }

    public override void _on_button_pressed() {
        GetParent<Game>().user.UserScore -= 20;
        GetParent<Game>().changeSceneTo(GetParent<Game>().homeScene);
    }
    public void _on_button_2_pressed() {
        GetParent<Game>().user.UserForce += 10;
        GD.Print("Force: ", GetParent<Game>().user.UserForce);
    }
}
