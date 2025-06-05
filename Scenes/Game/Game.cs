using Godot;
using System;
using System.Collections.ObjectModel;
using System.Linq;

public partial class Game : Node3D
{
    [Export] public PackedScene home;
    [Export] public PackedScene shop;
    [Export] public PackedScene rails;

    public Home homeScene;
    public Shop shopScene;
    public Rails railsScene;
    private Node3D currentScene;

    public User user = new User();
    public override void _Ready() {
        base._Ready();
        // create a new User
        homeScene = home.Instantiate<Home>();
        shopScene = shop.Instantiate<Shop>();
        railsScene = rails.Instantiate<Rails>();

        AddChild(homeScene);
        currentScene = homeScene;
    }
    public override void _Process(double delta) {
        base._Process(delta);
    }

    public void changeSceneTo(Node3D scene) {
        user.UserScore += 10;
        RemoveChild(currentScene);
        AddChild(scene);
        currentScene = scene;
        // GetTree().ReloadCurrentScene();
        // currentScene.QueueFree();
        // currentScene = scene.Instantiate<Node3D>();
    }
    public void _on_button_pressed() {
        if (currentScene == homeScene) {
            changeSceneTo(railsScene);
        } else if (currentScene == shopScene) {
            changeSceneTo(homeScene);
        } else if (currentScene == railsScene) {
            changeSceneTo(shopScene);
        }
    }
}
