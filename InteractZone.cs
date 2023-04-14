using Godot;
using System;

public partial class InteractZone : Area3D
{
	public Sprite2D baseCursor;
	public Sprite2D interactCursor;
	
	public override void _Ready(){
		baseCursor = GetNode<Sprite2D>("./CanvasLayer/BaseCursor");
		interactCursor = GetNode<Sprite2D>("./CanvasLayer/InteractCursor");
		baseCursor.Visible = false;
	}
	
	public void OnEnter(Node Other){
		
	}
}
