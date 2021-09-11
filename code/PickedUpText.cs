using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class PickedUpText : Panel
{
	public Label Label;

	public Label CurrentItem;

	public Label Tooltip;

	public Panel IconFuel;

	public Panel IconBattery;

	private int hideDelay;

	public PickedUpText()
	{
		Label = Add.Label("Holding:", "pickedup");

		CurrentItem = Add.Label("FUEL CAN", "item-name");

		Tooltip = Add.Label("E to use | M2 to drop", "toolt");

		IconFuel = Add.Panel("iconfuel");

		IconBattery = Add.Panel("iconbattery");
	}

	public override void Tick()
	{

		var player = Local.Pawn;
		if (player == null) return;

		if(player.Tags.Has("is_holding_fuel"))
        {

			IconFuel.SetClass("active", true);
			IconBattery.SetClass("active", false);

			Tooltip.Text = "E to use | M2 to drop";

			CurrentItem.Text = "FUEL CAN";

			if(Input.Pressed(InputButton.Use))
				hideDelay = 1;

			if(hideDelay < 800)
				hideDelay++;

		}
		else if (player.Tags.Has("is_holding_battery"))
		{

			IconFuel.SetClass("active", false);
			IconBattery.SetClass("active", true);

			Tooltip.Text = "E to use | M2 to drop";

			CurrentItem.Text = "CAR BATTERY";

			if(Input.Pressed(InputButton.Use))
				hideDelay = 1;

			if(hideDelay < 800)
				hideDelay++;
		}
        else
		{
			SetClass("active", false);
			hideDelay = 0;
		}

		if(hideDelay > 0 && hideDelay < 800)
			SetClass("active", true);

		if(hideDelay >= 800)
			SetClass("active", false);
	}
}
