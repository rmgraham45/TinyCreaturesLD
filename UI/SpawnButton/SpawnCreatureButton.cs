using Godot;
using System;

public partial class SpawnCreatureButton : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_area_2d_mouse_entered()
	{
		SceneManager.Instance.AddScene("res://Cubes/BasicCube.tscn", GlobalTransform.Origin);
	}

	public void _on_area_2d_mouse_exited()
	{

	}
}
