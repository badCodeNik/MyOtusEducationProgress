using Atomic.Elements;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_AtomicIntroduсtion
{
    public class ShootTargetMechanics
    {
        private readonly IAtomicAction _shootAction;
        private readonly IAtomicValue<Vector3> _targetPosition;
        private readonly IAtomicValue<Vector3> _rootPosition;
        private readonly IAtomicValue<float> _radius;
        private readonly IAtomicValue<bool> _isEnabled;

        public ShootTargetMechanics(
            IAtomicAction shootAction, 
            IAtomicValue<Vector3> targetPosition, 
            IAtomicValue<Vector3> rootPosition, 
            IAtomicValue<float> radius,
            IAtomicValue<bool> isEnabled)
        {
            _shootAction = shootAction;
            _targetPosition = targetPosition;
            _rootPosition = rootPosition;
            _radius = radius;
            _isEnabled = isEnabled;
        }

        public void Update()
        {
            if (!_isEnabled.Value)
            {
                return;
            }
            
            var direction = _targetPosition.Value - _rootPosition.Value;
            
            if (direction.magnitude < _radius.Value)
            {
                _shootAction.Invoke();
            }
        }
    }
}