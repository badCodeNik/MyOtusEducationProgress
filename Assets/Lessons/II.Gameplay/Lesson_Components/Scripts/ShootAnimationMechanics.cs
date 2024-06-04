using Atomic.Elements;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class ShootAnimationMechanics
    {
        private Animator _animator;
        private AnimatorDispatcher _animatorDispatcher;
        private readonly IAtomicObservable _shootRequest;
        private readonly IAtomicAction _shootAction;
        private readonly IAtomicValue<bool> _canFire;

        public ShootAnimationMechanics(
            Animator animator, 
            AnimatorDispatcher animatorDispatcher,
            IAtomicObservable shootRequest,
            IAtomicAction shootAction,
            IAtomicValue<bool> canFire)
        {
            _animator = animator;
            _animatorDispatcher = animatorDispatcher;
            _shootRequest = shootRequest;
            _shootAction = shootAction;
            _canFire = canFire;
        }

        public void OnEnable()
        {
            _shootRequest.Subscribe(OnShootRequested);
            _animatorDispatcher.SubscribeOnEvent("shoot", _shootAction.Invoke);
        }

        public void OnDisable()
        {
            _shootRequest.Unsubscribe(OnShootRequested);
            _animatorDispatcher.UnsubscribeOnEvent("shoot", _shootAction.Invoke);
        }
        
        private void OnShootRequested()
        {
            if (_canFire.Value)
            {
                _animator.SetTrigger("Shoot");
            }
        }
    }
}