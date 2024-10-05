using Godot;
using System;

public partial class CubePhysics : RigidBody2D
{
	[Signal]
	public delegate void MouseEnteredEventHandler(string NodeName);
	[Signal]
	public delegate void MouseExitedEventHandler(string NodeName);
	private bool grabbable = false;
	const int SPRING_CONSTANT = 10;
	private double deltaForce;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		if (grabbable && Input.IsActionPressed("Click"))
		{
			var transform = GlobalTransform;
			transform.Origin = GetGlobalMousePosition();
			GlobalTransform = transform;
		}
		/*if (grabbable && Input.IsActionPressed("Click"))
		{
			var mousePos = GetLocalMousePosition();
			Vector2 velVect = mousePos;

			var normVel = velVect.Normalized();
			GD.Print(mousePos);
			GD.Print(GetGlobalMousePosition());
			this.ApplyForce(Input.GetLastMouseScreenVelocity());
			
		}*/
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
