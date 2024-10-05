using Godot;
using System;
using System.Collections.Generic;

public partial class CareModePicker : Node
{

	[Export]
	public Button petButton, meatButton, veggieButton, trashButton, gunButton;

	private List<Button> buttons = new List<Button>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		buttons.Add(petButton);
		buttons.Add(meatButton);
		buttons.Add(veggieButton);
		buttons.Add(trashButton);
		buttons.Add(gunButton);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void DeselectAllOthers(Button currentButton)
	{
		foreach (Button button in buttons)
		{
			if (button != currentButton) {
				button.ButtonPressed = false;
			}
		}
	}

	public void _on_pet_button_pressed() {
		DeselectAllOthers(petButton);
	}

	public void _on_meat_button_pressed() {
		DeselectAllOthers(meatButton);
	}

	public void _on_veggie_button_pressed() {
		DeselectAllOthers(veggieButton);
	}

	public void _on_trash_button_pressed() {
		DeselectAllOthers(trashButton);
	}

	public void _on_gun_button_pressed() {
		DeselectAllOthers(gunButton);
	}
}
