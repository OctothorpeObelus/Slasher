using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class ItemPicker : Panel
{

	public bool Choosing = false;

	public Label Label;

	public Label Description;

	public Label Confirm;

	public Label Cancel;

	public Label Item1;

	public Label Item2;

	public Label Item3;

	public Label Item4;

	public int ItemSelected;

	public static int WardsLeft = 3;

	public ItemPicker()
	{
		Label = Add.Label("Choose an item to store", "ItemPicker");

		Description = Add.Label("Pick an item.", "desc");

		Confirm = Add.Label("Confirm", "confirm");

		Cancel = Add.Label("Cancel", "cancel");

		Item1 = Add.Label("", "item1");

		Item2 = Add.Label("", "item2");

		Item3 = Add.Label("", "item3");

		Item4 = Add.Label("", "item4");

		Item1.AddEventListener( "onclick", () =>
		{

			ItemSelected = 1;
			PlaySound("menu_select");

			Item1.SetClass("selected",true);
			Item2.SetClass("selected",false);
			Item3.SetClass("selected",false);
			Item4.SetClass("selected",false);

		} );
		Item2.AddEventListener( "onclick", () =>
		{

			ItemSelected = 2;
			PlaySound("menu_select");

			Item1.SetClass("selected",false);
			Item2.SetClass("selected",true);
			Item3.SetClass("selected",false);
			Item4.SetClass("selected",false);

		} );
		Item3.AddEventListener( "onclick", () =>
		{

			ItemSelected = 3;
			PlaySound("menu_select");

			Item1.SetClass("selected",false);
			Item2.SetClass("selected",false);
			Item3.SetClass("selected",true);
			Item4.SetClass("selected",false);

		} );
		Item4.AddEventListener( "onclick", () =>
		{

			ItemSelected = 4;
			PlaySound("menu_select");

			Item1.SetClass("selected",false);
			Item2.SetClass("selected",false);
			Item3.SetClass("selected",false);
			Item4.SetClass("selected",true);

		} );

		Confirm.AddEventListener( "onclick", () =>
		{

			switch(ItemSelected){
				case 1:
				PlaySound("menu_ready");
				ConsoleSystem.Run("store_fuel");
				if(Local.Pawn.Tags.Has("has_deathward")){
					ConsoleSystem.Run("cancel_ward");
					WardsLeft++;
				}
				break;
				case 2:
				PlaySound("menu_ready");
				ConsoleSystem.Run("store_milk");
				Choosing = false;
				break;
				case 3:
				PlaySound("menu_ready");
				ConsoleSystem.Run("store_mayo");
				Choosing = false;
				break;
				case 4:
				if(WardsLeft>0 && !Local.Pawn.Tags.Has("has_deathward")){
					PlaySound("menu_ready");
					PlaySound("deathward");
					ConsoleSystem.Run("store_ward");
					WardsLeft--;
				}
				else{
					PlaySound("menu_ready_slasher");
				}
				break;
			}


		} );

		Cancel.AddEventListener( "onclick", () =>
		{

			ConsoleSystem.Run("stop_choosing");

		} );
	}

	public override void Tick()
	{

		var player = Local.Pawn;
		if (player == null) return;

		SetClass("active",Choosing);

		Choosing = player.Tags.Has("choosing_item");

		switch(ItemSelected){
			case 1:
			Description.SetText("Fuel Can\n\nA jerry can filled with high-octane gas.\nIt will help advance the main objective.\nTaking it with you will decrease the amount of fuel \nyou will find within the Slasher Zone.");
			break;
			case 2:
			Description.SetText("Milk Jug\n\nA gallon of fresh milk.\nDrinking it will grant you a boost in speed.\nCan also be used to discract a certain Slasher.");
			break;
			case 3:
			Description.SetText("Mayonnaise\n\nA jar of high-quality mayonnaise.\nWill heal you greatly upon consumption.\nCan also be used to discract a certain Slasher.");
			break;
			case 4:
			Description.SetText("The Deathward\n\nA ceramic charm in the shape of a skull.\nWill let you escape death once.\nThere is a limited amount.\nDeathwards left: "+ WardsLeft);
			break;
		}

		if(WardsLeft<1)
			Item4.SetClass("depleted",true);

	}
}
