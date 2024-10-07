using Godot;
using System;

public partial class SceneManager : Node
{
	public static SceneManager Instance {get; private set;}
	public override void _Ready()
	{
		Instance = this;
	}

	public Node2D AddScene(string scenePath, Vector2? spawnPosition = null, Node2D parent = null)
	{
		Node2D sceneToAdd = (Node2D)GD.Load<PackedScene>(scenePath).Instantiate();
		
		if (spawnPosition != null)
		{
			sceneToAdd.GlobalPosition = spawnPosition.Value;
		}

		if (parent == null)
		{
			Instance.AddChild(sceneToAdd, true);
		}
		else
		{
			parent.AddChild(sceneToAdd, true);
		}

		return sceneToAdd;
	}

	public Control AddControl(string controlPath, Vector2? spawnPosition = null, Node2D? parent = null)
	{
		Control controlAdd = (Control)GD.Load<PackedScene>(controlPath).Instantiate();
		
		if (spawnPosition != null)
		{
			controlAdd.GlobalPosition = spawnPosition.Value;
		}

		if (parent != null)
		{
			parent.AddChild(controlAdd);
		}
		else
		{
			Instance.AddChild(controlAdd);
		}

		return controlAdd;
	}

}
