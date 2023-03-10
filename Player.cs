using Godot;
using System;

public class Player : KinematicBody
{
	// Declare member variables here. Examples:
	[Export]
	public int speed = 14;
	
	[Export]
	public int fallAcceleration = 75;
	
	private Vector3 velocity = Vector3.Zero;
	
	//Moves body based on input
	public override void _PhysicsProcess(float delta)
	{
		var direction = Vector3.Zero;
		
		if (Input.IsActionPressed("move_right")){
			direction.x += 1f;
		}
		if (Input.IsActionPressed("move_left")){
			direction.x -= 1f;
		}	
		if (Input.IsActionPressed("move_back")){
			direction.z += 1f;
		}	
		if (Input.IsActionPressed("move_forward")){
			direction.z -= 1f;
		}	
		
		//normalizes vector if greater than 0 for consistent movement
		if(direction != Vector3.Zero){
			direction = direction.Normalized();
			GetNode<Spatial>("Spatial").LookAt(Translation + direction, Vector3.Up);
		}
		
		//Ground Velocity
		velocity.x = direction.x * speed;
		velocity.z = direction.z * speed;
		//Vertical Velocity
		velocity.y -= fallAcceleration * delta;
		//Moving the Character
		velocity = MoveAndSlide(velocity, Vector3.Up);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
