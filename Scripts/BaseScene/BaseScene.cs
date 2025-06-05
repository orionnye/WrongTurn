using Godot;
using System;

public partial class BaseScene : Node3D
{
    [Export] public Button button;

    public override void _Ready() {
        base._Ready();
        if (button != null) {
            button.Pressed += _on_button_pressed;
        }
    }

    public override void _Process(double delta) {
        base._Process(delta);
    }

    public virtual void _on_button_pressed() {
        GetParent<Game>().changeSceneTo(GetParent<Game>().homeScene);
    }
}
