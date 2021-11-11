using System.Numerics;
using System;
using Sandbox;
using LobbySlasher;

public partial class Slasher : Sandbox.Game {

	public bool spawned = false;

	public static AnimEntity gene1;

	public static AnimEntity gene2;

	public bool ExitOpen;

	public static AnimEntity exit1;
	
	public static AnimEntity exit2;

	public static AnimEntity exit3;

	public static AnimEntity exit4;

	public int OpenExit1;

	public int OpenExit2;

	public int PlayerCount;

	public SlasherLobby GameLobby;

    public Slasher() {
		if (IsServer)
		{
			// Create the HUD
			_ = new SlasherHud();

			//Play the ambient sound
			//PlaySound("ambient");

		}

		OpenExit1 = new Random().Next(1, 4);

		OpenExit2 = new Random().Next(1, 4);


		BeginSpawn();

	}

	public static void BeginSpawn()	
	{
		gene1 = new GeneratorEntity();
		gene1.Position = new Vector3(130f, 570f, 16f);
		gene1.Rotation = new Angles(0f, 90f, 0f).ToRotation();
        gene1.Spawn();
		gene1.Tags.Add("Generator_1");

		if(gene1.IsServer == false)
			gene1.Delete();

		gene2 = new GeneratorEntity();
		gene2.Position = new Vector3(-600f, 1080f, 16f);
		gene2.Rotation = new Angles(0f, -90f, 0f).ToRotation();
        gene2.Spawn();
		gene2.Tags.Add("Generator_2");

		if(gene2.IsServer == false)
			gene2.Delete();

		exit1 = new ExitEntity();
		exit1.Position = new Vector3(-500f, 350f, 16f);
		exit1.Rotation = new Angles(0f, 0f, 0f).ToRotation();
		exit1.Tags.Add("Exit_1");

		if(exit1.IsServer == false)
			exit1.Delete();

		exit2 = new ExitEntity();
		exit2.Position = new Vector3(-500f, 420f, 16f);
		exit2.Rotation = new Angles(0f, 0f, 0f).ToRotation();
        exit2.Spawn();
		exit2.Tags.Add("Exit_2");

		if(exit2.IsServer == false)
			exit2.Delete();

		exit3 = new ExitEntity();
		exit3.Position = new Vector3(-500f, 490f, 16f);
		exit3.Rotation = new Angles(0f, 0f, 0f).ToRotation();
        exit3.Spawn();
		exit3.Tags.Add("Exit_3");

		if(exit3.IsServer == false)
			exit3.Delete();

		exit4 = new ExitEntity();
		exit4.Position = new Vector3(-500f, 560f, 16f);
		exit4.Rotation = new Angles(0f, 0f, 0f).ToRotation();
        exit4.Spawn();
		exit4.Tags.Add("Exit_4");

		if(exit4.IsServer == false)
			exit4.Delete();

		var stash = new StashEntity();
		stash.Position = new Vector3(-0f, 0f, 14f);
		stash.Rotation = new Angles(0f, 0f, 0f).ToRotation();
        stash.Spawn();

		if(stash.IsServer == false)
			stash.Delete();


		//GameLobby = new SlasherLobby();

	}

   public override void ClientJoined(Client client) {
        base.ClientJoined(client);

		if (IsServer)
		{
			//Play the ambient sound
			PlaySound("ambient");
		}

		GameLobby = new SlasherLobby();

		GameLobby.Owner = client;

		GameLobby.PlayerJoined(client);
    }

	public override void ClientDisconnect( Client client, NetworkDisconnectionReason reason )	{

		base.ClientDisconnect( client, reason );

		GameLobby.PlayerDisconnect(client);

	}

	public override void Simulate(Client cl)
	{
		base.Simulate(cl);

		if(OpenExit2 == OpenExit1)
			OpenExit2 = new Random().Next(1, 4);

		//if(gene1.Tags.Has("running") && gene2.Tags.Has("running"))
		if(gene1.Tags.Has("running") || gene2.Tags.Has("running"))
		{
			ExitOpen = true;
		}

		if(ExitOpen == true){
			switch(OpenExit1)
			{
				case 1:
				exit1.Tags.Add("open");
				break;
				case 2:
				exit2.Tags.Add("open");
				break;
				case 3:
				exit3.Tags.Add("open");
				break;
				case 4:
				exit4.Tags.Add("open");
				break;
			}
			switch(OpenExit2)
			{
				case 1:
				exit1.Tags.Add("open");
				break;
				case 2:
				exit2.Tags.Add("open");
				break;
				case 3:
				exit3.Tags.Add("open");
				break;
				case 4:
				exit4.Tags.Add("open");
				break;
			}
	
		}

	}

	[ServerCmd( "spawnsurvivor" )]
	public static void SpawnSurvivor(int SelectedSurvivor)
	{
		if ( ConsoleSystem.Caller == null )
			return;

		Sandbox.Log.Info("Spawning as Survivor...");

		var player = new SurvivorPlayer();
		player.Tags.Add("survivor");
		ConsoleSystem.Caller.Pawn = player;

		player.SelectSurvivor(SelectedSurvivor);

		player.Respawn();
	}

	[ServerCmd( "spawnslasher" )]
	public static void SpawnSlasher(int SlasherSelection)
	{
		if ( ConsoleSystem.Caller == null )
			return;

		Sandbox.Log.Info("Spawning as Slasher...");

		var player = new SlasherPlayer();
		player.Tags.Add("slasher");
		ConsoleSystem.Caller.Pawn = player;

		player.SelectSlasher(SlasherSelection);

		player.Respawn();
	}
	[ServerCmd( "spawnspectator" )]
	public static void SpawnSpectator()
	{
		if ( ConsoleSystem.Caller == null )
			return;

		Sandbox.Log.Info("Now Spectating...");

		var player = new SpectatorPlayer();
		player.Tags.Add("spectator");
		ConsoleSystem.Caller.Pawn = player;

		player.Respawn();
	}

	[ServerCmd( "store_fuel" )]
	public static void StoreFuel()
	{
		if ( ConsoleSystem.Caller == null )
			return;

		ConsoleSystem.Caller.Pawn.Tags.Remove("choosing_item");
		ConsoleSystem.Caller.Pawn.Tags.Add("is_storing_fuel");
	}

	[ServerCmd( "store_milk" )]
	public static void StoreMilk()
	{
		if ( ConsoleSystem.Caller == null )
			return;

		ConsoleSystem.Caller.Pawn.Tags.Remove("choosing_item");
		ConsoleSystem.Caller.Pawn.Tags.Add("is_storing_milk");
	}

	[ServerCmd( "store_mayo" )]
	public static void StoreMayo()
	{
		if ( ConsoleSystem.Caller == null )
			return;

		ConsoleSystem.Caller.Pawn.Tags.Remove("choosing_item");
		ConsoleSystem.Caller.Pawn.Tags.Add("is_storing_mayo");
	}

	[ServerCmd( "stop_choosing" )]
	public static void StopChoose()
	{
		if ( ConsoleSystem.Caller == null )
			return;

		ConsoleSystem.Caller.Pawn.Tags.Remove("choosing_item");
	}

	[ServerCmd( "cancel_ward" )]
	public static void CancelWard()
	{
		if ( ConsoleSystem.Caller == null )
			return;

		ConsoleSystem.Caller.Pawn.Tags.Remove("has_deathward");
	}

}
