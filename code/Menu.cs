using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using LobbySlasher;

public class Menu : Panel
{

	//public SlasherLobby Lobby;

	public Label Label;

	public Label SurvivorSpawn;

	public Label SlasherSpawn;

	public Label SpectatorSpawn;

	public Label ReadyConfirm;	

	public Label SlasherName;	

	public Label SlasherDesc;	

	public Label SlasherIcon1;	

	public Label SlasherIcon2;	

	public Label SlasherIcon3;	

	public Label SurvivorIcon1;	

	public Label SurvivorIcon2;	

	public Label SurvivorIcon3;		

	public Label SurvivorIcon4;		


	public bool picked = false;

	public int SelectedSlasher = 1;

	public int SelectedSurvivor = 1;

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

	public bool SelectionSurvivor = true;

	public bool IsThePlayerReady = false;

	public int PlayerCount = 0;

	public Client Owner;

	public Client P1ID;

	public Client P2ID;

	public Client P3ID;

	public Client P4ID;

	public Client P5ID;

	public Menu()
	{
		//Lobby = new SlasherLobby();

		Label = Add.Label("Alpha v0.01", "logo");

		SurvivorSpawn = Add.Label("Play as Survivor", "spawn-survivor");

		SlasherSpawn = Add.Label("Play as Slasher", "spawn-slasher");

		SpectatorSpawn = Add.Label("Spawn (Debug)", "spawn-spectator");

		ReadyConfirm = Add.Label("Ready Up", "ready-confirmation");

		SlasherName = Add.Label("Bababooey", "slasher-name");

		SlasherDesc = Add.Label("Unknown Slasher.", "slasher-description");

		SlasherIcon1 = Add.Label("", "slashericon1");

		SlasherIcon2 = Add.Label("", "slashericon2");

		SlasherIcon3 = Add.Label("", "slashericon3");


		SurvivorIcon1 = Add.Label("", "survivoricon1");

		SurvivorIcon2 = Add.Label("", "survivoricon2");

		SurvivorIcon3 = Add.Label("", "survivoricon3");

		SurvivorIcon4 = Add.Label("", "survivoricon4");


		LobbyBox = Add.Label("Player Lobby", "lobby-box");

			Player1 = LobbyBox.Add.Label("", "player1");
				ReadyP1 = Player1.Add.Label("", "ready");

			Player2 = LobbyBox.Add.Label("", "player2");
				ReadyP2 = Player2.Add.Label("", "ready");

			Player3 = LobbyBox.Add.Label("", "player3");
				ReadyP3 = Player3.Add.Label("", "ready");

			Player4 = LobbyBox.Add.Label("", "player4");
				ReadyP4 = Player4.Add.Label("", "ready");

			Player5 = LobbyBox.Add.Label("", "player5");
				ReadyP5 = Player5.Add.Label("", "ready");

		Label.SetClass("active" , false);

		SurvivorSpawn.AddEventListener( "onclick", () =>
		{

			SelectionSurvivor = true;
			IsThePlayerReady = false;
			

			PlaySound("menu_ready");

			SurvivorSpawn.SetClass("active",true);	
			SlasherSpawn.SetClass("active",false);	

		} );

		SpectatorSpawn.AddEventListener( "onclick", () =>
			{
				picked = true;

				if(SelectionSurvivor == true)
					ConsoleSystem.Run( "spawnsurvivor",SelectedSurvivor);
					
				if(SelectionSurvivor == false)
					ConsoleSystem.Run( "spawnslasher",SelectedSlasher);
		} );

		SlasherSpawn.AddEventListener( "onclick", () =>
		{

			SelectionSurvivor = false;
			IsThePlayerReady = false;

			PlaySound("menu_ready_slasher");

			SurvivorSpawn.SetClass("active",false);	
			SlasherSpawn.SetClass("active",true);

		} );

		ReadyConfirm.AddEventListener( "onclick", () =>
		{

			IsThePlayerReady = !IsThePlayerReady;

			if(IsThePlayerReady)
				PlaySound("menu_confirm");

		} );


		SlasherIcon1.AddEventListener( "onclick", () =>
			{
				SelectedSlasher = 1;
				PlaySound("menu_select");
		} );

		SlasherIcon2.AddEventListener( "onclick", () =>
			{
				SelectedSlasher = 2;
				PlaySound("menu_select");
		} );

		SlasherIcon3.AddEventListener( "onclick", () =>
			{
				SelectedSlasher = 3;
				PlaySound("menu_select");
		} );


		SurvivorIcon1.AddEventListener( "onclick", () =>
			{
				SelectedSurvivor = 1;
				PlaySound("menu_select");
		} );

		SurvivorIcon2.AddEventListener( "onclick", () =>
			{
				SelectedSurvivor = 2;
				PlaySound("menu_select");
		} );

		SurvivorIcon3.AddEventListener( "onclick", () =>
			{
				SelectedSurvivor = 3;
				PlaySound("menu_select");
		} );

		SurvivorIcon4.AddEventListener( "onclick", () =>
			{
				SelectedSurvivor = 4;
				PlaySound("menu_select");
		} );

	}

	public override void Tick()
	{

		//TEST

		ReadyP1.SetClass("survivor",SelectionSurvivor);
		ReadyP1.SetClass("slasher",!SelectionSurvivor);

		ReadyP1.SetClass("confirm",IsThePlayerReady);

		//END TEST

		ReadyConfirm.SetClass("confirm",IsThePlayerReady);
		ReadyConfirm.SetClass("survivor",SelectionSurvivor);
		ReadyConfirm.SetClass("slasher",!SelectionSurvivor);

		if(picked == false) SetClass("active" , true);
		if(picked == true) SetClass("active" , false);

		if(SelectionSurvivor){
			SlasherIcon1.SetClass("active",false);
			SlasherIcon2.SetClass("active",false);
			SlasherIcon3.SetClass("active",false);

			SurvivorIcon1.SetClass("active",true);
			SurvivorIcon2.SetClass("active",true);
			SurvivorIcon3.SetClass("active",true);
			SurvivorIcon4.SetClass("active",true);
		}
		else{
			SlasherIcon1.SetClass("active",true);
			SlasherIcon2.SetClass("active",true);
			SlasherIcon3.SetClass("active",true);

			SurvivorIcon1.SetClass("active",false);
			SurvivorIcon2.SetClass("active",false);
			SurvivorIcon3.SetClass("active",false);
			SurvivorIcon4.SetClass("active",false);
		}

		if(SelectionSurvivor == false){

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
		else{

			switch(SelectedSurvivor)
		{
			case 1:

				SurvivorIcon1.SetClass("selected",true);
				SurvivorIcon2.SetClass("selected",false);
				SurvivorIcon3.SetClass("selected",false);
				SurvivorIcon4.SetClass("selected",false);
				
				SlasherName.Text = "Joe Averidge";

				SlasherDesc.Text = "The Ordinary Man\n\nAs a Slasher Zone Power Recovery worker, your goal is to restore \npower by activating two generators and then escape unscathed. \n\n You will get the chance to choose one item to take before entering the Zone.";

			break;
			case 2:

				SurvivorIcon1.SetClass("selected",false);
				SurvivorIcon2.SetClass("selected",true);
				SurvivorIcon3.SetClass("selected",false);
				SurvivorIcon4.SetClass("selected",false);

				SlasherName.Text = "John Goober";

				SlasherDesc.Text = "The Safety Compliant\n\nAs a Slasher Zone Power Recovery worker, your goal is to restore \npower by activating two generators and then escape unscathed. \n\n You will get the chance to choose one item to take before entering the Zone.";

			break;
			case 3:

				SurvivorIcon1.SetClass("selected",false);
				SurvivorIcon2.SetClass("selected",false);
				SurvivorIcon3.SetClass("selected",true);
				SurvivorIcon4.SetClass("selected",false);

				SlasherName.Text = "Biluy Howard";

				SlasherDesc.Text = "The Thief\n\nAs a Slasher Zone Power Recovery worker, your goal is to restore \npower by activating two generators and then escape unscathed. \n\n You will get the chance to choose one item to take before entering the Zone.";

			break;
			case 4:

				SurvivorIcon1.SetClass("selected",false);
				SurvivorIcon2.SetClass("selected",false);
				SurvivorIcon3.SetClass("selected",false);
				SurvivorIcon4.SetClass("selected",true);

				SlasherName.Text = "Dash Ketchup";

				SlasherDesc.Text = "The Trespasser\n\nAs a Slasher Zone Power Recovery worker, your goal is to restore \npower by activating two generators and then escape unscathed. \n\n You will get the chance to choose one item to take before entering the Zone.";

			break;
		}

		}

		//Player1.Text = Slasher.P1ID.Name;

	}

	public void PlayerJoined(Client client) {

		switch(PlayerCount){
			case 0:
			P1ID = client;
			Event.Run( "player_1", P1ID.Name );
			PlayerCount++;
			Sandbox.Log.Info(P1ID.Name + ", " + PlayerCount);
			break;
			case 1:
			P2ID = client;
			Event.Run( "player_2", P1ID.Name );
			PlayerCount++;
			Sandbox.Log.Info(P1ID.Name + ", " + P2ID.Name + ", " + PlayerCount);
			break;
		}

	}

	[Event( "player_1" )]
	public void Player1Info( string playername ) {
		Player1.Text = playername;
	}

	[Event( "player_2" )]
	public void Player2Info( string playername ) {
		Player2.Text = playername;
	}

	[Event( "player_3" )]
	public void Player3Info( string playername ) {
		Player3.Text = playername;
	}

	[Event( "player_4" )]
	public void Player4Info( string playername ) {
		Player4.Text = playername;
	}

	[Event( "player_5" )]
	public void Player5Info( string playername ) {
		Player5.Text = playername;
	}

}
