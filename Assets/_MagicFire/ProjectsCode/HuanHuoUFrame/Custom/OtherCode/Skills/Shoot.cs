/* *********************************************************
 * Company   
	: MagicFire Studio
 * Autor         
	: Changmin
 * Description 
	: 
 * Date          
	: 5/28/2017
 * *********************************************************/

using QuickEngine.Extensions;

namespace MagicFire.HuanHuoUFrame
{
    using UnityEngine;
    using System.Collections;

    public class Shoot : Skill
    {
        public Shoot(SkillEntityView spellcaster) : base(spellcaster)
        {
        }

        public override void Ready(AvatarView spellcaster)
        {
            base.Ready(spellcaster);
            SkillTrajectory.transform.position = spellcaster.transform.position + new Vector3(0, 0.1f, 0);
            SkillTrajectory.transform.LookAt(new Vector3(RaycastHit.point.x, SkillTrajectory.transform.position.y, RaycastHit.point.z));
            if (Input.GetMouseButtonDown(0))
                Conjure();
        }

        public override void Conjure(params object[] args)
        {
            var point = SkillTrajectory.transform.Find("SkillPoint").position;
            ArgsString = point.x + ":" + point.y + ":" + point.z + ":";
            base.Conjure(args);
            SkillController.CancelReady();
        }

        public override void OnCast(string argsString)
        {
            ((AvatarView)Spellcaster).Animator.SetTrigger("Attack_1");
        }
    }
}