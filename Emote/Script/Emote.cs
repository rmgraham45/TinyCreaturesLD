using Godot;
using System;

public partial class Emote : GpuParticles2D
{
	public Sprite2D laughSprite;
	public Sprite2D hateSprite;
	public Sprite2D loveSprite;
	public Sprite2D happySprite;
	public Sprite2D sickSprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		laughSprite = new Sprite2D();
		laughSprite.Texture = GD.Load<Texture2D>("res://Emote/Sprite/laugh.png");

		hateSprite = new Sprite2D();
		hateSprite.Texture = GD.Load<Texture2D>("res://Emote/Sprite/hate.png");

		happySprite = new Sprite2D();
		happySprite.Texture = GD.Load<Texture2D>("res://Emote/Sprite/happy.png");

		loveSprite = new Sprite2D();
		loveSprite.Texture = GD.Load<Texture2D>("res://Emote/Sprite/love.png");

		sickSprite = new Sprite2D();
		sickSprite.Texture = GD.Load<Texture2D>("res://Emote/Sprite/sick.png");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ShowLaugh() {
		this.Emitting = false;
		this.Texture = laughSprite.Texture;
		this.Emitting = true;
	}

	public void ShowHate() {
		this.Emitting = false;
		this.Texture = hateSprite.Texture;
		this.Emitting = true;
	}

	public void ShowLove() {
		this.Emitting = false;
		this.Texture = loveSprite.Texture;
		this.Emitting = true;
	}

	public void ShowHappy() {
		this.Emitting = false;
		this.Texture = happySprite.Texture;
		this.Emitting = true;
	}

	public void ShowSick() {
		this.Emitting = false;
		this.Texture = sickSprite.Texture;
		this.Emitting = true;
	}
}
