using Godot;
using System;

public partial class SceneManager : Node
{
	public static SceneManager Instance {get; private set;}
	public override void _Ready()
	{
		Instance = this;
	}

	public void AddScene(string scenePath, Vector2? spawnPosition = null)
	{
		Node2D sceneToAdd = (Node2D)GD.Load<PackedScene>(scenePath).Instantiate();

		if (spawnPosition != null)
		{
			sceneToAdd.GlobalPosition = spawnPosition.Value;
		}

		Instance.AddChild(sceneToAdd, true);
	}

}
