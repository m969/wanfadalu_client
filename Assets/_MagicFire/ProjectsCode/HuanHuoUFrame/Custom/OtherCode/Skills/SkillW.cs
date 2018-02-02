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

    //[RequireComponent(typeof())]
    //[AddComponentMenu("")]
    public class SkillW : Skill
    {
        public SkillW(SkillEntityView spellcaster) : base(spellcaster)
        {
            SkillName = "基础土系法术:碎石阵";
            SkillID = 2;
        }

        public override void Ready(AvatarView spellcaster)
        {
            base.Ready(spellcaster);
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Terrian"));
            SkillTrajectory.transform.eulerAngles = new Vector3(90, 0, 0);
            SkillTrajectory.transform.position = new Vector3(hit.point.x, hit.point.y + 1.0f, hit.point.z);
            if (Input.GetMouseButtonDown(0))
                Conjure();
        }

        public override void Conjure(params object[] args)
        {
            var point = SkillTrajectory.transform.position;
            ArgsString = point.x + ":" + point.y + ":" + point.z + ":";
            base.Conjure();
            SkillController.CancelReady();
        }

        public override void OnCast(string argsString)
        {
            //var args = argsString.Split(":");
            //Spellcaster.transform.LookAt(new Vector3(int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2])));
            ((AvatarView)Spellcaster).Animator.SetTrigger("Attack_2");
        }
    }
}//namespace_end