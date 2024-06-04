using Atomic.Elements;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class BoolAnimationMechanics
    {
        private readonly IAtomicObservable<bool> _value;
        private readonly Animator _animator;
        private readonly int _animatorKey;

        public BoolAnimationMechanics(IAtomicObservable<bool> value, Animator animator, int animatorKey)
        {
            _value = value;
            _animator = animator;
            _animatorKey = animatorKey;
        }

        public void OnEnable()
        {
            _value.Subscribe(OnIsDeadChanged);
        }

        public void OnDisable()
        {
            _value.Unsubscribe(OnIsDeadChanged);
        }

        private void OnIsDeadChanged(bool isDead)
        {
            _animator.SetBool(_animatorKey, isDead);
        }
    }
}