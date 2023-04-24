using Godot;
using System;
using System.Collections.Generic;

public partial class dialoguePlayer : CanvasLayer
{
	[Export(PropertyHint.GlobalFile, "*.txt")]
	public string dialogueFile;
	
	public List<List<string>> dialogues;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		play();
	}
	
	public void play(){
		loadDialogue();
		List<string> currentBlock = dialogues[0];
		GetNode<RichTextLabel>("Panel/Name").Text = currentBlock[0];
		currentBlock.RemoveAt(0);
		GetNode<RichTextLabel>("Panel/Message").Text = currentBlock[0];
		currentBlock.RemoveAt(0);
	}
	
	public void playNext(){
		List<string> currentBlock = dialogues[0];
		if (currentBlock.Count == 0){
			dialogues.RemoveAt(0);
			if(dialogues.Count == 0){
				((Appliance)GetNode<StaticBody3D>("../")).endInteract();
			}
			else{
				currentBlock = dialogues[0];
				GetNode<RichTextLabel>("Panel/Name").Text = currentBlock[0];
				currentBlock.RemoveAt(0);
				GetNode<RichTextLabel>("Panel/Message").Text = currentBlock[0];
				currentBlock.RemoveAt(0);
			}

		}
		else{
			GetNode<RichTextLabel>("Panel/Message").Text = currentBlock[0];
			currentBlock.RemoveAt(0);
		}
	}
	
	public void loadDialogue(){
		dialogues = new List<List<string>>();
		using var file = FileAccess.Open(dialogueFile, FileAccess.ModeFlags.Read);
		if (file != null){
			var data = file.GetAsText(true);
			string[] blocks = data.Split("\n");
			foreach (string block in blocks){
				string[] lines = block.Split('~');
				List<string> temp = new List<string>();
				foreach (string line in lines) {
					temp.Add(line);
				}
				dialogues.Add(temp);
			}
		}
		file.Close();
	}


}
