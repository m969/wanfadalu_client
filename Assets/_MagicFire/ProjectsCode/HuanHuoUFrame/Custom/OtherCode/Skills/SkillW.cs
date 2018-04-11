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

    public class SkillW : Skill
    {
        public SkillW(SkillEntityView spellcaster) : base(spellcaster)
        {
        }

        public override void Ready(AvatarView spellcaster)
        {
            base.Ready(spellcaster);
            SkillTrajectory.transform.eulerAngles = new Vector3(90, 0, 0);
            SkillTrajectory.transform.position = new Vector3(RaycastHit.point.x, RaycastHit.point.y + 1.0f, RaycastHit.point.z);
            if (Input.GetMouseButtonDown(0))
                Conjure();
        }

        public override void Conjure(params object[] args)
        {
            var point = SkillTrajectory.transform.position;
            ArgsString = point.x + ":" + point.y + ":" + point.z + ":";
            base.Conjure(args);
            SkillController.CancelReady();
        }

        public override void OnCast(string argsString)
        {
            ((AvatarView)Spellcaster).Animator.SetTrigger("Attack_2");
        }
    }
}