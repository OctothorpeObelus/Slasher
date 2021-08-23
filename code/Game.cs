using Sandbox;
public partial class Slasher : Sandbox.Game {
    public Slasher() {

    }

    public override void ClientJoined(Client client) {
        base.ClientJoined(client);

        var player = new SurvivorPlayer();
        player.Tags.Add("survivor");
        client.Pawn = player;

        player.Respawn();
    }
}