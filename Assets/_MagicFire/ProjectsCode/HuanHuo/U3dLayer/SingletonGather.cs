using MagicFire.Mmorpg;

namespace MagicFire.Common
{
    using UnityEngine;
    using System.Collections;

    public partial class SingletonGather
    {
        public static WorldMediator WorldMediator { get { return MagicFire.Mmorpg.WorldMediator.Instance; } }
        public static UiManager UiManager { get { return Common.UiManager.Instance; } }
        public static GameManager GameManager { get { return Common.GameManager.Instance; } }
        public static PlayerInputController PlayerInputController { get { return PlayerInputController.Instance; } }
        public static PlayerTarget PlayerTarget { get { return PlayerTarget.Instance; } }
    }

}