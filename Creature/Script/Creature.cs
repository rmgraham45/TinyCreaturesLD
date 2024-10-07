using System;
using System.ComponentModel;
using Godot;

public partial class Creature : Node2D 
{

	public Godot.Collections.Dictionary<CreatureType, string> creatureScenes = new Godot.Collections.Dictionary<CreatureType, string> {
		{ CreatureType.TwoHeadedCrab, "res://Creature/Scene/TwoHeadedCrab.tscn" },
		{ CreatureType.Galoshes, "res://Creature/Scene/Galoshes.tscn" },
		{ CreatureType.Beemurai, "res://Creature/Scene/Beemurai.tscn"},
	};

	[Export]
	public string creatureName = "ERR", creatureDescription = "ERR", creatureLikes = "ERR", creatureDislikes = "ERR";

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

	private CollisionObject2D collider;

	private Emote emote;

	private CareModePicker careModePicker;

	private Node2D cube;
	private CollisionShape2D cubeArea;
	private RigidBody2D rb2d;

	private Bestiary bestiary;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		// Called every time the node is added to the scene.
		LoadScene();

		collider = GetChild<CollisionObject2D>(0);

		collider.InputEvent += MouseClick;

		emote = GetChild<Node2D>(0).GetNode<Emote>("GPUParticles2D");

		careModePicker = GetTree().Root.GetNode<SceneManager>("SceneManager").GetNode<CareModePicker>("Control/Camera2D/CanvasLayer/CareUi");
	
		cube = GetParent<RigidBody2D>().GetParent<Node2D>();
		cubeArea = cube.GetNode<RigidBody2D>("BoxBody").GetNode<CollisionShape2D>("CollisionShape2D");
		rb2d = GetChild<RigidBody2D>(0);

		bestiary = GetTree().Root.GetNode<SceneManager>("SceneManager").GetNode<Bestiary>("Control/Camera2D/CanvasLayer/BestiaryUi");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		// Called every frame. Delta is the elapsed time since the previous frame.
		FixOutOfBounds();
	}

	public void LoadScene() {
		if (creatureScenes.ContainsKey(creatureType)) {
			Node2D instance = SceneManager.Instance.AddScene(creatureScenes[creatureType], Vector2.Zero, this);
		}
	}
	
	private void MouseClick(Node viewport, InputEvent @event, long shapeIdx) {
		if (@event is InputEventMouseButton click) {
			if (click.Pressed && click.ButtonIndex == MouseButton.Right) {
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
				} else if (foodType == CreatureFoodType.Meat) {
					if (hunger > 0) {
						emote.ShowLove();
						hunger = 0;
					}
				} else if (foodType == CreatureFoodType.Veggie) {
					emote.ShowSick();
					hunger++;
				} else if (foodType == CreatureFoodType.Omni) {
					if (hunger > 0) {
						emote.ShowLove();
						hunger = 0;
					}
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
					if (hunger > 0) {
						emote.ShowLove();
						hunger = 0;
					}
				} else if (foodType == CreatureFoodType.Omni) {
					if (hunger > 0) {
						emote.ShowLove();
						hunger = 0;
					}
				}
				break;
			
			case CareModePicker.Intent.Gun:
				Die();
				break;
			
			case CareModePicker.Intent.Learn:
				bestiary.OpenWindow(this);
				break;
		}

		if (hunger == 5 || happiness == 0) {
			Die();
		}
	}

	public void Die() {
		cube.QueueFree();
	}

	public void FixOutOfBounds() {
		// check if Creature position falls within cubeArea
		if (cubeArea.Shape.GetRect().HasPoint(rb2d.Position)) {
			return;
		}
		else {
			// if not, move Creature to the center of it
			rb2d.Position = cubeArea.Shape.GetRect().GetCenter();
		}
	}
}

public enum CreatureType {
	TwoHeadedCrab,
	Galoshes,
	Beemurai
}

public enum CreatureFoodType {
	None,
	Meat,
	Veggie,
	Omni
};
