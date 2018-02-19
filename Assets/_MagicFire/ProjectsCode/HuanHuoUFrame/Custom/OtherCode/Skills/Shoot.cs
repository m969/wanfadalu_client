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
    public class Shoot : Skill
    {
        public Shoot(SkillEntityView spellcaster) : base(spellcaster)
        {
            GongFaID = 1001;
            SkillIndex = 0;
        }

        public override void Ready(AvatarView spellcaster)
        {
            base.Ready(spellcaster);

            SkillTrajectory.transform.position = spellcaster.transform.position + Vector3.up;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Terrian"));
            SkillTrajectory.transform.LookAt(new Vector3(hit.point.x, spellcaster.transform.position.y,hit.point.z) + Vector3.up);
            if (Input.GetMouseButtonDown(0))
                Conjure();
        }

        public override void Conjure(params object[] args)
        {
            var point = SkillTrajectory.transform.Find("SkillPoint").position;
            ArgsString = point.x + ":" + point.y + ":" + point.z + ":";
            base.Conjure();
            SkillController.CancelReady();
        }

        public override void OnCast(string argsString)
        {
            //Debug.Log("argsString = " + argsString);
            //var args = argsString.Split(":");
            //Debug.Log("argsString = " + args[0] + args[1] + args[2]);
            //Spellcaster.transform.LookAt(new Vector3(float.Parse(args[0]), float.Parse(args[1]), float.Parse(args[2])));
            ((AvatarView)Spellcaster).Animator.SetTrigger("Attack_1");
        }
    }//class_end
}//namespace_end