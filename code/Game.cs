using System.Numerics;
using Sandbox;
public partial class Slasher : Sandbox.Game {

	public static AnimEntity gene1;

	public static AnimEntity gene2;

	public bool ExitOpen;

	public static AnimEntity exit1;

	public static AnimEntity exit2;

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

		gene2 = new GeneratorEntity();
		gene2.Position = new Vector3(50f, -3424.05f, 4.03f);
        gene2.Spawn();
		gene2.Tags.Add("Generator_2");

	}

    public override void ClientJoined(Client client) {
        base.ClientJoined(client);

		var player = new SurvivorPlayer();
		player.Tags.Add("survivor");
		client.Pawn = player;

		//var player = new SlasherPlayer();
		//player.Tags.Add("slasher");
		//client.Pawn = player;

		player.Respawn();
		/*
		var generator = new GeneratorEntity();
        generator.Position = new Vector3(-100f, -3424.05f, 4.03f);
        generator.Spawn();
		generator.Tags.Add("Generator_1");
		gene1 = generator;

		var generator2 = new GeneratorEntity();
        generator2.Position = new Vector3(50f, -3424.05f, 4.03f);
        generator2.Spawn();
		generator2.Tags.Add("Generator_2");
		gene2 = generator2;
		*/

		var fuelcan = new FuelEntity();
		fuelcan.Position = new Vector3(-272.90f, -3064.05f, 4.03f);
		fuelcan.Spawn();

		var battery = new BatteryEntity();
		battery.Position = new Vector3(-272.90f, -2964.05f, 4.03f);
		battery.Spawn();

		if (IsServer)
		{
			//Play the ambient sound
			PlaySound("ambient");
		}
    }

	public override void Simulate(Client cl)
	{
		base.Simulate(cl);

		if(gene1.Tags.Has("running") && gene2.Tags.Has("running"))
			ExitOpen = true;

	}

	/*
	public SpawnSurvivor()
	{
		foreach ( var client in Client.All )
			{
				var player = new SurvivorPlayer();
				player.Tags.Add("survivor");
				client.Pawn = player;

    			player.Respawn();
					
			}
	}

	public SpawnSlasher()
	{
		foreach ( var client in Client.All )
			{
				var player = new SlasherPlayer();
				player.Tags.Add("slasher");
				client.Pawn = player;

    			player.Respawn();
					
			}
	}
	*/
}
