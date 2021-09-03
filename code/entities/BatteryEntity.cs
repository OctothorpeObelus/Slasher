using Sandbox;
using System;

public partial class BatteryEntity : Prop, IUse
{

	public override void Spawn()
	{
		base.Spawn();

		SetModel("models/battery/battery.vmdl");
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
			if (!player.Tags.Has("is_holding_battery") && !player.Tags.Has("has_item"))
			{
				player.Tags.Add("is_holding_battery");
				player.Tags.Add("has_item");

				Delete();
			}
		}

		return false;
	}
}
