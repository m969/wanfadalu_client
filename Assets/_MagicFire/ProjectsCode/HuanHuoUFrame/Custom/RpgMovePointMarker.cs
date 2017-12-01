namespace MagicFire.HuanHuoUFrame {
    using UnityEngine;
    using System.Collections;

    public partial class RpgMovePointMarker
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Main Avatar")
            {
                this.Publish<StopMoveEvent>(new StopMoveEvent());
                gameObject.SetActive(false);
            }
        }
    }
}
