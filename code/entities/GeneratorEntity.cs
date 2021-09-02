using Sandbox;

public partial class GeneratorEntity : AnimEntity, IUse {
    private SurvivorPlayer user;
    private bool HasBattery;

    public override void Spawn() {
        base.Spawn();

        SetModel("models/generator/generator.vmdl");
        SetupPhysicsFromModel(PhysicsMotionType.Static, false);
        this.user = null;
        this.HasBattery = false;
        Sequence = "DefaultState";
    }

    public bool IsUsable( Entity user ) {
        return true;
    }

    public bool OnUse( Entity user ) {
        Sandbox.Log.Info(user);
        if ( user is SurvivorPlayer player) {
            //TODO: Check for tags of items and handle appropriately.
            if (player.Tags.Has("is_holding_battery") && !Tags.Has("has_battery")) {
                this.user = player;
                Sandbox.Log.Info("User has battery and generator does not.");
                Sequence = "BatteryInsert";
            }
        }

        return false;
    }

    public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData) {
        Sandbox.Log.Info(name+" "+Tags.Has("has_battery"));
        if (name == "BatteryIn" && this.user != null) {
            //Insert the battery.
            SetBodyGroup("Battery",1);
            Sequence = "DefaultState";
            Tags.Add("has_battery");
            this.user.SetBodyGroup("Battery", 0);
            this.user.Tags.Remove("is_holding_battery");
            this.user = null;
        }
    }
}