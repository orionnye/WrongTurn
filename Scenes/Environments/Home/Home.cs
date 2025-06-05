using Godot;
using System;

public partial class Home : BaseScene
{
    public override void _Ready() {
        base._Ready();
    }

    public override void _Process(double delta) {
        base._Process(delta);
    }

    public override void _on_button_pressed() {
        GetParent<Game>().changeSceneTo(GetParent<Game>().railsScene);
    }
}
