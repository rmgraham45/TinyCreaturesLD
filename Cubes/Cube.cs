using Godot;
using System;

public partial class Cube : Node2D
{
	[Signal]
	public delegate void MouseEnteredEventHandler();
	[Signal]
	public delegate void MouseExitedEventHandler();

	private Banner banner;
	private RigidBody2D rb2d;
	private CollisionShape2D cs2d;
	private CubePhysics physics;

	private bool canScore;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		banner = GetNode<Banner>("/root/SceneManager/Banner");
		rb2d = GetChild<RigidBody2D>(0);
		cs2d = rb2d.GetNode<CollisionShape2D>("CollisionShape2D");
		physics = GetNode<CubePhysics>("BoxBody");

		var animatedSprite2D = GetNode("BoxBody/Sprite2D");
		if (animatedSprite2D.GetType().ToString() == "Godot.AnimatedSprite2D")
		{
			(animatedSprite2D as AnimatedSprite2D).Play();
		}

		canScore = false;
		// start a 3 second timer to allow scoring
		var timer = GetNode<Timer>("CountdownTimer");
		timer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// if the cube is at rest, attempt to raise the banner
		if (rb2d.LinearVelocity.Length() < 0.05f && canScore && !physics.grabbed) {
			banner.AttemptHighest(GetHighestPoint());
		}
	}

	private float GetHighestPoint()
	{
		Rect2 rect = cs2d.Shape.GetRect();
		
		Vector2 point1 = rect.Position + cs2d.GlobalPosition;
		Vector2 point2 = rect.End + cs2d.GlobalPosition;
		Vector2 point3 = new Vector2(point1.X, point2.Y);
		Vector2 point4 = new Vector2(point2.X, point1.Y);

		// get max y value of the 4 points
		float maxY = Mathf.Min(point1.Y, point2.Y);
		maxY = Mathf.Min(maxY, point3.Y);
		maxY = Mathf.Min(maxY, point4.Y);

		return maxY;
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

	private void _on_countdown_timer_timeout()
	{
		canScore = true;
		GD.Print("unlocked cube " + Name + ". its current highest point is " + GetHighestPoint());
	}
}
