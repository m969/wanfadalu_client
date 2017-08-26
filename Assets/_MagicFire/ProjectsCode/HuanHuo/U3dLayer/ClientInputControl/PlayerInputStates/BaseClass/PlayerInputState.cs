using MagicFire.Common;
using MagicFire.Common.Plugin;

namespace MagicFire.Mmorpg.AvatarInputState
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerInputState
    {
        private static Vector3 _moveVector = Vector3.zero;
        private static AvatarView _avatarView;
        private static Object _clickPointAuxiliaryPrefab;
        private static GameObject _clickPointObject;

        protected static Object ClickPointAuxiliaryPrefab
        {
            get
            {
                if (_clickPointAuxiliaryPrefab == null)
                    _clickPointAuxiliaryPrefab = AssetTool.LoadAuxiliaryAssetByName("ClickPointAuxiliary");
                return _clickPointAuxiliaryPrefab;
            }
        }

        protected static GameObject ClickPointObject
        {
            get
            {
                return _clickPointObject;
            }
            set
            {
                _clickPointObject = value;
            }
        }

        protected static AvatarView AvatarView
        {
            get
            {
                if (_avatarView == null)
                    _avatarView = SingletonGather.WorldMediator.MainAvatarView;
                return _avatarView;
            }
        }

        public static Vector3 MoveVector
        {
            get { return _moveVector; }
            set { _moveVector = value; }
        }

        protected PlayerInputState()
        {

        }

        public virtual void Run()
        {

        }

        public virtual void FixedRun()
        {

        }
    }

}