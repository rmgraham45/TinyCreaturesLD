using System;
using System.ComponentModel;
using Godot;

public partial class Creature : Node2D 
{

	public Godot.Collections.Dictionary<CreatureType, string> creatureScenes = new Godot.Collections.Dictionary<CreatureType, string> {
		{ CreatureType.TwoHeadedCrab, "res://Creature/Scene/TwoHeadedCrab.tscn" },
		{ CreatureType.Galoshes, "res://Creature/Scene/Galoshes.tscn" }
	};

	[Export]
	public CreatureType creatureType;
	[Export]
	public CreatureFoodType foodType = CreatureFoodType.None;

	[Export]
	public int happiness; // 0-5
	[Export]
	public float hunger; // 0-3
	[Export]
	public float hungerRate = 1.0f; // speed multiplier for hunger
	[Export]
	public bool likesTickled = true;

	private CollisionPolygon2D bounds;
	private CollisionObject2D collider;

	private Emote emote;

	private CareModePicker careModePicker;

	private Node2D cube;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		// Called every time the node is added to the scene.
		LoadScene();

		bounds = GetParent().GetNode<RigidBody2D>("InteriorWalls").GetChild<CollisionPolygon2D>(0);
		collider = GetChild<CollisionObject2D>(0);

		collider.InputEvent += MouseClick;

		emote = GetChild<Node2D>(0).GetNode<Emote>("GPUParticles2D");

		careModePicker = GetTree().Root.GetNode<SceneManager>("SceneManager").GetNode<CareModePicker>("CareUi");
	
		cube = GetParent<RigidBody2D>().GetParent<Node2D>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		// Called every frame. Delta is the elapsed time since the previous frame.
	}

	public void LoadScene() {
		if (creatureScenes.ContainsKey(creatureType)) {
			Node2D instance = SceneManager.Instance.AddScene(creatureScenes[creatureType], Vector2.Zero, this);
		}
	}
	
	private void MouseClick(Node viewport, InputEvent @event, long shapeIdx) {
		if (@event is InputEventMouseButton click) {
			if (click.Pressed) {
				Interact();
			}
		}
	}

	public void Interact() {
		switch (careModePicker.currentIntent) {
			case CareModePicker.Intent.Pet:
				if (likesTickled) {
					emote.ShowLaugh();
					happiness++;
					if (happiness > 5) happiness = 5;
				} else {
					emote.ShowHate();
					happiness--;
					if (happiness < 0) happiness = 0;
				}
				break;

			case CareModePicker.Intent.Meat:
				if (foodType == CreatureFoodType.None) {
					emote.ShowHate();
				}
				else if (foodType == CreatureFoodType.Meat) {
					emote.ShowLove();
					hunger = 0;
				} else if (foodType == CreatureFoodType.Veggie) {
					emote.ShowSick();
					hunger++;
					if (hunger < 0) hunger = 0;
				} else if (foodType == CreatureFoodType.Omni) {
					emote.ShowLove();
					hunger = 0;
				}
				break;

			case CareModePicker.Intent.Veggie:
				if (foodType == CreatureFoodType.None) {
					emote.ShowHate();
				}
				else if (foodType == CreatureFoodType.Meat) {
					emote.ShowSick();
					hunger++;
					if (hunger < 0) hunger = 0;
				} else if (foodType == CreatureFoodType.Veggie) {
					emote.ShowLove();
					hunger = 0;
				} else if (foodType == CreatureFoodType.Omni) {
					emote.ShowLove();
					hunger = 0;
				}
				break;
		}

		if (hunger == 5 || happiness == 0) {
			Die();
		}
	}

	public void Die() {
		cube.QueueFree();
	}
}

public enum CreatureType {
	TwoHeadedCrab,
	Galoshes
}

public enum CreatureFoodType {
	None,
	Meat,
	Veggie,
	Omni
};
