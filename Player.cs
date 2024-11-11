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
    
    private Vector3 _targetVelocity = Vector3.Zero;

    public override void _PhysicsProcess(double delta)
    {

        var direction = Vector3.Zero;

        // Input direction Management

        if (Input.IsActionPressed("move_forward"))
        {
            direction.Z = 1.0f;
        }
        if (Input.IsActionPressed("move_left"))
        {
            direction.X = 1.0f;
        }
        if (Input.IsActionPressed("move_back"))
        {
            direction.Z = -1.0f;
        }
        if (Input.IsActionPressed("move_right"))
        {
            direction.X = -1.0f;
        }
        if (Input.IsActionPressed("jump") && IsOnFloor())
        {
            _targetVelocity.Y = JumpHeight * Speed;
        
        }
        


        // Normalising direction

        if(direction != Vector3.Zero)
        {
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
