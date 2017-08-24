namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using System.Collections;
    using UnityEngine.UI;
    using MagicFire.Common;

    public class MessageBox : Panel
    {
        private Text _messageText;

        protected override void Start()
        {
            base.Start();
            //transform.SetParent(UIManager.instance.canvas.transform);
            //transform.SetParent(SingletonGather.UiManager.Canvas.transform);
            //transform.localPosition = new Vector3(0, 0, 0);
        }

        public void Show(object message)
        {
            if (_messageText)
            {
                _messageText.text = message.ToString();
            }
            else
            {
                _messageText = transform.Find("Image/MessageText").GetComponent<Text>();
                _messageText.text = message.ToString();
            }
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }

}