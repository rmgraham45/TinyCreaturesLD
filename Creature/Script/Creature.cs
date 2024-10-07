using System;
using System.ComponentModel;
using Godot;

public partial class Creature : Node2D 
{

	public Godot.Collections.Dictionary<CreatureType, string> creatureScenes = new Godot.Collections.Dictionary<CreatureType, string> {
		{ CreatureType.TwoHeadedCrab, "res://Creature/Scene/TwoHeadedCrab.tscn" },
		{ CreatureType.Galoshes, "res://Creature/Scene/Galoshes.tscn" },
		{ CreatureType.Beemurai, "res://Creature/Scene/Beemurai.tscn"},
		{ CreatureType.Squiggles, "res://Creature/Scene/Squiggles.tscn"}
	};


	[Export]
	public string creatureName = "ERR", creatureDescription = "ERR", creatureLikes = "ERR", creatureDislikes = "ERR";

	[Export]
	public bool floating = false;
	[Export]
	public CreatureType creatureType;
	[Export]
	public CreatureFoodType foodType = CreatureFoodType.None;

	[Export]
	public int happiness; // 0-5
	[Export]
	public float hunger; // 0-5
	[Export]
	public float needsRate = 1.0f; // speed multiplier for hunger - higher is faster
	[Export]
	public bool likesTickled = true;

	private CollisionObject2D collider;

	private Emote emote;

	private CareModePicker careModePicker;

	private Node2D cube;
	private CollisionShape2D cubeArea;
	private RigidBody2D rb2d;

	private Bestiary bestiary;

	public Timer needsTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		// Called every time the node is added to the scene.
		LoadScene();

		collider = GetChild<CollisionObject2D>(1);

		collider.InputEvent += MouseClick;

		emote = GetChild<Node2D>(1).GetNode<Emote>("GPUParticles2D");

		careModePicker = GetTree().Root.GetNode<SceneManager>("SceneManager").GetNode<CareModePicker>("Control/Camera2D/CanvasLayer/CareUi");
	
		cube = GetParent<RigidBody2D>().GetParent<Node2D>();
		cubeArea = cube.GetNode<RigidBody2D>("BoxBody").GetNode<CollisionShape2D>("CollisionShape2D");
		rb2d = GetChild<RigidBody2D>(1);

		bestiary = GetTree().Root.GetNode<SceneManager>("SceneManager").GetNode<Bestiary>("Control/Camera2D/CanvasLayer/BestiaryUi");

		needsTimer = GetNode<Timer>("Timer").GetNode<Timer>("NeedsTimer");
		needsTimer.WaitTime = needsTimer.WaitTime / needsRate;
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
					happiness = 5;
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

		CheckIfDead();
		UpdateMoodBubble();
	}

	public void Die() {
		RemoveWarning();
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

	private void _on_timer_timeout()
	{
		if (floating)
		{
			RigidBody2D floatingBody = (RigidBody2D)this.GetChild(1);
			floatingBody.ApplyImpulse(new Vector2(80f,80f));
		}
	}

	private void _on_needs_timer_timeout() {
		hunger++;
		happiness--;
		UpdateMoodBubble();
		CheckIfDead();
	}

	public void CheckIfDead() {
		if (hunger >= 5) {
			Die();
		}
		else if (happiness <= 0) {
			Die();
		}
	}

	public MoodWarning moodWarning;

	public void UpdateMoodBubble() {
		if (hunger == 4) {
			SpawnWarning(WarningType.HungerCritical);
		}
		else if (happiness == 1) {
			SpawnWarning(WarningType.MoodCritical);
		}
		else if (hunger == 3) {
			SpawnWarning(WarningType.Hunger);
		}
		else if (happiness == 2) {
			SpawnWarning(WarningType.Mood);
		}
		else {
			RemoveWarning();
		}
	}

	public void SpawnWarning(WarningType warningType) {
		if (moodWarning != null) {
			moodWarning.Resolve();
			moodWarning = null;
		}

		MoodWarning warning = (MoodWarning) SceneManager.Instance.AddControl("res://WorldScenes/MoodWarning.tscn", rb2d.GlobalPosition);
		warning.Initiate(warningType, rb2d);

		moodWarning = warning;
	}

	public void ChangeWarningType(WarningType warningType) {
		moodWarning.Initiate(warningType, this);
	}

	public void RemoveWarning() {
		if (moodWarning != null) moodWarning.Resolve();
		moodWarning = null;
	}
}

public enum CreatureType {
	TwoHeadedCrab,
	Galoshes,
	Beemurai,
	Squiggles
}

public enum CreatureFoodType {
	None,
	Meat,
	Veggie,
	Omni
};
