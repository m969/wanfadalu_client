namespace MagicFire.HuanHuoUFrame
{
    using System.Collections;
    using System.Collections.Generic;
    using MagicFire.Mmorpg;
    using UnityEngine;

    public class AvatarState
    {
        protected readonly AvatarView AvatarView;

        protected AvatarState(AvatarView avatarView)
        {
            AvatarView = avatarView;
        }

        public virtual void Run()
        {

        }

        public virtual void FixedRun()
        {

        }
    } 
}