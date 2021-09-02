using Sandbox;

partial class SurvivorPlayer : Player {
    public override void Respawn() {
        SetModel("models/survivor/basesurvivor.vmdl");

        Controller = new WalkController();
        Animator = new StandardPlayerAnimator();
		//Camera = new ThirdPersonCamera();
		Camera = new FirstPersonCamera();

		SetBodyGroup("Survivors",1);
		SetBodyGroup("GasCan", 0);
		SetBodyGroup("Flashlight", 0);
		SetBodyGroup("Battery", 1);
        Tags.Add("is_holding_battery");
        SetAnimBool( "b_item_equipped_generic", true );
        Sandbox.Log.Info("Player spawned!");
		EnableAllCollisions = true;
        EnableDrawing = true;
        EnableHideInFirstPerson = true;
        EnableShadowInFirstPerson = true;
        
        base.Respawn();

	}

    public override void Simulate(Client cl) {
        base.Simulate(cl);

        TickPlayerUse();
    }
}

