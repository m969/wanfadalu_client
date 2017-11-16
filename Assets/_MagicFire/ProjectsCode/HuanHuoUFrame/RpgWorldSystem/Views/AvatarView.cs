namespace MagicFire.HuanHuoUFrame {
    using MagicFire.HuanHuoUFrame;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    using uFrame.MVVM.Services;
    using uFrame.MVVM.ViewModels;
    using UniRx;
    using UnityEngine;
    
    
    public class AvatarView : AvatarViewBase {

        [SerializeField]
        [Range(0.1f, 1.0f)]
        private float _speed;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private Animation _animation;

        [SerializeField]
        private CharacterController _characterController;

        public SkillManager SkillManager
        {
            get;
            set;
        }

        public float Speed
        {
            get { return _speed; }
        }

        public Animator Animator
        {
            get
            {
                return _animator;
            }
        }

        public Animation Animation
        {
            get
            {
                return _animation;
            }
        }

        public CharacterController CharacterController
        {
            get
            {
                return _characterController;
            }
        }


        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as AvatarViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.Avatar to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.

            SkillManager = new SkillManager { Owner = this };
            SkillManager.Init();

            this.Bindings.Add(
                Observable.EveryUpdate().Subscribe(evt =>
                {
                    SkillManager.Update();
                    transform.GetChild(0).localPosition = Vector3.zero;
                })
            );
        }

        public override void OnStopMoveExecuted(OnStopMoveCommand command)
        {
            //_avatarState = _standState;
        }

        public override void DoMoveExecuted(DoMoveCommand command)
        {
            //_avatarState = _runState;
            //Avatar.avatarStateProperty.SetState<TheRunState>();
            //Avatar.avatarState.OnNext(Avatar.avatarStateProperty.Run);
            //Avatar.avatarStateProperty.Run.OnNext(true);
        }

        public override void OnSkillStartCastExecuted(OnSkillStartCastCommand command)
        {
            base.OnSkillStartCastExecuted(command);

        }

        public override void OnSkillEndCastExecuted(OnSkillEndCastCommand command)
        {
            base.OnSkillEndCastExecuted(command);
        }

        public override void OnIdleState()
        {
            base.OnIdleState();
            Animator.SetFloat("Speed", 0.0f);
        }

        public override void OnDeadState()
        {
            base.OnDeadState();
        }

        public override void OnWalkState()
        {
            base.OnWalkState();
            Animator.SetFloat("Speed", 1.0f);
        }

        public override void OnRunState()
        {
            base.OnRunState();
            Animator.SetFloat("Speed", 1.0f);
        }

        public override void OnHitState()
        {
            base.OnHitState();
        }

        public override void OnCastSkillState()
        {
            base.OnCastSkillState();
            SkillManager.OnCastSkill(Avatar.CurrentSkillName, Avatar.CurrentSkillArgs);
        }
    }
}
