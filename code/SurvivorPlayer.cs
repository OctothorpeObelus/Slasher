using Sandbox;

partial class SurvivorPlayer : Player {
    public override void Respawn() {
        //SetModel("models/citizen/citizen.vmdl");
        SetModel("models/survivor/basesurvivor.vmdl");

        Controller = new WalkController();
        Animator = new StandardPlayerAnimator();
		Camera = new ThirdPersonCamera();
		//Camera = new FirstPersonCamera();

		SetBodyGroup("Survivors",1);
		SetBodyGroup("GasCan", 0);
		SetBodyGroup("Flashlight", 0);
		EnableAllCollisions = true;
        EnableDrawing = true;
        EnableHideInFirstPerson = true;
        EnableShadowInFirstPerson = true;
        
        base.Respawn();

	}

}

