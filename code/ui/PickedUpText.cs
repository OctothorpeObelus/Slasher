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

	public Panel IconMayo;

	public Panel IconMilk;

	public Panel IconBurger;

	private int hideDelay;

	public PickedUpText()
	{
		Label = Add.Label("Holding:", "pickedup");

		CurrentItem = Add.Label("FUEL CAN", "item-name");

		Tooltip = Add.Label("E to use | M2 to drop", "toolt");

		IconFuel = Add.Panel("iconfuel");

		IconBattery = Add.Panel("iconbattery");

		IconMayo = Add.Panel("iconmayo");

		IconMilk = Add.Panel("iconmilk");

		IconBurger = Add.Panel("iconburger");
	}

	public override void Tick()
	{

		var player = Local.Pawn;
		if (player == null) return;

		if(player.Tags.Has("is_holding_fuel"))
        {

			IconFuel.SetClass("active", true);
			IconBattery.SetClass("active", false);
			IconMayo.SetClass("active", false);
			IconMilk.SetClass("active", false);
			IconBurger.SetClass("active", false);

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
			IconMayo.SetClass("active", false);
			IconMilk.SetClass("active", false);
			IconBurger.SetClass("active", false);

			Tooltip.Text = "E to use | M2 to drop";

			CurrentItem.Text = "CAR BATTERY";

			if(Input.Pressed(InputButton.Use))
				hideDelay = 1;

			if(hideDelay < 800)
				hideDelay++;
		}
		else if (player.Tags.Has("is_holding_mayo"))
		{

			IconFuel.SetClass("active", false);
			IconBattery.SetClass("active", false);
			IconMayo.SetClass("active", true);
			IconMilk.SetClass("active", false);
			IconBurger.SetClass("active", false);

			Tooltip.Text = "R to use | M2 to drop";

			CurrentItem.Text = "MAYONNAISE";

			if(Input.Pressed(InputButton.Use))
				hideDelay = 1;

			if(hideDelay < 800)
				hideDelay++;
		}
		else if (player.Tags.Has("is_holding_milk"))
		{

			IconFuel.SetClass("active", false);
			IconBattery.SetClass("active", false);
			IconMayo.SetClass("active", false);
			IconMilk.SetClass("active", true);
			IconBurger.SetClass("active", false);

			Tooltip.Text = "R to use | M2 to drop";

			CurrentItem.Text = "MILK JUG";

			if(Input.Pressed(InputButton.Use))
				hideDelay = 1;

			if(hideDelay < 800)
				hideDelay++;
		}
		else if (player.Tags.Has("is_holding_burger"))
		{

			IconFuel.SetClass("active", false);
			IconBattery.SetClass("active", false);
			IconMayo.SetClass("active", false);
			IconMilk.SetClass("active", false);
			IconBurger.SetClass("active", true);

			Tooltip.Text = "M2 to drop";

			CurrentItem.Text = "BURGER";

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
