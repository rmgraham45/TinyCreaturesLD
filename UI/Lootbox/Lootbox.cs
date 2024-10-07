using Godot;
using System;
using System.Collections.Generic;

public partial class Lootbox : Control
{
	public const string cubeLoc = "res://Cubes/CubeScenes/";
	public Godot.Collections.Dictionary<CubeType, string> cubeScenes = 
		new Godot.Collections.Dictionary<CubeType, string> {
			{ CubeType.Steel, cubeLoc + "SteelCube.tscn" },
			{ CubeType.Tree, cubeLoc + "TreeCube.tscn"},
			{ CubeType.Galoshes, cubeLoc + "GaloshesHome.tscn"},
			{ CubeType.Squiggles, cubeLoc + "SquiggleCube.tscn"},
			{ CubeType.Pancake, cubeLoc + "PancakeCube.tscn"},
			{ CubeType.Cactus, cubeLoc + "CactusDesert.tscn"}
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
	Squiggles,
	Pancake,
	Gemma,
	Cactus
}
