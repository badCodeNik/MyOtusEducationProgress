using System;
using Atomic.Elements;
using Lessons.Lesson_AtomicIntroduсtion;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    [Serializable]
    public class CharacterCore
    {
        public MoveComponent MoveComponent;
        public LifeComponent LifeComponent;
        public RotationComponent RotationComponent;
        public ShootComponent ShootComponent;

        [SerializeField] private Collider _collider;
        [SerializeField] private Transform _targetPoint;

        private LookAtTargetMechanics _lookAtTargetMechanics;
        
        public void Compose()
        {
            MoveComponent.Compose();
            MoveComponent.AppendCondition(LifeComponent.IsAlive);
            // MoveComponent.AppendCondition(ShootComponent.CanFire.Invoke);
            
            RotationComponent.Construct();
            RotationComponent.AppendCondition(LifeComponent.IsAlive);
            
            ShootComponent.Construct();

            var targetPosition = new AtomicFunction<Vector3>(() =>
            {
                return _targetPoint.position;
            });

            var rootPosition = new AtomicFunction<Vector3>(() =>
            {
                return RotationComponent.RotationRoot.position;
            });

            var hasTarget = new AtomicFunction<bool>(() =>
            {
                return _targetPoint != null;
            });
            
            _lookAtTargetMechanics =
                new LookAtTargetMechanics(RotationComponent.RotateAction, targetPosition,
                    rootPosition, hasTarget);
            
            LifeComponent.IsDead.Subscribe(isDead => _collider.enabled = !isDead);
        }

        public void Update(float deltaTime)
        {
            MoveComponent.Update(deltaTime);
            ShootComponent.Update(deltaTime);
            
            _lookAtTargetMechanics.Update();
        }
    }
}