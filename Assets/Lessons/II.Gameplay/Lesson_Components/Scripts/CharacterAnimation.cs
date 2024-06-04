using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    [Serializable]
    public class CharacterAnimation
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorDispatcher _animatorDispatcher;
        
        private CharacterCore _core;

        private BoolAnimationMechanics _moveAnimationMechanics;
        private BoolAnimationMechanics _boolAnimationMechanics;
        private ShootAnimationMechanics _shootAnimationMechanics;
        
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        public void Compose(CharacterCore characterCore)
        {
            _core = characterCore;
            _moveAnimationMechanics = 
                new BoolAnimationMechanics(_core.MoveComponent.IsMoving, _animator, IsMoving);
            _boolAnimationMechanics = 
                new BoolAnimationMechanics(_core.LifeComponent.IsDead, _animator, IsDead);
            _shootAnimationMechanics =
                new ShootAnimationMechanics(_animator, _animatorDispatcher,
                    _core.ShootComponent.ShootRequest, _core.ShootComponent.ShootAction, 
                    _core.ShootComponent.CanFire);
        }

        public void OnEnable()
        {
            _moveAnimationMechanics.OnEnable();
            _boolAnimationMechanics.OnEnable();
            _shootAnimationMechanics.OnEnable();
        }

        public void OnDisable()
        {
            _moveAnimationMechanics.OnDisable();
            _boolAnimationMechanics.OnDisable();
            _shootAnimationMechanics.OnDisable();
        }
    }
}