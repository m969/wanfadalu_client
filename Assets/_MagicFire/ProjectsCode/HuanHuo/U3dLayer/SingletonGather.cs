using MagicFire.Mmorpg;
using MagicFire.Mmorpg.AvatarState;

namespace MagicFire.Common
{
    using UnityEngine;
    using System.Collections;

    public partial class SingletonGather
    {
        public static WorldMediator WorldMediator { get { return MagicFire.Mmorpg.WorldMediator.Instance; } }
        public static UiManager UiManager { get { return Common.UiManager.Instance; } }
        public static GameManager GameManager { get { return Common.GameManager.Instance; } }
        public static AvatarStateController AvatarStateController { get { return AvatarStateController.Instance; } }
        public static PlayerTarget PlayerTarget { get { return PlayerTarget.Instance; } }
    }

}