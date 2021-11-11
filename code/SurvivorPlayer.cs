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
	private int SelectedSurvivor;

	public override void Respawn()
	{
		SetModel("models/survivor/basesurvivor.vmdl");

		Controller = new SurvivorController();
		Animator = new StandardPlayerAnimator();
		//Camera = new ThirdPersonCamera();
		Camera = new FirstPersonCamera();

		//SetBodyGroup("Survivors", new Random().Next(0, 3));
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

	public void SelectSurvivor(int id)
	{
		SelectedSurvivor = id;
	}

	public override void OnKilled()
	{
		base.OnKilled();
		Controller = null;

		//bool "b_dying" to be set to true when slasher kills the survivor

		//this.SetAnimBool("b_dying",true);

		flashlightOn = false;

		this.flashlight = null;
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

			Controller = new SurvivorController();
		}

		if (name == "PourFinished")
		{
			Tags.Remove("active_fuel_pourer");

			Controller = new SurvivorController();
		}

		if (name == "OnDeath")
		{

		//Camera = new SpectateRagdollCamera();

		var ent = new ModelEntity();
		ent.Position = Position;
		ent.Rotation = Rotation;
		ent.Scale = Scale;
		ent.MoveType = MoveType.Physics;
		ent.UsePhysicsCollision = true;
		ent.EnableAllCollisions = true;
		ent.CollisionGroup = CollisionGroup.Debris;
		ent.SetModel( GetModelName() );
		ent.CopyBonesFrom( this );
		ent.CopyBodyGroups( this );
		ent.CopyMaterialGroup( this );
		ent.TakeDecalsFrom( this );
		ent.EnableHitboxes = true;
		ent.EnableAllCollisions = true;
		ent.SurroundingBoundsMode = SurroundingBoundsType.Physics;
		ent.RenderColor = RenderColor;

		//ent.DeleteAsync( 10.0f );

		EnableAllCollisions = false;
		EnableDrawing = false;

		ConsoleSystem.Run("spawnspectator");

		Delete();
		}
	}

	public override void Simulate(Client cl)
	{
		base.Simulate(cl);

		TickPlayerUse();

		SetBodyGroup("Survivors", SelectedSurvivor);

		if (Tags.Has("active_battery_inserter"))
		{
			this.SetAnimBool("b_batteryinsertion", true);

			this.SetBodyGroup("Battery", 0);

			Controller = null;
		}
		else
		{
			this.SetAnimBool("b_batteryinsertion", false);
		}

		if (Tags.Has("active_fuel_pourer"))
		{
			this.SetAnimBool("b_pouring_fuel", true);

			this.SetBodyGroup("GasCan", 0);

			Controller = null;
		}
		else
		{
			this.SetAnimBool("b_pouring_fuel", false);
		}

		if (Input.Pressed(InputButton.Attack2))
		{

			DropHeldItem();

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


		if (Input.Pressed(InputButton.Flashlight)  && !Tags.Has("has_item"))
		{
			if(Tags.Has("is_storing_fuel")){

				PlaySound("item_equip");

				this.Tags.Add("has_item");
				this.Tags.Add("is_holding_fuel");

				this.Tags.Remove("is_storing_fuel");
			}

			if(Tags.Has("is_storing_milk")){

				PlaySound("item_equip");

				this.Tags.Add("has_item");
				this.Tags.Add("is_holding_milk");

				this.Tags.Remove("is_storing_milk");
			}

			if(Tags.Has("is_storing_mayo")){

				PlaySound("item_equip");

				this.Tags.Add("has_item");
				this.Tags.Add("is_holding_mayo");

				this.Tags.Remove("is_storing_mayo");
			}
		}


		if (Tags.Has("is_holding_battery") || Tags.Has("is_holding_fuel") || Tags.Has("is_holding_milk"))
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
		if (Input.Pressed(InputButton.Slot3))
		{
			DropMayo();
		}
		if (Input.Pressed(InputButton.Slot4))
		{
			DropMilk();
		}


	}

		//dropping an item causes an extra, useless dummy prop to be spawned for no reason
		//needs fixed

		//update: the dummy prop exists only clientside for some reason

		//update: fixed

	void DropHeldItem()
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

			if (Tags.Has("is_holding_mayo"))
			{
				Tags.Remove("is_holding_mayo");
				Tags.Remove("has_item");

				DropMayo();
			}

			if (Tags.Has("is_holding_milk"))
			{
				Tags.Remove("is_holding_milk");
				Tags.Remove("has_item");

				DropMilk();
			}
	}

	void DropFuelCan()
	{
		var droppedfuel = new FuelEntity()
		{
			Position = EyePos + EyeRot.Forward * 15,
		};

		if (droppedfuel != null)
		{
			droppedfuel.PhysicsGroup.ApplyImpulse(Velocity + EyeRot.Forward * 150.0f + Vector3.Up * 30.0f, true);
			droppedfuel.PhysicsGroup.ApplyAngularImpulse(Vector3.Random * 100.0f, true);
		}
		if (droppedfuel.IsServer == false) //the fix, remember for later
		{
			droppedfuel.Delete();
		}
	}
	void DropBattery()
	{
		var droppedbat = new BatteryEntity()
		{
			Position = EyePos + EyeRot.Forward * 15,
		};

		if (droppedbat != null)
		{
			droppedbat.PhysicsGroup.ApplyImpulse(Velocity + EyeRot.Forward * 200.0f + Vector3.Up * 50.0f, true);
			droppedbat.PhysicsGroup.ApplyAngularImpulse(Vector3.Random * 50.0f, true);
		}
		if (droppedbat.IsServer == false) //the fix, remember for later
		{
			droppedbat.Delete();
		}

	}

	void DropMayo()
	{
		var droppedmayo = new MayoEntity()
		{
			Position = EyePos + EyeRot.Forward * 15,
		};

		if (droppedmayo != null)
		{
			droppedmayo.PhysicsGroup.ApplyImpulse(Velocity + EyeRot.Forward * 200.0f + Vector3.Up * 50.0f, true);
			droppedmayo.PhysicsGroup.ApplyAngularImpulse(Vector3.Random * 50.0f, true);
		}
		if (droppedmayo.IsServer == false)
		{
			droppedmayo.Delete();
		}

	}

	void DropMilk()
	{
		var droppedmilk = new MilkEntity()
		{
			Position = EyePos + EyeRot.Forward * 15,
		};

		if (droppedmilk != null)
		{
			droppedmilk.PhysicsGroup.ApplyImpulse(Velocity + EyeRot.Forward * 200.0f + Vector3.Up * 50.0f, true);
			droppedmilk.PhysicsGroup.ApplyAngularImpulse(Vector3.Random * 50.0f, true);
		}
		if (droppedmilk.IsServer == false)
		{
			droppedmilk.Delete();
		}

	}

	public override void FrameSimulate(Client cl)
	{
		base.FrameSimulate(cl);

		if (Tags.Has("is_holding_battery"))
		{
			this.SetBodyGroup("Battery", 1);
		}
		else
		{
			this.SetBodyGroup("Battery", 0);
		}

		if (Tags.Has("is_holding_fuel"))
		{
			this.SetBodyGroup("GasCan", 1);
		}
		else
		{
			this.SetBodyGroup("GasCan", 0);
		}

	}
	//why are bodygroups like this why
	//it's like they are consistently delayed by one action what do i do
	//no matter what i try they ALWAYS need one extra push to realize what tf they are supposed to be doing

	//update: apparently this only applies to the game host (or literally just me)

	protected override void OnTagAdded(string tag)
	{
		if(tag == "has_item")
        {
			PlaySound("pickup");
		}

		if (IsLocalPawn != true)
		{
			if (Tags.Has("is_holding_battery"))
			{
				this.SetBodyGroup("Battery", 1);
			}
			else
			{
				this.SetBodyGroup("Battery", 0);
			}

			if (Tags.Has("is_holding_fuel"))
			{
				this.SetBodyGroup("GasCan", 1);
			}
			else
			{
				this.SetBodyGroup("GasCan", 0);
			}

			if (Tags.Has("is_holding_mayo"))
			{
				this.SetBodyGroup("Mayo", 1);
			}
			else
			{
				this.SetBodyGroup("Mayo", 0);
			}

			if (Tags.Has("is_holding_milk"))
			{
				this.SetBodyGroup("Milk", 1);
			}
			else
			{
				this.SetBodyGroup("Milk", 0);
			}
		}

	}

	protected override void OnTagRemoved(string tag)
	{

		if (IsServer == true)
		{
			if (Tags.Has("is_holding_battery"))
			{
				this.SetBodyGroup("Battery", 1);
			}
			else
			{
				this.SetBodyGroup("Battery", 0);
			}

			if (Tags.Has("is_holding_fuel"))
			{
				this.SetBodyGroup("GasCan", 1);
			}
			else
			{
				this.SetBodyGroup("GasCan", 0);
			}

			if (Tags.Has("is_holding_mayo"))
			{
				this.SetBodyGroup("Mayo", 1);
			}
			else
			{
				this.SetBodyGroup("Mayo", 0);
			}

			if (Tags.Has("is_holding_milk"))
			{
				this.SetBodyGroup("Milk", 1);
			}
			else
			{
				this.SetBodyGroup("Milk", 0);
			}
		}
	}
}

