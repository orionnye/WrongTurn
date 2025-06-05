using Godot;
using System;

public partial class Garage : Node3D {

    // This class will help me control the Users Vehicle
    // I'm going to need to export some value controls for the User
    // Values like, guncount. Ideally a universal way to control and place items
    // UserCount
    [Export] public int CarCount;
    // [Export] public Car car;
    
    public override void _Ready() {
    }
}
