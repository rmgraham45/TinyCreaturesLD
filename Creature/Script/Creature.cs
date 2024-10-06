using System;
using Godot;

public partial class Creature : Node2D {

	public Godot.Collections.Dictionary<CreatureType, string> creatureScenes = new Godot.Collections.Dictionary<CreatureType, string> {
		{ CreatureType.TwoHeadedCrab, "res://Creature/Scene/TwoHeadedCrab.tscn" }
	};

	[Export]
	public CreatureType creatureType;
	[Export]
	public CreatureFoodType foodType = CreatureFoodType.None;

	[Export]
	public int happiness; // 0-5

	private CollisionPolygon2D bounds;
	private CollisionObject2D collider;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		// Called every time the node is added to the scene.
		LoadScene();

		bounds = GetParent().GetNode<RigidBody2D>("InteriorWalls").GetChild<CollisionPolygon2D>(0);
		collider = GetChild<CollisionObject2D>(0);

		collider.InputEvent += MouseClick;
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
				GD.Print("click!");
			}
		}
	}
}

public enum CreatureType {
	TwoHeadedCrab
}

public enum CreatureFoodType {
	None,
	Meat,
	Veggie
};
