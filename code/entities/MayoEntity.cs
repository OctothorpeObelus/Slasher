using Sandbox;
using System;

public partial class MayoEntity : Prop, IUse
{

	public override void Spawn()
	{
		base.Spawn();

		SetModel("models/mayo/mayo.vmdl");
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
			if (!player.Tags.Has("is_holding_mayo") && !player.Tags.Has("has_item") && player.Tags.Has("survivor"))
			{
				player.Tags.Add("is_holding_mayo");
				player.Tags.Add("has_item");

				Delete();
			}
		}

		return false;
	}
}
