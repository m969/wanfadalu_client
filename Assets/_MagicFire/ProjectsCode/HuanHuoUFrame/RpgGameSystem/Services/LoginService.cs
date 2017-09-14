namespace MagicFire.HuanHuoUFrame{
    using HuanHuoUFrame;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.IOC;
    using uFrame.Kernel;
    using uFrame.MVVM;
    using UniRx;
    using UnityEngine;
    
    
    public class LoginService : LoginServiceBase {
        
        /// <summary>
        /// This method is invoked whenever the kernel is loading
        /// Since the kernel lives throughout the entire lifecycle  of the game, this will only be invoked once.
        /// </summary>
        public override void Setup() {
            base.Setup();
            // Use the line below to subscribe to events
            // this.OnEvent<MyEvent>().Subscribe(myEventInstance => { TODO });

            this.OnEvent<OnLoginSuccessfullyEvent>().ObserveOnMainThread().Subscribe(OnLoginSuccessfully);

        }

        private void OnLoginSuccessfully(OnLoginSuccessfullyEvent evt)
        {
            Debug.Log("OnLoginSuccessfullyEvent");
            this.Publish(new UnloadSceneCommand()
            {
                SceneName = "LoginScene",
                
            });
        }

    }
}
