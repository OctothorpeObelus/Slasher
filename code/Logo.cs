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

		SurvivorSpawn.AddEventListener( "onclick", () =>
			{
				picked = true;

				ConsoleSystem.Run( "spawnsurvivor");					
		} );

		SlasherSpawn.AddEventListener( "onclick", () =>
			{
				picked = true;

				ConsoleSystem.Run( "spawnslasher");
		} );
	}

	public override void Tick()
	{

		if(picked == false) SetClass("active" , true);
		if(picked == true) SetClass("active" , false);

	}
}
