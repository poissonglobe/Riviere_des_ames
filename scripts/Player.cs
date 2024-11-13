using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [Export]
    public int Speed { get; set; } = 10;

    [Export]
    public float JumpHeight { get; set; } = 2.5f;

    [Export]
    public int FallAcceleration { get; set; } = 75;
    
    [Export]
    public Vector3 _targetVelocity = Vector3.Zero;

    public Node3D camera;

    public override void _Ready()
    {
        GetNode<AnimationTree>("AnimationTree").Active = true;
    }

    public override void _PhysicsProcess(double delta)
    {

        camera = GetNode<Node3D>("CameraRoot/Camera");
        Vector3 direction = Vector3.Zero;

        // Input direction Management
        Vector2 inputVector = Input.GetVector("move_left", "move_right", "move_back", "move_forward").Normalized();
        direction = new Vector3(-inputVector.X, 0, inputVector.Y);


        if (Input.IsActionPressed("jump") && IsOnFloor())
        {
            _targetVelocity.Y = JumpHeight * Speed;
        
        }
        else if(IsOnFloor())
        {
            _targetVelocity.Y = 0;
        }

        // Normalising direction

        if(direction != Vector3.Zero)
        {
            // make the direction of the deplacement depend of the camera and not the character axis
            direction = camera.GlobalTransform.Basis * direction; 
            direction.Y = 0;
            direction = direction.Normalized();

            //update the direction of the player depending of the direction
            GetNode<Node3D>("Pivot").Basis = Basis.LookingAt(-direction);
        }

        _targetVelocity.X = direction.X * Speed;
        _targetVelocity.Z = direction.Z * Speed;


        // Vertical velocity
        if (!IsOnFloor())
        {
            _targetVelocity.Y = Velocity.Y;
            _targetVelocity.Y -= FallAcceleration * (float)delta; 
        }

        // Moving the character
        Velocity = _targetVelocity;
        MoveAndSlide();
    }
}
