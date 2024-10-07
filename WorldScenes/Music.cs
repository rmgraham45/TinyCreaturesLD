using Godot;
using System;

public partial class Music : Control
{

	public AudioStreamPlayer player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_audio_stream_player_finished()
	{
		player.Play();
	}

	public void Pause() {
		player.StreamPaused = true;
	}

	public void Play() {
		player.StreamPaused = false;
	}
}
