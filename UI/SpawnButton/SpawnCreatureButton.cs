using Godot;
using System;
using System.Linq;

public partial class SpawnCreatureButton : Node2D
{

	public Godot.Collections.Dictionary<CubeType, string> cubeScenes = 
		new Godot.Collections.Dictionary<CubeType, string> {
			{ CubeType.Steel, "res://Cubes/SteelCube.tscn" },
			{ CubeType.Basic, "res://Cubes/BasicCube.tscn"},
			{ CubeType.Tree, "res://Cubes/TreeCube.tscn"},
		};

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
		Random rand = new Random();
		SceneManager.Instance.AddScene(cubeScenes.ElementAt(rand.Next(0, cubeScenes.Count)).Value, GlobalTransform.Origin);
	}

	public void _on_area_2d_mouse_exited()
	{

	}
}

public enum CubeType {
	Basic,
	Steel,
	Tree,
}
