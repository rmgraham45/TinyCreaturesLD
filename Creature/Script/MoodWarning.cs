using Godot;
using System;

public partial class MoodWarning : Control
{

	[Export]
	public Texture2D hungryTexture, sadTexture;
	public Texture2D normalBubbleTexture, extremeBubbleTexture;

	[Export]
	public TextureRect bubbleTextureRect;

	[Export]
	public TextureRect typeTextureRect;

	[Export]
	public Node2D extremeNode;

	[Export]
	public WarningType warningType;

	[Export]
	public Node2D target;

	private Vector2 vectorTarget;
	
	[Export]
	public Vector2 offset;

	private float lerpSpeed = 0.5f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	public void Initiate(WarningType w, Node2D target) {
		switch (w) {
			case WarningType.Hunger:
				typeTextureRect.Texture = hungryTexture;
				break;
			case WarningType.Mood:
				typeTextureRect.Texture = sadTexture;
				break;
			case WarningType.HungerCritical:
				typeTextureRect.Texture = hungryTexture;
				extremeNode.Visible = true;
				break;
			case WarningType.MoodCritical:
				typeTextureRect.Texture = sadTexture;
				extremeNode.Visible = true;
				break;
		}

		this.target = target;
	}

	public override void _Process(double delta)
	{
		if (target != null) {
			GlobalPosition = GlobalPosition.Lerp(target.GlobalPosition + offset, lerpSpeed);
		}
	}

	public void Resolve() {
		QueueFree();
	}
}

public enum WarningType {
	Hunger,
	Mood,
	HungerCritical,
	MoodCritical
}
