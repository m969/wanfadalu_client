using MagicFire.Common;
using MagicFire.Common.Plugin;
using PathologicalGames;

namespace MagicFire.HuanHuoUFrame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerInputState
    {
        private static Vector3 _moveVector = Vector3.zero;
        private static Object _clickPointAuxiliaryPrefab;
        private static GameObject _clickPointObject;

        protected static Object ClickPointAuxiliaryPrefab
        {
            get
            {
                if (_clickPointAuxiliaryPrefab == null)
                {
                    var viewPool = PoolManager.Pools["AuxiliaryPool"];
                    _clickPointAuxiliaryPrefab = viewPool.prefabs["ClickPointAuxiliary"];
                }
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

        public static AvatarView AvatarView
        {
            get;set;
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