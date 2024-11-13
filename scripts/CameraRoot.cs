using Godot;
using System;

public partial class CameraRoot : Node3D
{
    private Vector3 cameraRot = Vector3.Zero;

    [Export]
    public float MouseCameraSpeed { get; set; } = 0.5f; // Sensitivity for camera movement
    [Export]
    public float ControllerCameraSpeed { get; set; } = 125.0f; // Sensitivity for camera movement

    [Export]
    public float VerticalAngleLimit { get; set; } = 45.0f; // Limit for vertical rotation

    public override void _Input(InputEvent @event)
    {
        // Input direction Management for mouse

        if (@event is InputEventMouseMotion mouseEvent)
        {
            // Adjust horizontal and vertical rotation based on mouse movement
            cameraRot.Y -= mouseEvent.Relative.X * MouseCameraSpeed; // Horizontal rotation 
            cameraRot.X -= mouseEvent.Relative.Y * MouseCameraSpeed; // Vertical rotation 

            // Clamp the vertical angle to the specified limit
            
        }
        cameraRot.X = Mathf.Clamp(cameraRot.X, -27, 45);
    }

    public override void _PhysicsProcess(double delta)
    {

        // Input direction Management for controller
        Vector2 inputVector = Input.GetVector("camera_left", "camera_right", "camera_down", "camera_up").Normalized();
        cameraRot -= new Vector3( inputVector.Y * ControllerCameraSpeed * (float)delta, inputVector.X * ControllerCameraSpeed * (float)delta, 0);
        
        // Apply the rotation to the node
        RotationDegrees = new Vector3(cameraRot.X, cameraRot.Y, 0);

    }
}
