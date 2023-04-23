using Godot;
using System;

public partial class Appliance : Godot.StaticBody3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void startInteract(){
		GetNode<CanvasLayer>("CanvasLayer").Visible = true;
	}
	
	public void contInteract(){
		((dialoguePlayer)GetNode<CanvasLayer>("CanvasLayer")).playNext();
	}
	
	public void endInteract(){
		GetNode<CanvasLayer>("CanvasLayer").Visible = false;
	}
}
