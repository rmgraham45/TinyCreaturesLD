using Godot;
using System;

public partial class Start : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		SceneManager.Instance.AddScene("res://WorldScenes/Background.tscn");
		//SceneManager.Instance.AddControl("res://WorldScenes/CareUI.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Space"))
		{
			SceneManager.Instance.AddScene("res://Cubes/BasicCube.tscn", GetGlobalMousePosition());
		}
	}
}
