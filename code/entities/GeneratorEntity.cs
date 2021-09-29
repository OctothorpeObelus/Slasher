using System.Diagnostics.Tracing;
using Sandbox;

public partial class GeneratorEntity : AnimEntity, IUse {

    public bool HasBattery = true;

	public bool IsCurrentlyBeingFilledWithFuel;

	public bool IsCurrentlyHavingTheBatteryInsertedIntoIt;

	public int fuelIn;

	public int fuelTime;

	public int batteryTime;

	public int startTime;

    public override void Spawn() {
        base.Spawn();

        SetModel("models/generator/generator.vmdl");
        SetupPhysicsFromModel(PhysicsMotionType.Static, false);
        Sequence = "DefaultState";

		this.SetBodyGroup("Battery", 0);

		HasBattery = false;
	}

	public bool IsUsable( Entity user ) {
       return true;
    }

    public bool OnUse( Entity user ) 
	{

			if(HasBattery == true)
				this.SetBodyGroup("Battery", 1);
			else
				this.SetBodyGroup("Battery", 0);


		//if (Sequence == "BatteryInsert" || Sequence == "FuelPour" || Tags.Has("being_filled")) {return false;}

		if ( user is Player player) {
            if (player.Tags.Has("is_holding_battery") && !Tags.Has("has_battery") && IsCurrentlyBeingFilledWithFuel == false  && IsCurrentlyHavingTheBatteryInsertedIntoIt == false) {

				player.Tags.Remove("is_holding_battery");
				player.Tags.Remove("has_item");

				player.Tags.Add("active_battery_inserter");

				Sequence = "BatteryInsert";
				Tags.Add("has_battery");

				IsCurrentlyHavingTheBatteryInsertedIntoIt = true;

				batteryTime = 0;
			}

			if (player.Tags.Has("is_holding_fuel") && fuelIn < 4  && IsCurrentlyBeingFilledWithFuel == false  && IsCurrentlyHavingTheBatteryInsertedIntoIt == false)
			{

				player.Tags.Remove("is_holding_fuel");
				player.Tags.Remove("has_item");

				player.Tags.Add("active_fuel_pourer");

				Sequence = "FuelPour";

				IsCurrentlyBeingFilledWithFuel = true;

				fuelTime = 0;
			}

		}

        return false;
    }

	[Event.Tick.Server]
	protected void Tick()
	{
		if(HasBattery == true)
			this.SetBodyGroup("Battery", 1);
		else
			this.SetBodyGroup("Battery", 0);


		if(IsCurrentlyBeingFilledWithFuel == false && IsCurrentlyHavingTheBatteryInsertedIntoIt == false)
			Sequence = "DefaultState";

		if(IsCurrentlyBeingFilledWithFuel == true && IsCurrentlyHavingTheBatteryInsertedIntoIt == false)
		{
			fuelTime++;

			if(fuelTime > 230)
			{
				IsCurrentlyBeingFilledWithFuel = false;
				fuelIn++;
			}
		}

		if(IsCurrentlyBeingFilledWithFuel == false && IsCurrentlyHavingTheBatteryInsertedIntoIt == true)
		{
			batteryTime++;

			if(batteryTime > 180)
			{
				IsCurrentlyHavingTheBatteryInsertedIntoIt = false;
				HasBattery = true;
			}
		}

		if(HasBattery == true & fuelIn>3)
		{
			if(startTime == 0)
				PlaySound("generator_start");

			startTime++;

			if(startTime == 600)
			{
				PlaySound("generator_run");
				Tags.Add("running");
			}
		}
	}

	//what lies below is just fundamentally broken. i had to butcher it to make it work but i DID IT.

/*
	public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
	{

		switch(name)
		{
			case "BatteryBegin":
				//Battering beginning insertion.

				IsCurrentlyHavingTheBatteryInsertedIntoIt = true;

				Sandbox.Log.Info("The following has to read true istg: " + IsCurrentlyHavingTheBatteryInsertedIntoIt);
			break;
			case "BatteryIn":
				//Insert the battery into the generator.		
				Tags.Add("battery_in");

				HasBattery = true;

				Sandbox.Log.Info("battery should be inside for good now");

				IsCurrentlyHavingTheBatteryInsertedIntoIt = false;

				Sandbox.Log.Info("The following should read 'false' if not, there is something wrong with s&box: " + IsCurrentlyHavingTheBatteryInsertedIntoIt);

				if(HasBattery) this.SetBodyGroup("Battery", 1);

				Sequence = "DefaultState";

				Sandbox.Log.Info("the following should read 'DefaultState' . if not, fuck me: " + Sequence);

				if(fuelIn > 3) PlaySound("generator_start");
			break;
			case "FuelBegin":
				//Can of Fuel now being poured.
				
				IsCurrentlyBeingFilledWithFuel = true;

				Sandbox.Log.Info("The following has to read true istg: " + IsCurrentlyBeingFilledWithFuel);
			break;
			case "FuelFilled":
				//Can of Fuel poured.

				Sandbox.Log.Info("Alright here's where the code is supposed to increase the amound of gas cans poured by 1! Is it right? Answer: " + fuelIn);

				Sequence = "DefaultState";

				Sandbox.Log.Info("the following should read 'DefaultState' . if not, fuck me: " + Sequence);

				IsCurrentlyBeingFilledWithFuel = false;

				Sandbox.Log.Info("The following should read 'false' if not, there is something wrong with s&box: " + IsCurrentlyBeingFilledWithFuel);

				if(fuelIn > 3) PlaySound("generator_start");
			break;

		}

		base.OnAnimEventGeneric(name,intData, floatData,vectorData,stringData);
	}
*/
}
