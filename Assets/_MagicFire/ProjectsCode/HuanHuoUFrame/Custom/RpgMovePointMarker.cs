namespace MagicFire.HuanHuoUFrame {
    using UnityEngine;
    using System.Collections;

    public partial class RpgMovePointMarker
    {
        private void OnTriggerEnter(Collider other)
        {
            gameObject.SetActive(false);
        }
    }
}
