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

	public void Simulate(Client cl)
	{

		this.Simulate(cl);

		if (Tags.Has("battery_in"))
		{
			this.SetBodyGroup("Battery", 1);
		}

		if (!Tags.Has("has_fuel") && !Tags.Has("has_battery"))
		{
			Sequence = "DefaultState";
		}
	}

	public bool IsUsable( Entity user ) {
       return true;
    }

    public bool OnUse( Entity user ) 
	{

		if (Tags.Has("being_filled"))
		{
			Sandbox.Log.Info("Fuel do be being poured!");
		}

		if (Tags.Has("has_battery"))
		{
			Sandbox.Log.Info("battery do be being poured!");
		}

		//if (Sequence == "BatteryInsert" || Sequence == "FuelPour" || Tags.Has("being_filled")) {return false;}

		if ( user is Player player) {
            if (player.Tags.Has("is_holding_battery") && !Tags.Has("has_battery")) {

				player.Tags.Remove("is_holding_battery");
				player.Tags.Remove("has_item");

				player.Tags.Add("active_battery_inserter");

				Sequence = "BatteryInsert";
				Tags.Add("has_battery");
			}

			if (player.Tags.Has("is_holding_fuel") && Sequence != "FuelPour")
			{

				player.Tags.Remove("is_holding_fuel");
				player.Tags.Remove("has_item");

				player.Tags.Add("active_fuel_pourer");

				Sequence = "FuelPour";
				Tags.Add("being_filled");
			}

		}

        return false;
    }

	public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData) {
        if (name == "BatteryIn") {

			//Insert the battery into the generator.
			Tags.Add("battery_in");

		}
		if (name == "FuelFilled")
		{
			//Can of Fuel poured.
			Tags.Remove("being_filled");
			Sequence = "DefaultState";
		}
	}
}
