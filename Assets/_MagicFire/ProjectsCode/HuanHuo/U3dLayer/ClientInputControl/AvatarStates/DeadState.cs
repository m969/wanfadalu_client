namespace MagicFire.Mmorpg.AvatarState
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DeadState : PcPlayerState
    {
        public DeadState(AvatarStateController avatarStateController): base(avatarStateController)
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
