using Sandbox.Rcon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sandbox {
    public class SurvivorPlayerAnimator : PawnAnimator {
        public override void Simulate() {
            DoWalk();
        }

        void DoWalk() {
            //Move Speed
            {
                var dir = Velocity;
                var forward = Rotation.Forward.Dot(dir);
                var sideward = Rotation.Right.Dot(dir);

                var angle = MathF.Atan2(sideward, forward).RadianToDegree().NormalizeDegrees();

                SetParam("move_direction", angle);
                SetParam("move_speed", Velocity.Length);
                SetParam("move_groundspeed", Velocity.WithZ(0).Length);
                SetParam("move_y", sideward);
                SetParam("move_x", forward);
            }
        }
    }
}