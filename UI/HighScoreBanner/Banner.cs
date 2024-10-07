using Godot;

public partial class Banner : Node2D
{

	[Export]
	public Vector2 startingPos;
	[Export]
	public Vector2 offset;
	[Export]
	public float lerpSpeed = 5f;
	[Export]
	public float scoreScaleMultiplier = 1f;

	[Export]
	public Vector2 targetPos;

	[Export]
	public RichTextLabel highScoreLabel;

	public override void _Ready()
	{
		Visible = false;

		startingPos = GlobalPosition;
		targetPos = startingPos;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// lerp to target position
		GlobalPosition = GlobalPosition.Lerp(targetPos, lerpSpeed);
	}
	
	public void UpdateHighest(float maxY)
	{
		Visible = true;

		targetPos = new Vector2(GlobalPosition.X, maxY) + offset;

		highScoreLabel.Text = "[center]" + ConvertYToScore(maxY).ToString("0.0") + "[/center]";
	}

	public float ConvertYToScore(float y) {
		// 100 pixels = 1 point
		return (startingPos.Y - y)/100;
	}

	public void AttemptHighest(float maxY)
	{
		if (maxY < targetPos.Y)
		{
			UpdateHighest(maxY);
		}
	}
}
