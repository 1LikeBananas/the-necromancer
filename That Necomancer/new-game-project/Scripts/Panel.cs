using Godot;
using System;

public partial class Panel : Godot.Panel
{
	//GetChild starts at 0

	ButtonGroup[] button;



	public override void _Ready()
	{


	}


	public override void _Process(double delta)
	{
	}

	private void _on_start_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Levels/test_level.tscn");
	}

	private void _on_options_button_pressed()
	{
		
	}

	private void _on_exit_button_pressed()
	{
		GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
		GetTree().Quit();
	}
}
