using Godot;
using System;

public partial class Emote : GpuParticles2D
{
	public Sprite2D loveSprite;
	public Sprite2D hateSprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		loveSprite = new Sprite2D();
		loveSprite.Texture = GD.Load<Texture2D>("res://Emote/Sprite/love.png");

		hateSprite = new Sprite2D();
		hateSprite.Texture = GD.Load<Texture2D>("res://Emote/Sprite/hate.png");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ShowLove() {
		this.Emitting = false;
		this.Texture = loveSprite.Texture;
		this.Emitting = true;
	}

	public void ShowHate() {
		this.Emitting = false;
		this.Texture = hateSprite.Texture;
		this.Emitting = true;
	}
}
