using System.Numerics;
using Sandbox;
public partial class Slasher : Sandbox.Game {
    public Slasher() {

    }

    public override void ClientJoined(Client client) {
        base.ClientJoined(client);

		var player = new SurvivorPlayer();
		player.Tags.Add("survivor");
		client.Pawn = player;

		//var player = new SlasherPlayer();
		//player.Tags.Add("slasher");
		//client.Pawn = player;

		var generator = new GeneratorEntity();
        generator.Position = new Vector3(-202.90f, -3024.05f, 4.03f);
        generator.Spawn();
		generator.Tags.Add("Generator_1");

		var generator2 = new GeneratorEntity();
		generator2.Position = new Vector3(-2.90f, -3024.05f, 4.03f);
		generator2.Spawn();
		generator2.Tags.Add("Generator_2");

		var fuelcan = new FuelEntity();
		fuelcan.Position = new Vector3(-272.90f, -3064.05f, 4.03f);
		fuelcan.Spawn();

		var battery = new BatteryEntity();
		battery.Position = new Vector3(-272.90f, -2964.05f, 4.03f);
		battery.Spawn();

		player.Respawn();
    }
}
