using Sandbox;

partial class SlasherPlayer : Player {

    private SpotLightEntity worldLight;

	private SpotLightEntity worldLight1;

	private SpotLightEntity worldLight2;

	private SpotLightEntity worldLight3;

	public int SelectedSlasher = 1;

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

		break;
		case 2:

		SetModel("models/slasher/amogus/amogus.vmdl");

		this.Tags.Add("amogus");

		break;
		case 3:

		SetModel("models/slasher/trollge/trolle.vmdl");

		Sound.FromEntity("trolle_breathing", this);

		this.Tags.Add("trollge");

		break;
		}

        Controller = new SlasherController();
        Animator = new StandardPlayerAnimator();
        Camera = new FirstPersonCamera();

		//Controller.OverwriteSprintSpeed(500f);

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
	}

    // Literally too angry to die
    public override void TakeDamage( DamageInfo info )
	{
	}

    [ClientRpc]
	public void TookDamage( DamageFlags damageFlags, Vector3 forcePos, Vector3 force )
	{
	}

}
