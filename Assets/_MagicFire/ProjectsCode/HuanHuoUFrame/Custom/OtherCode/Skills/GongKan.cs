using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickEngine.Extensions;

namespace MagicFire.HuanHuoUFrame
{
    public class GongKan : Skill
    {
        public GongKan(SkillEntityView spellcaster):base(spellcaster)
        {
        }

        public override void Ready(AvatarView spellcaster)
        {
            this.Conjure();
            SkillController.CancelReady();
        }

        public override void Conjure(params object[] args)
        {
            var point = Spellcaster.transform.position + Spellcaster.transform.forward * 4;
            ArgsString = point.x + ":" + point.y + ":" + point.z + ":";
            base.Conjure(args);
        }

        public override void OnCast(string argsString)
        {
            ((AvatarView)Spellcaster).Animator.SetTrigger("Attack_2");
        }
    }
}