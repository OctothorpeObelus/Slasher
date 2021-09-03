using System.Diagnostics.Tracing;
using Sandbox;

public partial class GeneratorEntity : AnimEntity, IUse {

    private bool HasBattery {get; set;} = false;

    public override void Spawn() {
        base.Spawn();

        SetModel("models/generator/generator.vmdl");
        SetupPhysicsFromModel(PhysicsMotionType.Static, false);
        Sequence = "DefaultState";
    }

    public bool IsUsable( Entity user ) {
        return true;
    }

    public bool OnUse( Entity user ) 
	{
        if (Sequence == "BatteryInsert" || Tags.Has("has_battery")) {return false;}

        if ( user is Player player) {
            if (player.Tags.Has("is_holding_battery") && !Tags.Has("has_battery")) {

				//player.SetAnimBool("b_batteryinsertion", true);
				//player.SetAnimBool("b_item_equipped_generic", false);

				player.Tags.Remove("is_holding_battery");
				player.Tags.Remove("has_item");

				player.Tags.Add("active_battery_inserter");


				Sequence = "BatteryInsert";
                Tags.Add("has_battery");
            }
        }

        return false;
    }

    public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData) {
        if (name == "BatteryIn") {
			//Insert the battery into the generator.
			Tags.Add("battery_in");
            Sequence = "DefaultState";
			this.SetBodyGroup("Battery", 1);
		}
	}

    public override void Simulate( Client cl ) {

		base.Simulate(cl);

		if (Tags.Has("battery_in")) {
			this.SetBodyGroup("Battery",1);
        } else {
			this.SetBodyGroup("Battery",0);
        }
    }
}
