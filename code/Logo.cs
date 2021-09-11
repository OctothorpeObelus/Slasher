using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class Logo : Panel
{
	public Label Label;

	public Logo()
	{
		Label = Add.Label("Alpha v0.01", "Logo");
	}

	public override void Tick()
	{

		var player = Local.Pawn;
		if (player == null) return;

	}
}
