using Godot;
using System;

public partial class CreatureClickSignaler : RigidBody2D
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		TryClick();
	}

	bool clickable = false;

	private void TryClick()
	{
		if (clickable && Input.IsActionJustPressed("Right Click"))
		{
			GD.Print("click!");
		}
	}

	public void _on_mouse_entered()
	{
		clickable = true;
		GD.Print("mouse entered");
	}

	private void _on_mouse_exited()
	{
		clickable = true;
	}

}
