using System.Diagnostics.Tracing;
using Sandbox;

public partial class GeneratorEntity : AnimEntity, IUse {

    public static bool HasBattery;

	public static bool IsCurrentlyBeingFilledWithFuel;

	public static bool IsCurrentlyHavingTheBatteryInsertedIntoIt;

	public int fuelIn;

    public override void Spawn() {
        base.Spawn();

        SetModel("models/generator/generator.vmdl");
        SetupPhysicsFromModel(PhysicsMotionType.Static, false);
        Sequence = "DefaultState";

		this.SetBodyGroup("Battery", 0);
	}

	public bool IsUsable( Entity user ) {
       return true;

	   if(HasBattery == true)
				this.SetBodyGroup("Battery", 1);
    }

    public bool OnUse( Entity user ) 
	{

			Sandbox.Log.Info("Does this jank ass code think the battery is being inserted? Answer: " + IsCurrentlyHavingTheBatteryInsertedIntoIt);
			Sandbox.Log.Info("Does this jank ass code think fuel is being poured? Answer: " + IsCurrentlyBeingFilledWithFuel);
			Sandbox.Log.Info("How many fuel cans does this jank ass code think you poured in? Answer: " + fuelIn);

			Sandbox.Log.Info("Does this jank ass code think the battery is nice and cozy inside it's perfect slot? Answer: " + HasBattery);

			if(HasBattery == true)
				this.SetBodyGroup("Battery", 1);


		//if (Sequence == "BatteryInsert" || Sequence == "FuelPour" || Tags.Has("being_filled")) {return false;}

		if ( user is Player player) {
            if (player.Tags.Has("is_holding_battery") && !Tags.Has("has_battery") && IsCurrentlyBeingFilledWithFuel == false  && IsCurrentlyHavingTheBatteryInsertedIntoIt == false) {

				player.Tags.Remove("is_holding_battery");
				player.Tags.Remove("has_item");

				player.Tags.Add("active_battery_inserter");

				Sequence = "BatteryInsert";
				Tags.Add("has_battery");
			}

			if (player.Tags.Has("is_holding_fuel") && fuelIn < 4  && IsCurrentlyBeingFilledWithFuel == false  && IsCurrentlyHavingTheBatteryInsertedIntoIt == false)
			{

				player.Tags.Remove("is_holding_fuel");
				player.Tags.Remove("has_item");

				player.Tags.Add("active_fuel_pourer");

				Sequence = "FuelPour";

				fuelIn++;
			}

		}

        return false;
    }

	public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
	{
		base.OnAnimEventGeneric(name, intData, floatData, vectorData, stringData);

		if (name == "BatteryBegin")
		{
			//Battering beginning insertion.

			IsCurrentlyHavingTheBatteryInsertedIntoIt = true;

			Sandbox.Log.Info("The following has to read true istg: " + IsCurrentlyHavingTheBatteryInsertedIntoIt);

		}

		if (name == "BatteryIn")
		{

			//Insert the battery into the generator.

			Tags.Add("battery_in");

			HasBattery = true;

			Sandbox.Log.Info("battery should be inside for good now");

			IsCurrentlyHavingTheBatteryInsertedIntoIt = false;

			Sandbox.Log.Info("The following should read 'false' if not, there is something wrong with s&box: " + IsCurrentlyHavingTheBatteryInsertedIntoIt);

			if(HasBattery)
				this.SetBodyGroup("Battery", 1);

			Sequence = "DefaultState";

			Sandbox.Log.Info("the following should read 'DefaultState' . if not, fuck me: " + Sequence);

		}

		if (name == "FuelBegin")
		{
			//Can of Fuel now being poured.

			IsCurrentlyBeingFilledWithFuel = true;

			Sandbox.Log.Info("The following has to read true istg: " + IsCurrentlyBeingFilledWithFuel);

		}

		if (name == "FuelFilled")
		{
			//Can of Fuel poured.

			Sandbox.Log.Info("Alright here's where the code is supposed to increase the amound of gas cans poured by 1! Is it right? Answer: " + fuelIn);

			Sequence = "DefaultState";

			Sandbox.Log.Info("the following should read 'DefaultState' . if not, fuck me: " + Sequence);

			IsCurrentlyBeingFilledWithFuel = false;

			Sandbox.Log.Info("The following should read 'false' if not, there is something wrong with s&box: " + IsCurrentlyBeingFilledWithFuel);
		}
	}

	// Simulate() literally just doesn't fucking work. it does not run at all.

	// public override void Simulate(Client cl)
	// {
		
	// 	base.Simulate(cl);

	// 	//if (cl == null) return;
	// 	//if (!IsServer) return;

	// 	if (HasBattery == true)
	// 	{
	// 		Tags.Add("battery_in");
	// 		Sequence = "DefaultState";
	// 		this.SetBodyGroup("Battery", 1);
	// 		Sandbox.Log.Info("Debug 2 !");
	// 	}

	// 	if (Tags.Has("battery_in"))
	// 	{
	// 		//this.SetBodyGroup("Battery", 1);
	// 	}

	// 	if (!Tags.Has("has_fuel") && !Tags.Has("has_battery"))
	// 	{
	// 		Sequence = "DefaultState";
	// 	}
	// }
}
