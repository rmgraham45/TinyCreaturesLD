using Godot;
using System;

public partial class CubePhysics : RigidBody2D
{
	[Signal]
	public delegate void MouseEnteredEventHandler(string NodeName);
	[Signal]
	public delegate void MouseExitedEventHandler(string NodeName);
	private bool grabbable = false;
	private bool grabbed = false;
	const int SPRING_CONSTANT = 10;

	private float storedGravity;

	private double force = 1f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		storedGravity = GravityScale;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (grabbed && Input.IsActionJustReleased("Click"))
		{
			Release();
		}
		else if (grabbed)
		{
			DoGrabLerpForce();
		}
		else if (grabbable && Input.IsActionJustPressed("Click"))
		{
			Grab();
		}
	}

	public void Grab() {
		grabbed = true;
		GravityScale = 0;
		
		RigidBody2D creature = GetParent().GetNode<Node2D>("BoxBody").GetNode<Node2D>("Creature").GetChild<RigidBody2D>(0);
		creature.SetDeferred("freeze", true);
	}

	public void DoGrabLerpForce()
	{
		// Get the direction and distance to the mouse
		Vector2 targetPosition = GetGlobalMousePosition();
		Vector2 direction = targetPosition - GlobalPosition;
		float distance = direction.Length();

		// Normalize the direction to get the unit vector
		if (distance > 0.1f) // A small threshold to avoid jittering when very close
		{
			direction = direction.Normalized();
			
			// Force strength (adjust this for the desired pull speed)
			float forceStrength = 9000f;

			// Apply a force towards the mouse scaled by distance and delta time
			Vector2 force = direction * forceStrength * Mathf.Min(distance, 50f) * (float)GetPhysicsProcessDeltaTime();
			ApplyCentralForce(force);

			// Apply damping to reduce velocity smoothly as the object gets closer to the target
			float damping = 0.9f; // Higher values create stronger damping
			LinearVelocity *= damping;
		}
		else
		{
			// Once close enough to the target, stop the object to avoid jittering
			LinearVelocity = Vector2.Zero;
		}
	}

	public void Release() {
		grabbed = false;
		GravityScale = storedGravity;

		RigidBody2D creature = GetParent().GetNode<Node2D>("BoxBody").GetNode<Node2D>("Creature").GetChild<RigidBody2D>(0);
		creature.SetDeferred("freeze", false);
	}

	private void _on_mouse_entered(string nodeName)
	{
		if (nodeName == GetParent().Name)
			grabbable = true;
	}

	private void _on_mouse_exited(string nodeName)
	{
		if (nodeName == GetParent().Name)
			grabbable = false;
	}
}
