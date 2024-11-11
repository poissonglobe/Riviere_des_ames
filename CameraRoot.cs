using Godot;
using System;

public partial class CameraRoot : Node3D
{
    private Vector3 cameraRot = Vector3.Zero;

    [Export]
    public float CameraSpeed { get; set; } = 0.5f; // Sensitivity for camera movement

    [Export]
    public float VerticalAngleLimit { get; set; } = 45.0f; // Limit for vertical rotation

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseEvent)
        {
            // Adjust horizontal and vertical rotation based on mouse movement
            cameraRot.Y -= mouseEvent.Relative.X * CameraSpeed; // Horizontal rotation 
            cameraRot.X -= mouseEvent.Relative.Y * CameraSpeed; // Vertical rotation 

            // Clamp the vertical angle to the specified limit
            cameraRot.X = Mathf.Clamp(cameraRot.X, -VerticalAngleLimit, VerticalAngleLimit);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
            // Apply the rotation to the node
            RotationDegrees = new Vector3(cameraRot.X, cameraRot.Y, 0);

    }
}
