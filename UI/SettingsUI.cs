using Godot;
using System;
using System.Collections.Generic;

public partial class SettingsUI : Control
{

	public Control tutorial;
	public Music music;
	
	public Button muteButton;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		tutorial = GetNode<Control>("Tutorial");
		tutorial.Visible = false;

		music = GetTree().Root.GetNode<Music>("SceneManager/Music");
		
		muteButton = GetNode<Button>("MuteButton");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void ToggleHelp() {
		tutorial.Visible = !tutorial.Visible;
	}

	public void ResetGame() {
		music.QueueFree();

		foreach (Node node in GetTree().GetNodesInGroup("KillOnReset")) {
			node.QueueFree();
		}

		GetTree().ReloadCurrentScene();
	}

	public void _on_tutorial_x_button_pressed() {
		ToggleHelp();
	}

	public void _on_help_button_pressed() {
		ToggleHelp();
	}
	
	public void _on_reset_button_pressed() {
		ResetGame();
	}

	[Export]
	public Texture2D muteTexture, unmuteTexture;

	public void _on_mute_button_pressed() {
		if (music.player.StreamPaused) {
			music.Play();
			muteButton.Icon = unmuteTexture;
		} else {
			music.Pause();
			muteButton.Icon = muteTexture;
		}
	}
}
