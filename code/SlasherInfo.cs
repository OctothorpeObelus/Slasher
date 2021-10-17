using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class SlasherInfo : Panel
{
	public Label CurrentSlasher;

	public Label Abilities;

	public Panel IconBaba;

	public Panel IconAmogus;

	public Panel IconTrollge;

	public SlasherInfo()
	{
		CurrentSlasher = Add.Label("Error!", "");

		Abilities = Add.Label("Error!", "abilities");

		IconBaba = Add.Panel("iconbaba");

		IconAmogus = Add.Panel("iconamogus");

		IconTrollge = Add.Panel("icontrollge");

	}

	public override void Tick()
	{

		var player = Local.Pawn;
		if (player == null) return;

		if(player.Tags.Has("slasher"))
			SetClass("active",true);
		else
			SetClass("active",false);
	
		if(player.Tags.Has("bababooey")){

			CurrentSlasher.Text = "Bababooey";

			Abilities.Text = "M1 to kill \nM2 to turn invisible \nR to place clone";

			IconBaba.SetClass("active",true);
			IconAmogus.SetClass("active",false);
			IconTrollge.SetClass("active",false);
		}

		if(player.Tags.Has("amogus")){

			CurrentSlasher.Text = "Amogus";

			Abilities.Text = "M1 to kill \nM2 to become Survivor \nR to become Fuel Can";

			IconBaba.SetClass("active",false);
			IconAmogus.SetClass("active",true);
			IconTrollge.SetClass("active",false);
		}

		if(player.Tags.Has("trollge")){

			CurrentSlasher.Text = "Trollge";

			Abilities.Text = "M1 to slash";

			IconBaba.SetClass("active",false);
			IconAmogus.SetClass("active",false);
			IconTrollge.SetClass("active",true);
		}

	}
}
