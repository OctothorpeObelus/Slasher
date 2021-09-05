using Sandbox;

partial class SlasherPlayer : Player {

    private SpotLightEntity worldLight;

    private SpotLightEntity CreateLight()
	{
		var light = new SpotLightEntity
		{
			Enabled = true,
			DynamicShadows = false,
			Range = 256,
			Falloff = 1.0f,
			LinearAttenuation = 0.0f,
			QuadraticAttenuation = 1.0f,
			Brightness = 20,
			Color = Color.Red,
			InnerConeAngle = 360,
			OuterConeAngle = 360,
			FogStength = 1.0f,
			Owner = Owner,
			LightCookie = Texture.Load( "materials/effects/lightcookie.vtex" )
		};

		return light;
	}


    public override void Respawn() {
        SetModel("models/slasher/baba/bababooey.vmdl");

        Controller = new WalkController();
        Animator = new StandardPlayerAnimator();
        Camera = new ThirdPersonCamera();

        EnableAllCollisions = true;
        EnableDrawing = true;
        EnableHideInFirstPerson = true;
        EnableShadowInFirstPerson = true;

        base.Respawn();

        worldLight = CreateLight();
        worldLight.SetParent(this, false);
        worldLight.Enabled = true;
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
