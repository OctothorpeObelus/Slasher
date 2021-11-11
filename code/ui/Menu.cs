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

	public Client P1;

	public Client P2;

	public Client P3;

	public Client P4;

	public Client P5;

	public static int P1R;
	public static int P2R;
	public static int P3R;
	public static int P4R;
	public static int P5R;

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

		//ReadyP1.SetClass("survivor",SelectionSurvivor);
		//ReadyP1.SetClass("slasher",!SelectionSurvivor);

		//ReadyP1.SetClass("confirm",IsThePlayerReady);

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

		foreach ( var client in Client.All )
			{

			//Player Joined / Left :)))	(this is actullay very good code don't wroryy)	

				if(P1 == null)
					P1 = client;

				if(P1 != client && P1 != null && P2 == null)
					P2 = client;

				if(P1 != null && P2 != client && P2 != null && P3 == null)
					P3 = client;

				if(P1 != null && P2 != null && P3 != client && P3 != null && P4 == null)
					P4 = client;

				if(P1 != null && P2 != null && P3 != null  && P4 != client && P4 != null && P5 == null)
					P5 = client;
			}

		//Player leaving checks uwu

		if(P5 == P4 || P5 == P3 || P5 == P2 || P5 == P1)
			P5 = null;

		if(P4 == P3 || P4 == P2 || P4 == P1)
			P4 = null;

		if(P3 == P2 || P3 == P1)
			P3 = null;

		if(P2 == P1)
			P2 = null;


		if(P1 != null)
			Player1.Text = P1.Name;
		else
			Player1.Text = "";

		if(P2 != null)
			Player2.Text = P2.Name;
		else
			Player2.Text = "";

		if(P3 != null)
			Player3.Text = P3.Name;
		else
			Player3.Text = "";

		if(P4 != null)
			Player4.Text = P4.Name;
		else
			Player4.Text = "";

		if(P5 != null)
			Player5.Text = P5.Name;
		else
			Player5.Text = "";


		//PlayerReady Definitions: 0-freshly joined 1-selected survivor 2-selected slasher 3-ready as survivor 4-ready as slasher

		if(!IsThePlayerReady && P1 == Local.Client && SelectionSurvivor)
			Event.Run("player_1_readiness", 1);

		else if(!IsThePlayerReady && P1 == Local.Client && !SelectionSurvivor)
			Event.Run("player_1_readiness", 2);

		else if(IsThePlayerReady && P1 == Local.Client && SelectionSurvivor)
			Event.Run("player_1_readiness", 3);

		else if(IsThePlayerReady && P1 == Local.Client && !SelectionSurvivor)
			Event.Run("player_1_readiness", 4);

		
		if(!IsThePlayerReady && P1 == Local.Client && SelectionSurvivor)
			Event.Run("player_1_readiness", 1);

		else if(!IsThePlayerReady && P1 == Local.Client && !SelectionSurvivor)
			Event.Run("player_1_readiness", 2);

		else if(IsThePlayerReady && P1 == Local.Client && SelectionSurvivor)
			Event.Run("player_1_readiness", 3);

		else if(IsThePlayerReady && P1 == Local.Client && !SelectionSurvivor)
			Event.Run("player_1_readiness", 4);



		if(!IsThePlayerReady && P2 == Local.Client && SelectionSurvivor)
			Event.Run("player_2_readiness", 1);

		else if(!IsThePlayerReady && P3 == Local.Client && !SelectionSurvivor)
			Event.Run("player_3_readiness", 2);

		else if(IsThePlayerReady && P3 == Local.Client && SelectionSurvivor)
			Event.Run("player_3_readiness", 3);

		else if(IsThePlayerReady && P3 == Local.Client && !SelectionSurvivor)
			Event.Run("player_3_readiness", 4);



		if(!IsThePlayerReady && P4 == Local.Client && SelectionSurvivor)
			Event.Run("player_4_readiness", 1);

		else if(!IsThePlayerReady && P4 == Local.Client && !SelectionSurvivor)
			Event.Run("player_4_readiness", 2);

		else if(IsThePlayerReady && P4 == Local.Client && SelectionSurvivor)
			Event.Run("player_4_readiness", 3);

		else if(IsThePlayerReady && P4 == Local.Client && !SelectionSurvivor)
			Event.Run("player_4_readiness", 4);



		if(!IsThePlayerReady && P5 == Local.Client && SelectionSurvivor)
			Event.Run("player_5_readiness", 1);

		else if(!IsThePlayerReady && P5 == Local.Client && !SelectionSurvivor)
			Event.Run("player_5_readiness", 2);

		else if(IsThePlayerReady && P5 == Local.Client && SelectionSurvivor)
			Event.Run("player_5_readiness", 3);

		else if(IsThePlayerReady && P5 == Local.Client && !SelectionSurvivor)
			Event.Run("player_5_readiness", 4);

		switch(P1R)
		{
			case 0:
			ReadyP1.SetClass("survivor",false);
			ReadyP1.SetClass("slasher",false);

			ReadyP1.SetClass("confirm",false);
			break;
			case 1:
			ReadyP1.SetClass("survivor",true);
			ReadyP1.SetClass("slasher",false);

			ReadyP1.SetClass("confirm",false);
			break;
			case 2:
			ReadyP1.SetClass("survivor",false);
			ReadyP1.SetClass("slasher",true);

			ReadyP1.SetClass("confirm",false);
			break;
			case 3:
			ReadyP1.SetClass("survivor",true);
			ReadyP1.SetClass("slasher",false);

			ReadyP1.SetClass("confirm",true);
			break;
			case 4:
			ReadyP1.SetClass("survivor",false);
			ReadyP1.SetClass("slasher",true);

			ReadyP1.SetClass("confirm",true);
			break;
		}


		switch(P2R)
		{
			case 0:
			ReadyP2.SetClass("survivor",false);
			ReadyP2.SetClass("slasher",false);

			ReadyP2.SetClass("confirm",false);
			break;
			case 1:
			ReadyP2.SetClass("survivor",true);
			ReadyP2.SetClass("slasher",false);

			ReadyP2.SetClass("confirm",false);
			break;
			case 2:
			ReadyP2.SetClass("survivor",false);
			ReadyP2.SetClass("slasher",true);

			ReadyP2.SetClass("confirm",false);
			break;
			case 3:
			ReadyP2.SetClass("survivor",true);
			ReadyP2.SetClass("slasher",false);

			ReadyP2.SetClass("confirm",true);
			break;
			case 4:
			ReadyP2.SetClass("survivor",false);
			ReadyP2.SetClass("slasher",true);

			ReadyP2.SetClass("confirm",true);
			break;
		}


		switch(P3R)
		{
			case 0:
			ReadyP3.SetClass("survivor",false);
			ReadyP3.SetClass("slasher",false);

			ReadyP3.SetClass("confirm",false);
			break;
			case 1:
			ReadyP3.SetClass("survivor",true);
			ReadyP3.SetClass("slasher",false);

			ReadyP3.SetClass("confirm",false);
			break;
			case 2:
			ReadyP3.SetClass("survivor",false);
			ReadyP3.SetClass("slasher",true);

			ReadyP3.SetClass("confirm",false);
			break;
			case 3:
			ReadyP3.SetClass("survivor",true);
			ReadyP3.SetClass("slasher",false);

			ReadyP3.SetClass("confirm",true);
			break;
			case 4:
			ReadyP3.SetClass("survivor",false);
			ReadyP3.SetClass("slasher",true);

			ReadyP3.SetClass("confirm",true);
			break;
		}



		switch(P4R)
		{
			case 0:
			ReadyP4.SetClass("survivor",false);
			ReadyP4.SetClass("slasher",false);

			ReadyP4.SetClass("confirm",false);
			break;
			case 1:
			ReadyP4.SetClass("survivor",true);
			ReadyP4.SetClass("slasher",false);

			ReadyP4.SetClass("confirm",false);
			break;
			case 2:
			ReadyP4.SetClass("survivor",false);
			ReadyP4.SetClass("slasher",true);

			ReadyP4.SetClass("confirm",false);
			break;
			case 3:
			ReadyP4.SetClass("survivor",true);
			ReadyP4.SetClass("slasher",false);

			ReadyP4.SetClass("confirm",true);
			break;
			case 4:
			ReadyP4.SetClass("survivor",false);
			ReadyP4.SetClass("slasher",true);

			ReadyP4.SetClass("confirm",true);
			break;
		}


		switch(P5R)
		{
			case 0:
			ReadyP5.SetClass("survivor",false);
			ReadyP5.SetClass("slasher",false);

			ReadyP5.SetClass("confirm",false);
			break;
			case 1:
			ReadyP5.SetClass("survivor",true);
			ReadyP5.SetClass("slasher",false);

			ReadyP5.SetClass("confirm",false);
			break;
			case 2:
			ReadyP5.SetClass("survivor",false);
			ReadyP5.SetClass("slasher",true);

			ReadyP5.SetClass("confirm",false);
			break;
			case 3:
			ReadyP5.SetClass("survivor",true);
			ReadyP5.SetClass("slasher",false);

			ReadyP5.SetClass("confirm",true);
			break;
			case 4:
			ReadyP5.SetClass("survivor",false);
			ReadyP5.SetClass("slasher",true);

			ReadyP5.SetClass("confirm",true);
			break;
		}
		
	}	

	[Event("player_1_readiness")]
	public static void Player1ReadyState(int state){	
		P1R = state;
	}
	[Event("player_2_readiness")]
	public static void Player2ReadyState(int state){	
		P2R = state;
	}
	[Event("player_3_readiness")]
	public static void Player3ReadyState(int state){	
		P3R = state;
	}
	[Event("player_4_readiness")]
	public static void Player4ReadyState(int state){	
		P4R = state;
	}
	[Event("player_5_readiness")]
	public static void Player5ReadyState(int state){	
		P5R = state;
	}

}
