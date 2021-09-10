using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class Logo : Panel
{
	public Label Label;

	public Logo()
	{
		Label = Add.Label("", "Logo");
	}

	public override void Tick()
	{

		var player = Local.Pawn;
		if (player == null) return;

	}
}
