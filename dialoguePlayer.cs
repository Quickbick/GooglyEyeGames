using Godot;
using System;

public partial class dialoguePlayer : CanvasLayer
{
	[Export(PropertyHint.GlobalFile, "*.json")]
	public string dialogueFile;
	
	Godot.Collections.Array dialogues = new Godot.Collections.Array();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		play();
	}
	
	public void play(){
		dialogues = loadDialogue();
	}
	
	public Godot.Collections.Array loadDialogue(){
		using var file = FileAccess.Open(dialogueFile, FileAccess.ModeFlags.Read);
		if (file != null){
			var data = Json.ParseString(file.GetAsText());
				return (Godot.Collections.Array)data;			
		}
		else{ return null; }
	}


}
