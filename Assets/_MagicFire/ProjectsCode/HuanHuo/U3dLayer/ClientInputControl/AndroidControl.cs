/* *********************************************************
 * Company   
	: MagicFire Studio
 * Autor         
	: Chengmin
 * Description 
	: 
 * Date          
	: 7/2/2017
 * *********************************************************/

using MagicFire.Mmorpg.AvatarInputState;

namespace MagicFire.Mmorpg.Huanhuo
{
    using UnityEngine;
    using System.Collections;

    //[RequireComponent(typeof())]
    //[AddComponentMenu("")]
    public class AndroidControl : MonoBehaviour
    {
        #region Property and Field
        //[Tooltip("")]
        [SerializeField]
        private ETCJoystick _characterMoveJoystick;
        [SerializeField]
        private ETCJoystick _skillQJoystick;
        [SerializeField]
        private ETCJoystick _skillWJoystick;
        [SerializeField]
        private ETCButton _skillEtcButton;
        #endregion

        #region Private Method
        void Start()
        {
            _characterMoveJoystick.onMoveStart.AddListener(OnMoveStart);
            _characterMoveJoystick.onMove.AddListener(OnMove);
            _characterMoveJoystick.onMoveEnd.AddListener(OnMoveEnd);

            _skillQJoystick.onMoveStart.AddListener(OnSkillQJoystickMoveStart);
            _skillQJoystick.onMove.AddListener(OnSkillQJoystickMove);
            _skillQJoystick.onMoveEnd.AddListener(OnSkillQJoystickMoveEnd);

            _skillWJoystick.onMoveStart.AddListener(OnSkillWJoystickMoveStart);
            _skillWJoystick.onMove.AddListener(OnSkillWJoystickMove);
            _skillWJoystick.onMoveEnd.AddListener(OnSkillWJoystickMoveEnd);

            _skillEtcButton.onDown.AddListener(OnSkillEDown);
        }

        //  void Update()
        //  {
        //
        //  }
        #endregion

        #region Public Method
        //
        public void OnMoveStart()
        {
            if (AvatarStateController.Instance)
                AvatarStateController.Instance.StartMove();
        }

        public void OnMove(Vector2 vec)
        {
            if (AvatarStateController.Instance)
                AvatarStateController.Instance.MoveMainAvatar(vec);
        }

        public void OnMoveEnd()
        {
            if (AvatarStateController.Instance)
                AvatarStateController.Instance.EndMove();
        }

        public void OnSkillQJoystickMoveStart()
        {
            if (AvatarStateController.Instance)
                AvatarStateController.Instance.SkillQReady();
        }

        public void OnSkillQJoystickMove(Vector2 vec)
        {
            if (AvatarStateController.Instance)
                AvatarStateController.Instance.OnSkillQReadying(vec);
        }

        public void OnSkillQJoystickMoveEnd()
        {
            if (AvatarStateController.Instance)
                AvatarStateController.Instance.DoSkillQ();
        }

        public void OnSkillWJoystickMoveStart()
        {
            if (AvatarStateController.Instance)
                AvatarStateController.Instance.SkillWReady();
        }

        public void OnSkillWJoystickMove(Vector2 vec)
        {
            if (AvatarStateController.Instance)
                AvatarStateController.Instance.OnSkillWReadying(vec);
        }

        public void OnSkillWJoystickMoveEnd()
        {
            if (AvatarStateController.Instance)
                AvatarStateController.Instance.DoSkillW();
        }

        public void OnSkillEDown()
        {
            if (AvatarStateController.Instance)
                AvatarStateController.Instance.DoSkillE();
        }
        //
        #endregion
    }//class_end
}//namespace_end