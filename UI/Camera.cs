using Godot;
using System;

public partial class Camera : Camera2D
{

	public Vector2 initialPos;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		initialPos = this.Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		int speed = 5;

		if (Input.IsActionPressed("Shift"))
		{
			speed *= 2;
		}

		if (Input.IsActionPressed("Up"))
		{
			this.Position = this.Position - new Vector2(0, speed);
		}
		if (Input.IsActionPressed("Down"))
		{
			this.Position = this.Position + new Vector2(0, speed);
		}

		if (this.Position.Y > initialPos.Y)
		{
			this.Position = initialPos;
		}	
	}
}
