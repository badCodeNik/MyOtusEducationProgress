using System;
using Atomic.Elements;
using Atomic.Objects;
using Lessons.Lesson_AtomicIntroduсtion;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_Components.Scripts
{
    //Facade
    public class Tower : AtomicEntity
    {
        [Get(LifeAPI.TAKE_DAMAGE_ACTION)]
        public IAtomicAction<int> TakeDamageAction => _lifeComponent.TakeDamageAction;
        
        [SerializeField] private RotationComponent _rotationComponent;
        [SerializeField] private LifeComponent _lifeComponent;
        [SerializeField] private ShootComponent _shootComponent;

        [SerializeField] private AtomicVariable<GameObject> _target;
        [SerializeField] private AtomicVariable<float> _radius;
        [SerializeField] private AtomicValue<LayerMask> _layerMask;

        private LookAtTargetMechanics _lookAtTargetMechanics;
        private ShootTargetMechanics _shootTargetMechanics;
        private TargetDetectionMechanics _targetDetectionMechanics;

        private void Awake()
        {
            _rotationComponent.Construct();
            _rotationComponent.AppendCondition(_lifeComponent.IsAlive);
            
            _shootComponent.Construct();
            _shootComponent.AppendCondition(_lifeComponent.IsAlive);
            
            _lifeComponent.Compose();

            var targetPosition = new AtomicFunction<Vector3>(() =>
            {
                return _target.Value.transform.position;
            });

            var rootPosition = new AtomicFunction<Vector3>(() =>
            {
                return _rotationComponent.RotationRoot.position;
            });
            
            var hasTarget = new AtomicFunction<bool>(() =>
            {
                return _target.Value != null;
            });

            _lookAtTargetMechanics =
                new LookAtTargetMechanics(_rotationComponent.RotateAction, targetPosition, 
                    rootPosition, hasTarget);
            _shootTargetMechanics =
                new ShootTargetMechanics(_shootComponent.ShootAction, targetPosition, rootPosition, _radius, hasTarget);
            _targetDetectionMechanics = new TargetDetectionMechanics(_radius, rootPosition,
                _target, _layerMask);
        }

        private void FixedUpdate()
        {
            _targetDetectionMechanics.FixedUpdate();
        }

        private void Update()
        {
            _shootComponent.Update(Time.deltaTime);
            
            _lookAtTargetMechanics.Update();
            _shootTargetMechanics.Update();
        }
    }
}