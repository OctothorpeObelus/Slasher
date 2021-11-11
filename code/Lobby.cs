using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace LobbySlasher
{
	public class SlasherLobby : Entity
	{
		public int PlayerCount = 0;

		public Client Owner;

		public Client P1ID;

		public Client P2ID;

		public Client P3ID;

		public Client P4ID;

		public Client P5ID;


		public void PlayerJoined(Client client) {
		switch(PlayerCount){
			case 0:
			P1ID = client;
			//Event.Run( "player_1", P1ID.Name );
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

		public void PlayerDisconnect( Client client)	{

		
		}
	}

	
}
