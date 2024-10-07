using Godot;
using System;

public partial class SteelCube : Node2D
{
	[Signal]
	public delegate void MouseEnteredEventHandler();
	[Signal]
	public delegate void MouseExitedEventHandler();

	[Export]
	bool canGrab;

	private Banner banner;
	private RigidBody2D rb2d;

	private bool grabbable = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("spawn");

		banner = GetNode<Banner>("/root/SceneManager/Banner");
		rb2d = GetChild<RigidBody2D>(0);
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// if the cube is at rest, attempt to raise the banner
		if (rb2d.LinearVelocity.Length() < 0.01f) {
			banner.AttemptHighest(GlobalPosition);
		}
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
