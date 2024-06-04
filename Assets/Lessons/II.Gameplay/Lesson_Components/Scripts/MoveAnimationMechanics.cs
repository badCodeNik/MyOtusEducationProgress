using Atomic.Elements;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class MoveAnimationMechanics
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private readonly IAtomicObservable<Vector3> _moveDirection;
        private readonly Animator _animator;

        public MoveAnimationMechanics(
            IAtomicObservable<Vector3> moveDirection, 
            Animator animator)
        {
            _moveDirection = moveDirection;
            _animator = animator;
        }

        public void OnEnable()
        {
            _moveDirection.Subscribe(OnMoveDirectionChanged);
        }

        public void OnDisable()
        {
            _moveDirection.Unsubscribe(OnMoveDirectionChanged);
        }
        
        private void OnMoveDirectionChanged(Vector3 moveDirection)
        {
            Debug.Log("move direction changed");
            _animator.SetBool(IsMoving, moveDirection != Vector3.zero);
        }
    }
}