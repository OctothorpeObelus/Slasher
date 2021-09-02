using System.Numerics;
using System;
using Sandbox;

partial class SurvivorPlayer : Player {
    private bool FirstPersonCamera = true;
    private int s_choice = 0;
    private bool flashlightOn = false;
    private ModelEntity ent = null;
    private SpotLightEntity flashlight;
    
    public override void Respawn() {
        SetModel("models/survivor/basesurvivor.vmdl");

        Controller = new WalkController();
        Animator = new StandardPlayerAnimator();
		//Camera = new ThirdPersonCamera();
		Camera = new FirstPersonCamera();

		SetBodyGroup("Survivors",new Random().Next(0,3));
        Tags.Add("is_holding_battery");
        Sandbox.Log.Info("Player spawned!");
		EnableAllCollisions = true;
        EnableDrawing = true;
        EnableHideInFirstPerson = true;
        EnableShadowInFirstPerson = true;
        
        base.Respawn();

        if (ent != null) {
            ent.DeleteAsync(0.0f);
        }

        this.flashlight = CreateLight();
        this.flashlight.SetParent(this, "HandR", new Transform(Vector3.Forward*10));
        this.flashlight.Enabled = false;
	}

    public override void OnKilled() {
        base.OnKilled();
		Camera = new SpectateRagdollCamera();
		Controller = null;

        ent = new ModelEntity();
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
		ent.RenderColorAndAlpha = RenderColorAndAlpha;
		ent.PhysicsGroup.Velocity = Velocity;
        ent.SetInteractsAs( CollisionLayer.Debris );
        ent.SetInteractsExclude( CollisionLayer.Player | CollisionLayer.Debris );
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
			LightCookie = Texture.Load( "materials/effects/lightcookie.vtex" )
		};

		return light;
	}

    public override void Simulate(Client cl) {
        base.Simulate(cl);

        TickPlayerUse();

        if (Input.Pressed(InputButton.Attack2)) {
            this.SetBodyGroup("Survivors",s_choice);
            s_choice = (s_choice < 4) ? s_choice+1 : 0;
        }

        if (Input.Pressed(InputButton.Attack1)) {
            flashlightOn = !flashlightOn;
            this.SetBodyGroup("Flashlight",(flashlightOn) ? 1 : 0);
            this.SetAnimBool("b_flashlight_equipped", flashlightOn);
            if (flashlight.IsValid()) {flashlight.Enabled = flashlightOn;}
        }

        if (Input.Pressed(InputButton.Reload)) {
            FirstPersonCamera = !FirstPersonCamera;
            if (FirstPersonCamera) {
                Camera = new FirstPersonCamera();
            } else {
                Camera = new ThirdPersonCamera();
            }
        }

        if (Tags.Has("is_holding_battery")) {
            this.SetBodyGroup("Battery", 0);
        } else {
            this.SetBodyGroup("Battery", 0);
        }

        if (Tags.Has("is_holding_fuel")) {
            this.SetBodyGroup("GasCan", 1);
        } else {
            this.SetBodyGroup("GasCan", 0);
        }

        if (Tags.Has("is_holding_battery") || Tags.Has("is_holding_fuel")) {
            this.SetAnimBool( "b_item_equipped_generic", true );
        } else {
            this.SetAnimBool( "b_item_equipped_generic", false );
        }
    }
}

