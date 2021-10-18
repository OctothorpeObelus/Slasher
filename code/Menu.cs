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

	public Label LobbyBox;

	public Label Player1;
	public Label ReadyP1;	

	public Label Player2;
	public Label ReadyP2;	

	public Label Player3;
	public Label ReadyP3;	

	public Label Player4;
	public Label ReadyP4;	

	public Label Player5;
	public Label ReadyP5;	

	public Menu()
	{

		Label = Add.Label("Alpha v0.01", "logo");

		SurvivorSpawn = Add.Label("Play as Survivor", "spawn-survivor");

		SlasherSpawn = Add.Label("Play as Slasher", "spawn-slasher");

		SlasherName = Add.Label("Bababooey", "slasher-name");

		SlasherDesc = Add.Label("Unknown Slasher.", "slasher-description");

		SlasherIcon1 = Add.Label("", "slashericon1");

		SlasherIcon2 = Add.Label("", "slashericon2");

		SlasherIcon3 = Add.Label("", "slashericon3");

		LobbyBox = Add.Label("Player Lobby", "lobby-box");

			Player1 = LobbyBox.Add.Label("Player 1", "player1");
				ReadyP1 = Player1.Add.Label("X", "ready");

			Player2 = LobbyBox.Add.Label("Player 2", "player2");
				ReadyP2 = Player2.Add.Label("X", "ready");

			Player3 = LobbyBox.Add.Label("Player 3", "player3");
				ReadyP3 = Player3.Add.Label("X", "ready");

			Player4 = LobbyBox.Add.Label("Player 4", "player4");
				ReadyP4 = Player4.Add.Label("X", "ready");

			Player5 = LobbyBox.Add.Label("Player 5", "player5");
				ReadyP5 = Player5.Add.Label("X", "ready");

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
