using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class Crosshair : Panel
{
	public Label Label;

	public Crosshair()
	{
		Label = Add.Label("", "Crosshair");
	}

	public override void Tick()
	{

		var player = Local.Pawn;
		if (player == null) return;

	}
}
