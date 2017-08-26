namespace MagicFire.Mmorpg.AvatarInputState
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DeadInputState : PcPlayerInputState
    {
        public DeadInputState(AvatarStateController avatarStateController): base(avatarStateController)
        {

        }

        public override void Run()
        {
            //base.Run();
        }

        public override void FixedRun()
        {
            //base.FixedRun();
        }
    } 
}
