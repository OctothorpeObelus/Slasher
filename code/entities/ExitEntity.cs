using Sandbox;
using System;

public partial class ExitEntity : AnimEntity
{

	public override void Spawn()
	{
		base.Spawn();

		SetModel("models/exitdoor/exitdoor.vmdl");

		SetupPhysicsFromModel(PhysicsMotionType.Static, true);
	}

	protected override void OnTagAdded(string tag) {
		
		if(tag == "open")
		{
			//this.SetAnimBool("b_opening", true);

			CurrentSequence.Name = "Opening";

			Sound.FromEntity("exit_unlock", this);
			Sound.FromEntity("exit_open", this);
		}

	}

}
