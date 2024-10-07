using Godot;
using System;

public partial class Cube : Node2D
{
	[Signal]
	public delegate void MouseEnteredEventHandler();
	[Signal]
	public delegate void MouseExitedEventHandler();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{			
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_box_body_mouse_entered()
	{
		RigidBody2D cubeBody = GetNode<RigidBody2D>("BoxBody");
		cubeBody.EmitSignal(SignalName.MouseEntered, Name);
	}
	private void _on_box_body_mouse_exited()
	{
		RigidBody2D cubeBody = GetNode<RigidBody2D>("BoxBody");
		cubeBody.EmitSignal(SignalName.MouseExited, Name);
	}
}
