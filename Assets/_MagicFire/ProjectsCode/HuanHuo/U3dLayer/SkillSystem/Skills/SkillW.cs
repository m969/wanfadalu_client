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
using MagicFire.SceneManagement;

namespace MagicFire.Mmorpg.Skill
{
    using UnityEngine;
    using System.Collections;

    //[RequireComponent(typeof())]
    //[AddComponentMenu("")]
    public class SkillW : Skill
    {
        public Vector2 SkillTrajectoryPosition { get; set; }

        public SkillW(AvatarView spellcaster) : base(spellcaster) { }

        public override void Ready(AvatarView spellcaster)
        {
            base.Ready(spellcaster);

            if (Application.platform == RuntimePlatform.Android)
            {
                var pos = spellcaster.transform.position;
                SkillTrajectory.transform.position = new Vector3(pos.x + SkillTrajectoryPosition.x * 20, pos.y, pos.z + SkillTrajectoryPosition.y * 20);
            }
            else
            {
                if (XmlSceneManager.Instance.ControlMode == XmlSceneManager.ControlModeEnum.PcControl)
                {
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit, AvatarStateController.RayCastHitDist, 1 << LayerMask.NameToLayer("Terrian"));
                    SkillTrajectory.transform.position = hit.point;
                    SkillTrajectory.transform.position = new Vector3(SkillTrajectory.transform.position.x, SkillTrajectory.transform.position.y + 0.5f, SkillTrajectory.transform.position.z);
                    if (Input.GetMouseButtonDown(0))
                    {
                        Conjure();
                    }
                }
                else
                {
                    var pos = spellcaster.transform.position;
                    SkillTrajectory.transform.position = new Vector3(pos.x + SkillTrajectoryPosition.x * 20, pos.y, pos.z + SkillTrajectoryPosition.y * 20);
                }
            }
        }

        public override void Conjure(params object[] args)
        {
            base.Conjure();
            if (_spellcaster.Animation)
            {
                if (!_spellcaster.Animation.IsPlaying("Attack"))
                {
                    _spellcaster.Animation.Play("Attack");
                }
            }
            var point = SkillTrajectory.transform.position;
            var argsString =
                point.x + " " +
                point.y + " " +
                point.z + " ";
            KBEngine.Avatar.MainAvatar.RequestCastSkillByName(this.GetType().Name, argsString);
            _spellcaster.SkillManager.CancelReady();
        }
    }
}//namespace_end