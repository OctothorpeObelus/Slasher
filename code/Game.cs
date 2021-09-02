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

        var generator = new GeneratorEntity();
        generator.Position = new Vector3(-202.90f, -3024.05f, 4.03f);
        generator.Spawn();

        player.Respawn();
    }
}
