using System.Numerics;
using System;
using Sandbox;

partial class SurvivorPlayer : Player
{
	private bool FirstPersonCamera = true;
	private int s_choice = 0;
	private bool flashlightOn = false;
	private ModelEntity ent = null;
	private SpotLightEntity flashlight;

	public override void Respawn()
	{
		SetModel("models/survivor/basesurvivor.vmdl");

		Controller = new WalkController();
		Animator = new StandardPlayerAnimator();
		//Camera = new ThirdPersonCamera();
		Camera = new FirstPersonCamera();

		SetBodyGroup("Survivors", new Random().Next(0, 3));
		//Tags.Add("is_holding_battery");
		Sandbox.Log.Info("Player spawned!");
		EnableAllCollisions = true;
		EnableDrawing = true;
		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;

		base.Respawn();

		if (ent != null)
		{
			ent.DeleteAsync(0.0f);
		}

		this.flashlight = CreateLight();
		this.flashlight.SetParent(this, "HandR", new Transform((Vector3.Forward * 0) + (Vector3.Left * 10) + (Vector3.Up * 0)));
		this.flashlight.LocalRotation = new Angles(0, 135, 0).ToRotation();
		this.flashlight.Enabled = false;
	}

	public override void OnKilled()
	{
		base.OnKilled();
		Controller = null;

		//bool "b_dying" to be set to true when slasher kills the survivor

		//this.SetAnimBool("b_dying",true);

		flashlightOn = false;

		Camera = new SpectateRagdollCamera();

		ent = new ModelEntity();
		ent.Position = Position;
		ent.Rotation = Rotation;
		ent.Scale = Scale;
		ent.MoveType = MoveType.Physics;
		ent.UsePhysicsCollision = true;
		ent.EnableAllCollisions = true;
		ent.CollisionGroup = CollisionGroup.Debris;
		ent.SetModel(GetModelName());
		ent.CopyBonesFrom(this);
		ent.CopyBodyGroups(this);
		ent.CopyMaterialGroup(this);
		ent.TakeDecalsFrom(this);
		ent.EnableHitboxes = true;
		ent.EnableAllCollisions = true;
		ent.SurroundingBoundsMode = SurroundingBoundsType.Physics;
		ent.RenderColorAndAlpha = RenderColorAndAlpha;
		ent.PhysicsGroup.Velocity = Velocity;
		ent.SetInteractsAs(CollisionLayer.Debris);
		ent.SetInteractsExclude(CollisionLayer.Player | CollisionLayer.Debris);
		//ent.DeleteAsync( 10.0f );

		EnableAllCollisions = false;
		EnableDrawing = false;
	}
	private SpotLightEntity CreateLight()
	{
		var light = new SpotLightEntity
		{
			Enabled = true,
			DynamicShadows = true,
			Range = 512,
			Falloff = 1.0f,
			LinearAttenuation = 0.0f,
			QuadraticAttenuation = 1.0f,
			Brightness = 2,
			Color = Color.White,
			InnerConeAngle = 20,
			OuterConeAngle = 40,
			FogStength = 1.0f,
			Owner = Owner,
			LightCookie = Texture.Load("materials/effects/lightcookie.vtex")
		};

		return light;
	}

	public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
	{
		if (name == "BatteryInsertionComplete")
		{
			Tags.Remove("active_battery_inserter");

			Controller = new WalkController();
		}

		if (name == "OnDeath")
		{
			Camera = new SpectateRagdollCamera();

			ent = new ModelEntity();
			ent.Position = Position;
			ent.Rotation = Rotation;
			ent.Scale = Scale;
			ent.MoveType = MoveType.Physics;
			ent.UsePhysicsCollision = true;
			ent.EnableAllCollisions = true;
			ent.CollisionGroup = CollisionGroup.Debris;
			ent.SetModel(GetModelName());
			ent.CopyBonesFrom(this);
			ent.CopyBodyGroups(this);
			ent.CopyMaterialGroup(this);
			ent.TakeDecalsFrom(this);
			ent.EnableHitboxes = true;
			ent.EnableAllCollisions = true;
			ent.SurroundingBoundsMode = SurroundingBoundsType.Physics;
			ent.RenderColorAndAlpha = RenderColorAndAlpha;
			ent.PhysicsGroup.Velocity = Velocity;
			ent.SetInteractsAs(CollisionLayer.Debris);
			ent.SetInteractsExclude(CollisionLayer.Player | CollisionLayer.Debris);
			//ent.DeleteAsync( 10.0f );

			EnableAllCollisions = false;
			EnableDrawing = false;

			//kill survivor also
		}
	}

	public override void Simulate(Client cl)
	{
		base.Simulate(cl);

		TickPlayerUse();

		if (Tags.Has("active_battery_inserter"))
		{
			this.SetAnimBool("b_batteryinsertion", true);

			this.SetBodyGroup("Battery", 0);

			Controller = null;

			//Position = GeneratorEntity().Position;

			//to do: force position to player to line up with the generator used
		}
		else
		{
			this.SetAnimBool("b_batteryinsertion", false);
		}

		if (Input.Pressed(InputButton.Attack2))
		{

			if (Tags.Has("is_holding_fuel"))
			{
				Tags.Remove("is_holding_fuel");
				Tags.Remove("has_item");

				DropFuelCan();
			}

			if (Tags.Has("is_holding_battery"))
			{
				Tags.Remove("is_holding_battery");
				Tags.Remove("has_item");

				DropBattery();
			}

		}

		if (Input.Pressed(InputButton.Flashlight)) 
		{
			this.SetBodyGroup("Survivors", s_choice);
			s_choice = (s_choice < 5) ? s_choice + 1 : 0;
		}

		if (Input.Pressed(InputButton.Attack1))
		{
			flashlightOn = !flashlightOn;
			this.SetBodyGroup("Flashlight", (flashlightOn) ? 1 : 0);
			this.SetAnimBool("b_flashlight_equipped", flashlightOn);
			if (flashlight.IsValid()) { flashlight.Enabled = flashlightOn; }

			PlaySound(flashlightOn ? "flashlight-on" : "flashlight-off");
		}

		if (Input.Pressed(InputButton.Reload))
		{
			FirstPersonCamera = !FirstPersonCamera;
			if (FirstPersonCamera)
			{
				Camera = new FirstPersonCamera();
			}
			else
			{
				Camera = new ThirdPersonCamera();
			}
		}

		//if (Tags.Has("is_holding_battery"))
		//{
		//	this.SetBodyGroup("Battery", 1);
		//}
		//else
		//{
		//	this.SetBodyGroup("Battery", 0);
		//}

		if (Tags.Has("is_holding_fuel"))
		{
			this.SetBodyGroup("GasCan", 1);
		}
		else
		{
			this.SetBodyGroup("GasCan", 0);
		}

		if (Tags.Has("is_holding_battery") || Tags.Has("is_holding_fuel"))
		{
			this.SetAnimBool("b_item_equipped_generic", true);
		}
		else
		{
			this.SetAnimBool("b_item_equipped_generic", false);
		}

		//debug item spawns

		if (Input.Pressed(InputButton.Slot1)) 
		{
			DropFuelCan();
		}
		if (Input.Pressed(InputButton.Slot2))
		{
			DropBattery();
		}

	}

	//dropping an item causes an extra, useless dummy prop to be spawned for no reason
	//needs fixed

	//update: the dummy prop exists only clientside for some reason

	void DropFuelCan()
	{
		var droppedfuel = new FuelEntity()
		{
			Position = Owner.EyePos + Owner.EyeRot.Forward * 50,
		};

		if (droppedfuel != null)
		{
			droppedfuel.PhysicsGroup.ApplyImpulse(Velocity + EyeRot.Forward * 300.0f + Vector3.Up * 100.0f, true);
			droppedfuel.PhysicsGroup.ApplyAngularImpulse(Vector3.Random * 100.0f, true);
		}
		else
		{ 
			
		}
	}
	void DropBattery()
	{
		var droppedbat = new BatteryEntity()
		{
			Position = Owner.EyePos + Owner.EyeRot.Forward * 50,
		};

		if (droppedbat != null)
		{
			droppedbat.PhysicsGroup.ApplyImpulse(Velocity + EyeRot.Forward * 400.0f + Vector3.Up * 100.0f, true);
			droppedbat.PhysicsGroup.ApplyAngularImpulse(Vector3.Random * 100.0f, true);
		}
	}

	//why are bodygroups like this why
	//it's like they are consistently delayed by one action what do i do
	//no matter what i try they ALWAYS need one extra push to realize what tf they are supposed to be doing

	//update: apparently this only applies to the game host (or literally just me)

	protected override void OnTagAdded(string tag)
	{
		if(tag == "is_holding_battery")
        {
			this.SetBodyGroup("Battery", 1);
		}
	}

	protected override void OnTagRemoved(string tag)
	{
		if (tag == "is_holding_battery")
		{
			this.SetBodyGroup("Battery", 0);
		}
	}
}

