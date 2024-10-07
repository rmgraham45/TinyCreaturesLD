using Godot;
using System;
using System.Collections.Generic;

public partial class Lootbox : Control
{
	
	public Godot.Collections.Dictionary<CubeType, string> cubeScenes = 
		new Godot.Collections.Dictionary<CubeType, string> {
			{ CubeType.Steel, "res://Cubes/CubeScenes/SteelCube.tscn" },
			{ CubeType.Basic, "res://Cubes/CubeScenes/BasicCube.tscn"},
			{ CubeType.Tree, "res://Cubes/CubeScenes/TreeCube.tscn"},
			{ CubeType.Galoshes, "res://Cubes/CubeScenes/GaloshesHome.tscn"},
		};
		
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	[Export]
	public Vector2 spawnTarget;
	
	public void _on_loot_button_pressed() {
		Random rand = new Random();
		int index = rand.Next(0, cubeScenes.Count);
		string scenePath = null;
		int i = 0;
		foreach (var kvp in cubeScenes)
		{
			if (i == index)
			{
				scenePath = kvp.Value;
				break;
			}
			i++;
		}

		if (scenePath != null)
		{
			SceneManager.Instance.AddScene(scenePath, spawnTarget);
		}
	}
}

public enum CubeType {
	Basic,
	Steel,
	Tree,
	Galoshes
}
