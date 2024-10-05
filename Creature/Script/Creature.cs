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

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		// Called every time the node is added to the scene.
		LoadScene();

		bounds = GetParent().GetNode<RigidBody2D>("InteriorWalls").GetChild<CollisionPolygon2D>(0);
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

	public void FixOutOfBounds() {
		// compare to bounds; if position is outside of bounds, move back inside. use local position
		Vector2[] array = bounds.Polygon;
		if (array.Length == 4) {
			Vector2[] points = new Vector2[4];
			for (int i = 0; i < 4; i++) {
				points[i] = array[i];
			}

			Vector2 localPosition = GlobalPosition - bounds.GlobalPosition;
			if (!IsPointInPolygon(localPosition, points)) {
				Vector2 closestPoint = GetClosestPointInPolygon(localPosition, points);
				Vector2 newPosition = bounds.GlobalPosition + closestPoint;
				GlobalPosition = newPosition;
			}
		} else {
			throw new Exception("Bounds polygon must have 4 points.");
		}
	}

	private bool IsPointInPolygon(Vector2 point, Vector2[] polygon) {
		int i, j;
		bool result = false;
		for (i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++) {
			if ((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y) &&
				(point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)) {
				result = !result;
			}
		}
		return result;
	}

	private Vector2 GetClosestPointInPolygon(Vector2 point, Vector2[] polygon) {
		Vector2 closestPoint = polygon[0];
		float minDistSq = point.DistanceSquaredTo(polygon[0]);

		for (int i = 1; i < polygon.Length; i++) {
			float distSq = point.DistanceSquaredTo(polygon[i]);
			if (distSq < minDistSq) {
				minDistSq = distSq;
				closestPoint = polygon[i];
			}
		}

		return closestPoint;
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
