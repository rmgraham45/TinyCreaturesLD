using Godot;
using System;

public partial class Emote : Node
{

	public GpuParticles2D particles;

	public Sprite2D loveSprite;
	public Sprite2D hateSprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		particles = GetParent() as GpuParticles2D;

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
		particles.Emitting = false;
		particles.Texture = loveSprite.Texture;
		particles.Emitting = true;
	}

	public void ShowHate() {
		particles.Emitting = false;
		particles.Texture = hateSprite.Texture;
		particles.Emitting = true;
	}

	public void _on_two_headed_crab_mouse_click() {
		ShowLove();
	}
}
