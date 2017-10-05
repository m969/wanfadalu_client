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
    public class SkillW : Skill
    {
        public SkillW(AvatarView spellcaster) : base(spellcaster)
        {

        }

        public override void Ready(AvatarView spellcaster)
        {
            base.Ready(spellcaster);
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, AvatarStateController.RayCastHitDist, 1 << LayerMask.NameToLayer("Terrian"));
            SkillTrajectory.transform.eulerAngles = new Vector3(90, 0, 0);
            SkillTrajectory.transform.position = new Vector3(hit.point.x, hit.point.y + 1.0f, hit.point.z);
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
            var point = SkillTrajectory.transform.position;
            var argsString =
                point.x + " " +
                point.y + " " +
                point.z + " ";
            KBEngine.Event.fireIn("RequestCastSkillByName", new object[] { this.GetType().Name, argsString });
            _spellcaster.SkillManager.CancelReady();
        }
    }
}//namespace_end