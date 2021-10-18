using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class SlasherCrosshair : Panel
{
	public Label Label;

	public static bool detected;

	public SlasherCrosshair()
	{
		Label = Add.Label(" M1\nKILL", "SlasherCrosshair");
	}

	public override void Tick()
	{

		var player = Local.Pawn;
		if (player == null) return;

		//if(player.Tags.Has("slasher"))
		//	Label.SetClass("active", true);
		//else
		//	Label.SetClass("active", false);

		Label.SetClass("active", detected);
	}

	[ServerCmd( "slasher_detected_survivor" )]
	public static void SurvivorDetect()
	{
		detected = true;
	}

}
