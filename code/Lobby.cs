using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class Lobby : Entity
{

	public Client LobbyHost; 

	public int PlayerCount;

	private string P1Name;

	private string P2Name;

	private string P3Name;

	private string P4Name;

	private string P5Name;

	public Lobby()
	{
		LobbyHost = GetClientOwner();
	}

	public void PlayerJoined(Client client) {
		if(IsServer){

		switch (PlayerCount)
		{
			case 0:
			Player1Inform( client.Name );
			P1Name = client.Name;
			break;
			case 1:
			Event.Run("player_2", client.Name);
			P2Name = client.Name;
			break;
			case 2:
			Event.Run("player_3", client.Name);
			P3Name = client.Name;
			break;
			case 3:
			Event.Run("player_4", client.Name);
			P4Name = client.Name;
			break;
			case 4:
			Event.Run("player_5", client.Name);
			P5Name = client.Name;
			break;			
		}
		if(PlayerCount>5){
			Sandbox.Log.Info("CRITICAL ERROR. PLAYER OVERLOAD.");
		}

		PlayerCount++;

		Sandbox.Log.Info("Players: " + PlayerCount);

		}

    }

	public void PlayerDisconnect( Client client)	{

		if(IsServer){

		if(client.Name == P1Name){
			Event.Run("player_1", P2Name);
			Event.Run("player_2", P3Name);
			Event.Run("player_3", P4Name);
			Event.Run("player_4", P5Name);
			Event.Run("player_5", "");

			P1Name = P2Name;
			P2Name = P3Name;
			P3Name = P4Name;
			P4Name = P5Name;
			P5Name = "";
		}
		if(client.Name == P2Name){
			Event.Run("player_2", P3Name);
			Event.Run("player_3", P4Name);
			Event.Run("player_4", P5Name);
			Event.Run("player_5", "");

			P2Name = P3Name;
			P3Name = P4Name;
			P4Name = P5Name;
			P5Name = "";
		}
		if(client.Name == P3Name){
			Event.Run("player_3", P4Name);
			Event.Run("player_4", P5Name);
			Event.Run("player_5", "");

			P3Name = P4Name;
			P4Name = P5Name;
			P5Name = "";
		}
		if(client.Name == P4Name){
			Event.Run("player_4", P5Name);
			Event.Run("player_5", "");

			P4Name = P5Name;
			P5Name = "";
		}
		if(client.Name == P5Name){
			Event.Run("player_5", "");

			P5Name = "";
		}

		PlayerCount--;

		}
	}

	public void Player1Inform( string playername )
	{
    Event.Run( "player_1", playername );
	}
}
