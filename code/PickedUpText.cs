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

	public PickedUpText()
	{
		Label = Add.Label("Picked Up:", "info");

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
			SetClass("active", true);

			IconFuel.SetClass("active", true);
			IconBattery.SetClass("active", false);

			CurrentItem.Text = "FUEL CAN";
		}
		else if (player.Tags.Has("is_holding_battery"))
		{
			SetClass("active", true);

			IconFuel.SetClass("active", false);
			IconBattery.SetClass("active", true);

			CurrentItem.Text = "CAR BATTERY";
		}
        else 
		{
			SetClass("active", false);
		}
	}
}
