using System.Numerics;
using System;
using Sandbox;
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

    public Slasher() {
		if (IsServer)
		{
			// Create the HUD
			_ = new SlasherHud();

			//Play the ambient sound
			PlaySound("ambient");

		}

		gene1 = new GeneratorEntity();
		gene1.Position = new Vector3(-100f, -3424.05f, 4.03f);
        gene1.Spawn();
		gene1.Tags.Add("Generator_1");

		if(gene1.IsServer == false)
			gene1.Delete();

		gene2 = new GeneratorEntity();
		gene2.Position = new Vector3(50f, -3424.05f, 4.03f);
        gene2.Spawn();
		gene2.Tags.Add("Generator_2");

		if(gene2.IsServer == false)
			gene2.Delete();

		exit1 = new ExitEntity();
		exit1.Position = new Vector3(-240f, -3350f, 4.03f);
        exit1.Spawn();
		exit1.Tags.Add("Exit_1");

		if(exit1.IsServer == false)
			exit1.Delete();

		exit2 = new ExitEntity();
		exit2.Position = new Vector3(-240f, -3300f, 4.03f);
        exit2.Spawn();
		exit2.Tags.Add("Exit_1");

		if(exit2.IsServer == false)
			exit2.Delete();

		exit3 = new ExitEntity();
		exit3.Position = new Vector3(-240f, -3250f, 4.03f);
        exit3.Spawn();
		exit3.Tags.Add("Exit_1");

		if(exit3.IsServer == false)
			exit3.Delete();

		exit4 = new ExitEntity();
		exit4.Position = new Vector3(-240f, -3200f, 4.03f);
        exit4.Spawn();
		exit4.Tags.Add("Exit_1");

		if(exit4.IsServer == false)
			exit4.Delete();

		OpenExit1 = new Random().Next(1, 4);

		OpenExit2 = new Random().Next(1, 4);

	}

    public override void ClientJoined(Client client) {
        base.ClientJoined(client);

		if(spawned == false)
		{

		var fuelcan = new FuelEntity();
		fuelcan.Position = new Vector3(-272.90f, -3064.05f, 4.03f);
		fuelcan.Spawn();

		var battery = new BatteryEntity();
		battery.Position = new Vector3(-272.90f, -2964.05f, 4.03f);
		battery.Spawn();

		spawned = true;

		}


		if (IsServer)
		{
			//Play the ambient sound
			PlaySound("ambient");
		}
    }

	public override void Simulate(Client cl)
	{
		base.Simulate(cl);

		if(OpenExit2 == OpenExit1)
			OpenExit2 = new Random().Next(1, 4);

		//if(gene1.Tags.Has("running") && gene2.Tags.Has("running"))
		if(gene1.Tags.Has("running") || gene2.Tags.Has("running"))
			ExitOpen = true;

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
	public static void SpawnSurvivor()
	{
		if ( ConsoleSystem.Caller == null )
			return;

		Sandbox.Log.Info("Spawning as Survivor...");

		var player = new SurvivorPlayer();
		player.Tags.Add("survivor");
		ConsoleSystem.Caller.Pawn = player;

		player.Respawn();
	}

	[ServerCmd( "spawnslasher" )]
	public static void SpawnSlasher()
	{
		if ( ConsoleSystem.Caller == null )
			return;

		Sandbox.Log.Info("Spawning as Slasher...");

		var player = new SlasherPlayer();
		player.Tags.Add("slasher");
		ConsoleSystem.Caller.Pawn = player;

		player.Respawn();
	}
}
