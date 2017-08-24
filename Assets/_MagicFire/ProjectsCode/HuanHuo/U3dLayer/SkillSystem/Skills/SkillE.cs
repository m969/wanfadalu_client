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

using MagicFire.Mmorpg.AvatarState;

namespace MagicFire.Mmorpg.Skill
{
    using UnityEngine;
    using System.Collections;

    //[RequireComponent(typeof())]
    //[AddComponentMenu("")]
    public class SkillE : Skill
    {
        public SkillE(AvatarView spellcaster) : base(spellcaster) { }

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
            var argsString =
                point.x + " " +
                point.y + " " +
                point.z + " ";
            KBEngine.Avatar.MainAvatar.RequestCastSkillByName("SkillE", argsString);
            _spellcaster.SkillManager.CancelReady();
        }
    }
}//namespace_end