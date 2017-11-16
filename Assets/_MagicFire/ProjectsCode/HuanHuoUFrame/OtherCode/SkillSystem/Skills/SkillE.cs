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

using MagicFire.Mmorpg.AvatarInputState;
using QuickEngine.Extensions;

namespace MagicFire.HuanHuoUFrame
{
    using UnityEngine;
    using System.Collections;

    //[RequireComponent(typeof())]
    //[AddComponentMenu("")]
    public class SkillE : Skill
    {
        public SkillE(AvatarView spellcaster) : base(spellcaster)
        {
            SkillName = "基础冰系法术:寒冰刺";
        }

        public override void Ready(AvatarView spellcaster)
        {
            base.Ready(spellcaster);
            if (SkillTrajectory)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit, AvatarStateController.RayCastHitDist, 1 << LayerMask.NameToLayer("Terrian"));
                SkillTrajectory.transform.position = new Vector3(spellcaster.transform.position.x, spellcaster.transform.position.y + 0.5f, spellcaster.transform.position.z);
                SkillTrajectory.transform.LookAt(new Vector3(hit.point.x, SkillTrajectory.transform.position.y, hit.point.z));
                if (Input.GetMouseButtonDown(0))
                {
                    var pos = SkillTrajectory.transform.Find("Point").position;
                    Conjure(pos);
                }
            }
        }

        public override void Conjure(params object[] args)
        {
            var point = (Vector3) args[0];
            ArgsString = point.x + ":" + point.y + ":" + point.z + ":";
            base.Conjure();
            Spellcaster.SkillManager.CancelReady();
        }

        public override void OnCast(string argsString)
        {
            //var args = argsString.Split(":");
            //Spellcaster.transform.LookAt(new Vector3(int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2])));
            Spellcaster.Animator.SetTrigger("Attack_2");
        }
    }
}//namespace_end