using Sandbox;
using System;

public partial class FuelEntity : Prop, IUse
{

	public override void Spawn()
	{
		base.Spawn();

		SetModel("models/fuelcan/fuelcan.vmdl");
		SetupPhysicsFromModel(PhysicsMotionType.Dynamic, false);
	}

	public bool IsUsable(Entity user)
	{
		return true;
	}

	public bool OnUse(Entity user)
	{
		if (user is Player player)
		{
			if (!player.Tags.Has("is_holding_fuel") && !player.Tags.Has("has_item"))
			{
				player.Tags.Add("is_holding_fuel");
				player.Tags.Add("has_item");
				player.SetAnimBool("b_item_equipped_generic", true);
				player.SetBodyGroup("GasCan", 1);
				Delete();
			}
		}

		return false;
	}
}
