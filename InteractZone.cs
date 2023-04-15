using Godot;
using System;

public partial class InteractZone : Area3D
{
	public Sprite2D baseCursor;
	public Sprite2D interactCursor;
	
	public override void _Ready(){
		baseCursor = GetNode<Sprite2D>("../../../CanvasLayer/Node2D/BaseCursor");
		interactCursor = GetNode<Sprite2D>("../../../CanvasLayer/Node2D/InteractCursor");
	}
	
	// When object enters sight sensor
	private void _on_body_entered(Node3D body)
	{
		baseCursor.Visible = false;
		interactCursor.Visible = true;
	}	
	
	// When object exits sight sensor
	private void _on_body_exited(Node3D body)
	{
		baseCursor.Visible = true;
		interactCursor.Visible = false;
	}
		
}






