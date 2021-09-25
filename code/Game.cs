using System.Numerics;
using Sandbox;
public partial class Slasher : Sandbox.Game {

    public Slasher() {
		if (IsServer)
		{
			// Create the HUD
			_ = new SlasherHud();

			//Play the ambient sound
			PlaySound("ambient");
		}
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

		var generator = new GeneratorEntity();
        generator.Position = new Vector3(-100f, -3424.05f, 4.03f);
        generator.Spawn();
		generator.Tags.Add("Generator_1");

		var generator2 = new GeneratorEntity();
        generator2.Position = new Vector3(50f, -3424.05f, 4.03f);
        generator2.Spawn();
		generator2.Tags.Add("Generator_2");

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
