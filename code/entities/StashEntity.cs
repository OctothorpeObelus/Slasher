using System.Diagnostics.Tracing;
using Sandbox;

public partial class StashEntity : AnimEntity, IUse {

	public static int WardsLeft = 3;

    public override void Spawn() {
        base.Spawn();

        SetModel("models/itemstash/ItemStash.vmdl");
        SetupPhysicsFromModel(PhysicsMotionType.Static, false);

		this.SetBodyGroup("Wards", 3);

	}

	public bool IsUsable( Entity user ) {
       return true;
    }

    public bool OnUse( Entity user ) 
	{

		if ( user is Player player) {

            if (player.Tags.Has("survivor") && !player.Tags.Has("choosing_item") && !player.Tags.Has("has_deathward")) {

				player.Tags.Add("choosing_item");

			}

		}

        return false;
    }

	[Event.Tick.Server]
	protected void Tick()
	{
		this.SetBodyGroup("Wards", WardsLeft);
	}

	[ServerCmd( "store_ward" )]
	public static void StoreWard()
	{
		if ( ConsoleSystem.Caller == null )
			return;

		WardsLeft--;

		ConsoleSystem.Caller.Pawn.Tags.Remove("choosing_item");
		ConsoleSystem.Caller.Pawn.Tags.Add("has_deathward");

	}

}
