using Sandbox;

partial class SlasherPlayer : Player {

    private SpotLightEntity worldLight;

	private SpotLightEntity worldLight1;

	private SpotLightEntity worldLight2;

	private SpotLightEntity worldLight3;

	public int SelectedSlasher = 1;

	private string JumpscareSound;

	private bool GoingInvisible = false;

	private bool Invisibility = false;

	private int TimeSinceVisible;

    private SpotLightEntity CreateLight()
	{
		var light = new SpotLightEntity
		{
			Enabled = true,
			DynamicShadows = true,
			Range = 800,
			Falloff = 1.0f,
			LinearAttenuation = 0.0f,
			QuadraticAttenuation = 1.0f,
			Brightness = 3,
			Color = Color.Red,
			InnerConeAngle = 0,
			OuterConeAngle = 90,
			FogStength = 1.0f,
			Owner = Owner,
			LightCookie = Texture.Load("materials/effects/lightcookie.vtex")
		};

		return light;
	}

	public void SelectSlasher(int selection)
	{
		SelectedSlasher = selection;
	}

    public override void Respawn() {

		switch(SelectedSlasher)
		{
		case 1:

		SetModel("models/slasher/baba/bababooey.vmdl");

		Sound.FromEntity("bababooey_breathing", this);

		this.Tags.Add("bababooey");

		JumpscareSound = "babadeath";

		Controller = new SlasherController();

		break;
		case 2:

		SetModel("models/slasher/amogus/amogus.vmdl");

		this.Tags.Add("amogus");

		JumpscareSound = "amogusdeath";

		Controller = new SlasherController();

		break;
		case 3:

		SetModel("models/slasher/trollge/trolle.vmdl");

		Sound.FromEntity("trolle_breathing", this);

		this.Tags.Add("trollge");

		JumpscareSound = "";

		Controller = new TrollgeController();

		//Controller.DefaultSpeed = 150f;

		break;
		}

        //Controller = new SlasherController();
        Animator = new StandardPlayerAnimator();
        //Camera = new FirstPersonCamera();
		Camera = new ThirdPersonCamera();

        EnableAllCollisions = true;
        EnableDrawing = true;
        EnableHideInFirstPerson = true;
        EnableShadowInFirstPerson = true;

        base.Respawn();

		if(IsServer){

        worldLight = CreateLight();
        worldLight.SetParent(this, "", new Transform((Vector3.Forward * 0) + (Vector3.Left * 0) + (Vector3.Up * 64)));
        worldLight.Enabled = true;
		worldLight.LocalRotation = new Angles(0, 0, 0).ToRotation();

		worldLight1 = CreateLight();
        worldLight1.SetParent(this, "", new Transform((Vector3.Forward * 0) + (Vector3.Left * 0) + (Vector3.Up * 64)));
        worldLight1.Enabled = true;
		worldLight1.LocalRotation = new Angles(0, -180, 0).ToRotation();

		worldLight2 = CreateLight();
        worldLight2.SetParent(this, "", new Transform((Vector3.Forward * 0) + (Vector3.Left * 0) + (Vector3.Up * 64)));
        worldLight2.Enabled = true;
		worldLight2.LocalRotation = new Angles(90, 0, 0).ToRotation();

		worldLight3 = CreateLight();
        worldLight3.SetParent(this, "", new Transform((Vector3.Forward * 0) + (Vector3.Left * 0) + (Vector3.Up * 64)));
        worldLight3.Enabled = true;
		worldLight3.LocalRotation = new Angles(-90, 0, 0).ToRotation();

		}
    }

	public override void Simulate(Client cl)
	{
		base.Simulate(cl);	

		//tworldLight.Rotation = EyeRot;

		if(Tags.Has("bababooey")){

		if(Input.Pressed(InputButton.Attack2) && Invisibility == true){
			this.SetBodyGroup("BababooeyBody",0);
			Invisibility = false;
			Sound.FromEntity("bababooey_reveal", this);
			TimeSinceVisible = 1;
			Event.Run("baba_visible");
		}

		if(Input.Pressed(InputButton.Attack2) && Invisibility == false && (TimeSinceVisible > 50 || TimeSinceVisible == 0) && GoingInvisible == false){
			this.SetAnimBool("b_hiding",true);
			Sound.FromEntity("bababooey_hide", this);
			GoingInvisible = true;
		}

		if(TimeSinceVisible > 0 && TimeSinceVisible <500)
			TimeSinceVisible++;

		}

		var tr = Trace.Ray( EyePos, EyePos + EyeRot.Forward *  50)
			.UseHitboxes()
			.Ignore( Owner )
			.Run();

		if(tr.Entity is Entity hitentity)
		{
			if(hitentity is SurvivorPlayer player)
			{
				ConsoleSystem.Run( "slasher_detected_survivor");

				if(Input.Pressed(InputButton.Attack1))
				{

					//var damageInfo = DamageInfo.FromBullet( tr.EndPos, EyeRot.Forward * 100, 9999 )
					//.UsingTraceResult( tr )
					//.WithAttacker( Owner );

					//player.TakeDamage( damageInfo );

					Sound.FromEntity(JumpscareSound, this);

					this.SetAnimBool("b_killing",true);

					player.SetAnimBool("b_dying",true);

					Controller = null;
				}

			}
		}

		if(Tags.Has("trollge")){

			if(Input.Pressed(InputButton.Attack1))
				this.SetAnimBool("b_slashing",true);

		}
	}

    // Literally too angry to die
    public override void TakeDamage( DamageInfo info )
	{
	}
	public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
	{
		if(name == "JumpscareFinished"){
			this.SetAnimBool("b_killing",false);
			Controller = new SlasherController();
		}
		if(name == "Slashed"){
			this.SetAnimBool("b_slashing",false);
		}
		if(name == "Invisible"){
			this.SetBodyGroup("BababooeyBody",1);
			this.SetAnimBool("b_hiding",false);
			Invisibility = true;
			Sound.FromEntity("bababooey_loud", this);
			GoingInvisible = false;
			Event.Run("baba_invisible");
		}
	}

    [ClientRpc]
	public void TookDamage( DamageFlags damageFlags, Vector3 forcePos, Vector3 force )
	{
	}

}
