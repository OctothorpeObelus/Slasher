using Sandbox;

partial class SurvivorPlayer : Player {
    public override void Respawn() {
        //SetModel("models/citizen/citizen.vmdl");
        SetModel("models/survivor/basesurvivor.vmdl");

        Controller = new WalkController();
        Animator = new StandardPlayerAnimator();
        Camera = new ThirdPersonCamera();

        SetBodyGroup("Survivors",2);
        EnableAllCollisions = true;
        EnableDrawing = true;
        EnableHideInFirstPerson = true;
        EnableShadowInFirstPerson = true;
        
        base.Respawn();
    }
}
