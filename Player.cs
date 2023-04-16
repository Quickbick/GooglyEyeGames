using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	public Node3D neck;
	public Camera3D camera;
	public InteractZone interactZone;
	
	public override void _Ready()
	{
		base._Ready();
		Input.MouseMode = Input.MouseModeEnum.Captured;
		neck = GetNode<Node3D>("Neck");
		camera = GetNode<Camera3D>("Neck/Camera3D");
		interactZone = (InteractZone)GetNode<Area3D>("Neck/Camera3D/InteractZone");
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouse){
			neck.RotateY(-mouse.Relative.X * (float)0.01);
			camera.RotateX(-mouse.Relative.Y * (float)0.01);
			Vector3 ClampMe = camera.Rotation;
			ClampMe.X = Mathf.Clamp(camera.Rotation.X, (float)-0.523599, (float)1.0472);
			camera.Rotation = ClampMe;
		}
		//add else if for controller inputs?
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = JumpVelocity;
			
		if (Input.IsActionJustPressed("interact") && interactZone.currentObject != null){
			((Appliance)interactZone.currentObject).interact();
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_back");
		Vector3 direction = (neck.Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
