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
using MagicFire.SceneManagement;

namespace MagicFire.HuanHuoUFrame
{
    using UnityEngine;
    using System.Collections;

    //[RequireComponent(typeof())]
    //[AddComponentMenu("")]
    public class SkillQ : Skill
    {
        public SkillQ(AvatarView spellcaster) : base(spellcaster)
        {

        }

        public override void Ready(AvatarView spellcaster)
        {
            base.Ready(spellcaster);

            if (SkillTrajectory == null)
                Debug.LogError("SkillTrajectory == null");
            if (spellcaster == null)
                Debug.LogError("spellcaster == null");
            if (SkillTrajectory == null || spellcaster == null)
                return;

            SkillTrajectory.transform.position = spellcaster.transform.position + Vector3.up;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, AvatarStateController.RayCastHitDist, 1 << LayerMask.NameToLayer("Terrian"));
            SkillTrajectory.transform.LookAt(new Vector3(hit.point.x, spellcaster.transform.position.y,hit.point.z) + Vector3.up);
            if (Input.GetMouseButtonDown(0))
                Conjure();
        }

        public override void Conjure(params object[] args)
        {
            base.Conjure();
            //if (_spellcaster.Animation)
            //{
            //    if (!_spellcaster.Animation.IsPlaying("Attack"))
            //    {
            //        _spellcaster.Animation.Play("Attack");
            //    }
            //}
            var point = SkillTrajectory.transform.Find("SkillPoint").position;
            var argsString =
                point.x + " " +
                point.y + " " +
                point.z + " " ;
            KBEngine.Event.fireIn("RequestCastSkillByName", new object[] { this.GetType().Name, argsString });
            _spellcaster.SkillManager.CancelReady();
        }
    }//class_end
}//namespace_end