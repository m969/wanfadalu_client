using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickEngine.Extensions;

namespace MagicFire.HuanHuoUFrame
{
    public class GongKan : Skill
    {
        public GongKan(AvatarView spellcaster):base(spellcaster)
        {
            SkillName = "PrimaryArrowGongFa:GongKan";
        }

        public override void Ready(AvatarView spellcaster)
        {
            base.Ready(spellcaster);
        }

        public override void Conjure(params object[] args)
        {
            var point = Spellcaster.transform.position + Spellcaster.transform.forward * 4;
            ArgsString = point.x + ":" + point.y + ":" + point.z + ":";
            base.Conjure(args);
        }

        public override void OnCast(string argsString)
        {
            //var args = argsString.Split(":");
            //Spellcaster.transform.LookAt(new Vector3(int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2])));
            Spellcaster.Animator.SetTrigger("Attack_2");
        }
    }
}