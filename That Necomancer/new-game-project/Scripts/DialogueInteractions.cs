using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;


public partial class DialogueInteractions : Node2D
{
	/*
	All dialogue interactable is in this scripts
	Dialogue can remove items
	
	NPCs pull from master dictionary and parse their lines into their own personal 
	dictionary

	Have a range limit
	Have a text box
	*/
	[Export] string sceneDialogue;
	Dictionary<int, string> dialogueDic;
	[Export] int npcId;
	bool isHovered = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		dialogueDic = [];
		ReloadDialogue();
	}

	public void ReloadDialogue()
	{
		var startAt = 0;
		//sceneDialogue[2] is always the first key for the dialogue

		string[] masterTextFile = File.ReadAllLines("Levels/Level0/TestSkeletonDialogue.txt");

		GD.Print(Name + " got line: " + masterTextFile[npcId].ToString());
		sceneDialogue = masterTextFile[npcId];

		if (sceneDialogue[0].ToString() == npcId.ToString())
		{

			//Single digit ID
			GD.PrintRich("[color=green]" + Name + " succeeded to match line to ID!");
			startAt = 2;

		}
		else if ((npcId >= 10) && ((sceneDialogue[0].ToString() + sceneDialogue[1].ToString()) == npcId.ToString()))
		{

			//multi-digit ID (no more than 100)
			GD.PrintRich("[color=green]" + Name + " succeeded to match line to ID! (Was greater than or equal to 10)");
			startAt = 3;

		}
		else
		{

			GD.PrintErr(Name + " failed to match line to ID.");

		}


		int curKey = 0;
		string curLine = "";
		for (int i = startAt; i < sceneDialogue.Length; i++)
		{

			curLine = string.Concat(curLine, sceneDialogue[i]);

			//GD.Print(curLine + " CurKey: " + curKey);

			if (sceneDialogue[i].ToString() == ":")
			{
				dialogueDic.Add(curKey, curLine.Replace(":", ""));
				curKey++;
				curLine = "";
			}

		}

		dialogueDic.Add(curKey, curLine);


		foreach (var ele in dialogueDic)
		{
			GD.Print(ele);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public override void _Input(InputEvent ev)
	{
		if (ev is InputEventMouseButton eventMouseButton && eventMouseButton.Pressed)
		{
			//Interaction on click
			
			if (isHovered)
			{
				//GD.Print("Hey");
				//GD.Print(npcId);
				/*
				foreach (var ele in dialogueDic)
				{
					GD.Print(ele);
				}
				*/
				ReloadDialogue();
			}
			//ReloadDialogue();
		}
	}

	public void _on_interact_area_mouse_entered()
	{
		isHovered = true;
		//GD.Print(isHovered + " Object: "+Name+ " ID: "+npcId);

	}

	public void _on_interact_area_mouse_exited()
	{
		isHovered = false;
		//GD.Print(isHovered);
	}
}

	
