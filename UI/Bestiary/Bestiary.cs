using Godot;
using System;

public partial class Bestiary : Control
{

	[Export]
	public RichTextLabel nameLabel, descriptionLabel, likesLabel, dislikesLabel;


	public override void _Ready() {
		Visible = false;
	}

	public void OpenWindow(Creature creature) {
		nameLabel.Text = creature.creatureName;
		descriptionLabel.Text = creature.creatureDescription;
		likesLabel.Text = "[color=#30FF83]Likes: " + creature.creatureLikes + "[/color]";
		dislikesLabel.Text = "[color=#FF3B3B]Dislikes: " + creature.creatureDislikes + "[/color]";

		Visible = true;
	}

	public void CloseWindow() {
		Visible = false;
	}

	public void _on_x_button_pressed() {
		CloseWindow();
	}
}
