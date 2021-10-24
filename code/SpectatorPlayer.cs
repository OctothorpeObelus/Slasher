using Sandbox;

partial class SpectatorPlayer : Player {
    public override void Respawn() {
        //SetModel("models/citizen/citizen.vmdl");
        //SetModel("models/survivor/basesurvivor.vmdl");

        Controller = new NoclipController();
       // Animator = new StandardPlayerAnimator();
        Camera = new FirstPersonCamera();

        EnableAllCollisions = true;
        EnableDrawing = true;
        EnableHideInFirstPerson = true;
        EnableShadowInFirstPerson = true;
        
        base.Respawn();
    }
}