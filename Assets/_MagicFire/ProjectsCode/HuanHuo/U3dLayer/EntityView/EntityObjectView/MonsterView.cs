using MagicFire.Common.Plugin;

namespace MagicFire.Mmorpg
{
    using UnityEngine;
    using System.Collections;
    using System;
    using System.Linq;
    using Object = UnityEngine.Object;

    public class MonsterView : SkillEntityObjectView
    {
        private Animator _animator;
        private Animation _animation;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animation = GetComponent<Animation>();
        }

        public override void InitializeView(Common.IModel model)
        {
            base.InitializeView(model);
            Model.SubscribeMethodCall(KBEngine.Monster.AI.StartMove, StartMove);
            Model.SubscribeMethodCall(KBEngine.Monster.AI.StopMove,StopMove);
            Model.SubscribeMethodCall(KBEngine.Monster.AI.Attack_01, Attack_01);
            Model.SubscribeMethodCall(KBEngine.Monster.AI.Attack_02, Attack_02);
        }

        //开始移动，播放移动动画
        private void StartMove(object[] var)
        {
            if (_animation != null)
            {
                if (!_animation.IsPlaying("Walk"))
                {
                    _animation.Play("Walk");
                }
            }
            if (_animator != null)
            {
                var collection = _animator.GetCurrentAnimatorClipInfo(0);

                foreach (var item in collection)
                {
                    if (item.clip.name == "Walk")
                    {
                        _animator.Play("Walk");
                    }
                }
            }

        }

        //播放停止移动动画
        private void StopMove(object[] var)
        {
            if (_animation != null)
            {
                if (!_animation.IsPlaying("Idle_01"))
                {
                    _animation.Play("Idle_01");
                }
            }
            if (_animator != null)
            {
                var collection = _animator.GetCurrentAnimatorClipInfo(0);

                foreach (var item in collection)
                {
                    if (item.clip.name == "Idle_01")
                    {
                        _animator.Play("Idle_01");
                    }
                }
            }
        }

        //第一种攻击方式
        private void Attack_01(object[] var)
        {
            if (_animation != null)
            {
                if (!_animation.IsPlaying("Attack_01"))
                {
                    _animation.Play("Attack_01");
                }
            }
            if (_animator != null)
            {
                var collection = _animator.GetCurrentAnimatorClipInfo(0);

                foreach (var item in collection)
                {
                    if (item.clip.name == "Attack_01")
                    {
                        _animator.Play("Attack_01");
                    }
                }
            }
        }

        //第二种攻击方式
        private void Attack_02(object[] var)
        {
            if (_animation != null)
            {
                if (!_animation.IsPlaying("Attack_02"))
                {
                    _animation.Play("Attack_02");
                }
            }
            if (_animator != null)
            {
                var collection = _animator.GetCurrentAnimatorClipInfo(0);

                foreach (var item in collection)
                {
                    if (item.clip.name == "Attack_02")
                    {
                        _animator.Play("Attack_02");
                    }
                }
            }
        }

        //播放死亡动画
        private void Die(object var)
        {
            if (_animation != null)
            {
                if (!_animation.IsPlaying("Die"))
                {
                    _animation.Play("Die");
                }
            }
            if (_animator != null)
            {
                var collection = _animator.GetCurrentAnimatorClipInfo(0);

                foreach (var item in collection)
                {
                    if (item.clip.name == "Die")
                    {
                        _animator.Play("Die");
                    }
                }
            }
        }
    }
}