using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class Menu : Panel
{
	public Label Label;

	public Label SurvivorSpawn;

	public Label SlasherSpawn;

	public Label SlasherName;	

	public Label SlasherDesc;	

	public Label SlasherIcon1;	

	public Label SlasherIcon2;	

	public Label SlasherIcon3;	

	public bool picked = false;

	public int SelectedSlasher = 1;

	public Menu()
	{
		Label = Add.Label("Alpha v0.01", "Logo");

		SurvivorSpawn = Add.Label("Spawn as Survivor", "spawn-survivor");

		SlasherSpawn = Add.Label("Spawn as Slasher", "spawn-slasher");

		SlasherName = Add.Label("Bababooey", "slasher-name");

		SlasherDesc = Add.Label("Unknown Slasher.", "slasher-description");

		SlasherIcon1 = Add.Label("", "slashericon1");

		SlasherIcon2 = Add.Label("", "slashericon2");

		SlasherIcon3 = Add.Label("", "slashericon3");

		Label.SetClass("active" , false);

		SurvivorSpawn.AddEventListener( "onclick", () =>
			{
				picked = true;

				ConsoleSystem.Run( "spawnsurvivor");			
		} );

		SlasherSpawn.AddEventListener( "onclick", () =>
		{
			picked = true;

			switch(SelectedSlasher) {
				case 1:
					ConsoleSystem.Run( "slasher_1");
				break;
				case 2:
					ConsoleSystem.Run( "slasher_2");
				break;
				case 3:
					ConsoleSystem.Run( "slasher_3");
				break;
			}	

			ConsoleSystem.Run( "spawnslasher");


		} );
		SlasherIcon1.AddEventListener( "onclick", () =>
			{
				SelectedSlasher = 1;
		} );

		SlasherIcon2.AddEventListener( "onclick", () =>
			{
				SelectedSlasher = 2;
		} );

		SlasherIcon3.AddEventListener( "onclick", () =>
			{
				SelectedSlasher = 3;
		} );

	}

	public override void Tick()
	{

		if(picked == false) SetClass("active" , true);
		if(picked == true) SetClass("active" , false);

		switch(SelectedSlasher)
		{
			case 1:

				SlasherIcon1.SetClass("selected",true);
				SlasherIcon2.SetClass("selected",false);
				SlasherIcon3.SetClass("selected",false);
				
				SlasherName.Text = "Bababooey";

				SlasherDesc.Text = "A phantom slasher which can turn invisible. \n \n • Bababooey can turn himself invisible at any time, but survivors too close to him \nwhile invisible will be able to see his veil.\n\n• There is a short delay between turning visible and being able to kill survivors.\n\n• Bababooey can place phantom clones of himself to fool survivors.";

			break;
			case 2:

				SlasherIcon1.SetClass("selected",false);
				SlasherIcon2.SetClass("selected",true);
				SlasherIcon3.SetClass("selected",false);

				SlasherName.Text = "Amogus";

				SlasherDesc.Text = "An imposter slasher which can assume the form of a human or Fuel Can.\n\n• Amogus emits loud noises while sprinting quickly.\n\n• It can take the form of a Fuel Can, or a survivor. Use this ability to deceive \nand catch your victims off-guard.";

			break;
			case 3:

				SlasherIcon1.SetClass("selected",false);
				SlasherIcon2.SetClass("selected",false);
				SlasherIcon3.SetClass("selected",true);

				SlasherName.Text = "Trollge";

				SlasherDesc.Text = "A bloodthirsty slasher which gains power over the course of the round. \nHe has three distinct Phases.\n\n• Trollge gains more and more power as the round progresses.\n\n• If he is too weak, he is unable to see what is not moving.\n\n• Trollge's true form is incredibly powerful, but survivors can see it coming from afar.";

			break;
		}

	}
}
