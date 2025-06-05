using Godot;
using System;

public partial class NodeCloud : Node3D
{
    [Export] public Vector3 RotationRange;
    [Export] public Vector3 ScaleRange;
    [Export] public Vector3 PositionRange;
    [Export] public int NodeCount;
    [Export] public PackedScene Node;

    [Export] public float timerMax;
    [Export] public float timer;
    public override void _Ready() {
        timer = timerMax;
    }
    public Node3D CreateNode(Vector3 position) {
        var node = Node.Instantiate<Node3D>();
        node.Position = position;
        AddChild(node);
        return node;
    }
    

    public override void _Process(double delta) {
        timer -= (float)delta;
        if (timer <= 0) {
            timer = timerMax;
            CreateNode(new Vector3(0, 0, 0));
        }
    }
}
