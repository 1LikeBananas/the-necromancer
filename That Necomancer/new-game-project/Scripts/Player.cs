using Godot;
using System;
using System.Net;

public partial class Player : CharacterBody2D
{

	Vector2 velocity;

	[Export] float speed = 150;

	[Export] Camera2D cameraNode;

	[Export] Vector2 targetPos;

	bool playerCanMove = true;

	NavigationAgent2D navNode;

	float camSpeed = 3;

	public override void _Ready()
	{
		navNode = (NavigationAgent2D)GetNode("NavigationAgent2D");
		//GD.Print(navNode.Name);
		navNode.TargetPosition = targetPos;
	}

    public override void _Process(double delta)
    {
		if (Input.GetVector("Left", "Right", "Up", "Down") != Vector2.Zero)
		{
			playerCanMove = true;
	   	}
    }

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
		if ((playerCanMove == true) && (direction != Vector2.Zero))
		{
			//GD.Print(direction);

			velocity = direction * speed;

			Velocity = velocity;
			MoveAndSlide();
		}
		else { velocity = Vector2.Zero; }
		if (playerCanMove == false)
		{
			if (navNode.IsTargetReached() == false)
			{
				var navPointDirection = ToLocal(navNode.GetNextPathPosition()).Normalized();
				Velocity = navPointDirection * speed;
				MoveAndSlide();
			}
			
		}

		//cameraNode.Position = Position;


	}

	public override void _Input(InputEvent ev)
	{
		if (ev is InputEventMouseButton eventMouseButton && eventMouseButton.Pressed)
		{
			//Navigation Code

			targetPos = GetGlobalMousePosition();
			navNode.TargetPosition = targetPos;
			/*GD.Print(eventMouseButton.GlobalPosition);
			GD.Print(targetPos);
			GD.Print(Position);*/
			playerCanMove = false;
		}


	}
	}

