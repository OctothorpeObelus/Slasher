using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class Logo : Panel
{
	public Label Label;

	public Label SurvivorSpawn;

	public Label SlasherSpawn;

	public bool picked = false;

	public Logo()
	{
		Label = Add.Label("Alpha v0.01", "Logo");

		SurvivorSpawn = Add.Label("Spawn as Survivor", "spawn-survivor");

		SlasherSpawn = Add.Label("Spawn as Slasher", "spawn-slasher");

		Label.SetClass("active" , false);
	}

	public override void Tick()
	{
		/*

		if(picked == false) SetClass("active" , true);
		if(picked == true) SetClass("active" , false);

		SurvivorSpawn.AddEventListener( "onclick", () =>
			{
				picked = true;

				foreach ( var client in Client.All )
				{
					var player = new SurvivorPlayer();
					player.Tags.Add("survivor");
					client.Pawn = player;

    				player.Respawn();
					
				}
		} );

		SlasherSpawn.AddEventListener( "onclick", () =>
			{
				picked = true;

				foreach ( var client in Client.All )
				{

					var player = new SlasherPlayer();
					player.Tags.Add("slasher");
					client.Pawn = player;

    				player.Respawn();

					
				}
		} );
		*/
	}
}
