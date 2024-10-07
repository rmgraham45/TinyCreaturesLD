using Godot;
using System;
using System.Collections.Generic;

public partial class CareModePicker : Control
{

	[Export]
	public Button petButton, meatButton, veggieButton, trashButton, gunButton, learnButton;

	private List<Button> buttons = new List<Button>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		buttons.Add(petButton);
		buttons.Add(meatButton);
		buttons.Add(veggieButton);
		buttons.Add(trashButton);
		buttons.Add(gunButton);
		buttons.Add(learnButton);

		_on_pet_button_pressed();
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
	
	public Intent currentIntent;

	public void _on_pet_button_pressed() {
		currentIntent = Intent.Pet;
		DeselectAllOthers(petButton);
	}

	public void _on_meat_button_pressed() {
		currentIntent = Intent.Meat;
		DeselectAllOthers(meatButton);
	}

	public void _on_veggie_button_pressed() {
		currentIntent = Intent.Veggie;
		DeselectAllOthers(veggieButton);
	}

	public void _on_trash_button_pressed() {
		currentIntent = Intent.Trash;
		DeselectAllOthers(trashButton);
	}

	public void _on_gun_button_pressed() {
		currentIntent = Intent.Gun;
		DeselectAllOthers(gunButton);
	}

	public void _on_learn_button_pressed() {
		currentIntent = Intent.Learn;
		DeselectAllOthers(learnButton);
	}

	public enum Intent {
		Pet,
		Meat,
		Veggie,
		Trash,
		Gun,
		Learn
	}
}
