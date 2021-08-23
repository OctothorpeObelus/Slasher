namespace Sandbox {
    public class SurvivorController : WalkController {
        public new float SprintSpeed = 300.0f;
        public float WalkSpeed = 100.0f;
        public float DefaultSpeed = 200.0f;
    
        public override float GetWishSpeed()
		{
			var ws = Duck.GetWishSpeed();
			if ( ws >= 0 ) return ws;

			if ( Input.Down( InputButton.Run ) ) return SprintSpeed;
			if ( Input.Down( InputButton.Walk ) ) return WalkSpeed;

			return DefaultSpeed;
		}
    }
}