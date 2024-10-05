using Godot;
using System;

public partial class BasicCube : Node2D
{
	[Signal]
	public delegate void MouseEnteredEventHandler();

	private bool grabbable = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("spawn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var mouseDir = GetLocalMousePosition().Normalized();
		if (Input.IsActionPressed("Click") && grabbable)
		{
			GD.Print("test");
		}
	}

	private void _on_rigid_body_2d_mouse_entered()
	{
		grabbable = true;
	}

	private void _on_rigid_body_2d_mouse_exited()
	{
		grabbable = false;
	}

}
