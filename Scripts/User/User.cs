using Godot;

public partial class User : Node
{
    // Example user properties
    public string UserName { get; set; }
    public int UserScore { get; set; }
    public float UserForce { get; set; }
    public float UserDecay { get; set; }

    // Constructor
    public User() {
        // Initialize default values
        UserName = "Guest";
        UserScore = 0;
        UserForce = 50;
        UserDecay = 0.9f;
    }

    // Method to reset user data
    public void ResetUserData() {
        UserName = "Guest";
        UserScore = 0;
    }
}
