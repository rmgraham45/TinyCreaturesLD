using Godot;
using System;

public partial class Banner : Node2D
{

	[Export]
	public Vector2 startingPos;
	[Export]
	public float lerpSpeed = 0.4f;
	[Export]
	public float scoreScaleMultiplier = 1f;

	[Export]
	public Vector2 targetPos;

	public float currentHighestY;
	public RichTextLabel highScoreLabel;

	public override void _Ready()
	{
		startingPos = GlobalPosition;
		currentHighestY = startingPos.Y;
		targetPos = startingPos;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// lerp to target position
		GlobalPosition = GlobalPosition.Lerp(targetPos, lerpSpeed);
	}
	
	public void UpdateHighest(Vector2 highestPos)
	{
		GD.Print("new high score! position: " + highestPos.Y);
		currentHighestY = highestPos.Y;
		highScoreLabel.Text = ConvertYToScore(currentHighestY).ToString("0.0");

		targetPos = new Vector2(GlobalPosition.X, currentHighestY);
	}

	public float ConvertYToScore(float y) {
		return 0;
	}

	public void AttemptHighest(Vector2 pos)
	{
		if (pos.Y < currentHighestY)
		{
			UpdateHighest(pos);
		}
		else {
			GD.Print("not high enough: " + pos.Y + " vs " + currentHighestY);
		}
	}
}
