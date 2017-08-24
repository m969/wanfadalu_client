using MagicFire.SceneManagement;

namespace MagicFire.Common
{
    using UnityEngine;
    using System.Collections;
    using MagicFire;

    public partial class SingletonGather
    {
        public static FactorysFactory FactorysFactory { get { return MagicFire.FactorysFactory.Instance; } }
        public static XmlSceneManager XmlSceneManager { get { return XmlSceneManager.Instance; } }
    }

}