using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class PickedUpText : Panel
{
	public Label Label;

	public Label CurrentItem;

	public Label Tooltip;

	public Label IconFuel;

	public Label IconBattery;

	public PickedUpText()
	{
		Label = Add.Label("Picked Up:", "info");

		CurrentItem = Add.Label("FUEL CAN", "item-name");

		Tooltip = Add.Label("E to use | M2 to drop", "toolt");

		IconFuel = Add.Label("", "iconfuel");

		IconBattery = Add.Label("", "iconbattery");
	}

	public override void Tick()
	{

		var player = Local.Pawn;
		if (player == null) return;

		if(player.Tags.Has("is_holding_fuel"))
        {
			SetClass("active", true);

			SetClass("activeiconbattery", false);
			SetClass("activeiconfuel", true);

			CurrentItem.Text = "FUEL CAN";
		}
		else if (player.Tags.Has("is_holding_battery"))
		{
			SetClass("active", true);

			SetClass("activeiconbattery", true);
			SetClass("activeiconfuel", false);

			CurrentItem.Text = "CAR BATTERY";
		}
        else 
		{
			SetClass("active", false);
		}
	}
}
