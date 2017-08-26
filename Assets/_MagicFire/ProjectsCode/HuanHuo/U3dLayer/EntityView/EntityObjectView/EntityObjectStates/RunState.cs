namespace MagicFire.Mmorpg
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RunState : AvatarState
    {
        public RunState(AvatarView avatarView)
            : base(avatarView)
        {

        }

        public override void Run()
        {
            base.Run();
            if (AvatarView.Animator)
            {
                if (AvatarView.Animator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion") ||
                    AvatarView.Animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") ||
                    AvatarView.Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    AvatarView.Animator.SetFloat("Speed", 1.0f);
                }
            }
        }
    }

}