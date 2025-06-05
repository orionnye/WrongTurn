using Godot;
using System;
using System.Collections.Generic;

public partial class Spawner : Node3D
{
    [Export] public PackedScene sceneToSpawn;
    [Export] public Timer spawnTimer;
    [Export] public int spawnCount = 1;
    [Export] public Vector3 spawnDim = new Vector3(0, 0, 0);
    [Export] public float maxDistance = 60.0f; // Maximum distance objects can travel before being removed
    
    private List<Node3D> spawnedObjects = new List<Node3D>();

    public override void _Ready()
    {
        GD.Print("Spawner Ready");
        if (spawnTimer != null)
            spawnTimer.Timeout += on_timer_timeout;
        spawnTimer.Start();
        // SpawnObjects();
    }
    
    public override void _Process(double delta)
    {
        // Check each spawned object's distance from spawner
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            Node3D obj = spawnedObjects[i];
            
            // Skip if object was already freed or isn't valid
            if (!IsInstanceValid(obj))
            {
                spawnedObjects.RemoveAt(i);
                continue;
            }
            
            // Calculate distance between spawner and object
            float distance = GlobalPosition.DistanceTo(obj.GlobalPosition);
            
            // If distance exceeds max, remove the object
            if (distance > maxDistance)
            {
                obj.QueueFree();
                spawnedObjects.RemoveAt(i);
                // GD.Print("Object removed from list");
            }
        }
    }
    
    public void SpawnObjects() {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Mathf.Lerp(-spawnDim.X / 2, spawnDim.X / 2, GD.Randf()),
                Mathf.Lerp(-spawnDim.Y / 2, spawnDim.Y / 2, GD.Randf()),
                Mathf.Lerp(-spawnDim.Z / 2, spawnDim.Z / 2, GD.Randf())
            );
            // GD.Print("Spawn Position: ", spawnPosition);

            Node3D instance = sceneToSpawn.Instantiate<Node3D>();
            AddChild(instance);
            instance.GlobalPosition = this.GlobalPosition + spawnPosition;
            
            // Add to tracked objects list
            spawnedObjects.Add(instance);
        }
    }
    
    public void on_timer_timeout() {
        // GD.Print("Timer Timeout");
        SpawnObjects();
    }
}
