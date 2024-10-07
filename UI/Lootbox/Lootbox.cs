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
			{ CubeType.Squiggles, "res://Cubes/CubeScenes/SquiggleCube.tscn"}
		};
		
	private Node2D banner;
	private Timer cooldownTimer;

	private bool onCooldown = false;
		
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		banner = GetTree().Root.GetNode<Node2D>("SceneManager/Banner");
		cooldownTimer = GetNode<Timer>("CooldownTimer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Space"))
		{
			SceneManager.Instance.AddScene(cubeScenes[(CubeType.Tree)], new Vector2(banner.GlobalPosition.X, banner.GlobalPosition.Y - spawnDistance));
		}
	}

	[Export]
	public float spawnDistance = 1200;
	
	public void _on_loot_button_pressed() {
		if (onCooldown) {
			return;
		}

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
			SceneManager.Instance.AddScene(scenePath, new Vector2(banner.GlobalPosition.X, banner.GlobalPosition.Y - spawnDistance));
		}
		
		onCooldown = true;
		cooldownTimer.Start();
	}

	public void _on_cooldown_timer_timeout() {
		onCooldown = false;
	}
}

public enum CubeType {
	Basic,
	Steel,
	Tree,
	Galoshes,
	Squiggles
}
