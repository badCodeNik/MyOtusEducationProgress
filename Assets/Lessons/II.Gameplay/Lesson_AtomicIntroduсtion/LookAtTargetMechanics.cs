using Atomic.Elements;
using UnityEngine;

namespace Lessons.Lesson_AtomicIntroduсtion
{
    public class LookAtTargetMechanics
    {
        private readonly IAtomicAction<Vector3> _rotateAction;
        private readonly IAtomicValue<Vector3> _targetPoint;
        private readonly IAtomicValue<Vector3> _transform;
        private readonly IAtomicValue<bool> _isEnabled;

        public LookAtTargetMechanics(
            
            IAtomicAction<Vector3> rotateAction,
            IAtomicValue<Vector3> targetPoint,
            IAtomicValue<Vector3> transform,
            IAtomicValue<bool> isEnabled)
        {
            _rotateAction = rotateAction;
            _targetPoint = targetPoint;
            _transform = transform;
            _isEnabled = isEnabled;
        }

        public void Update()
        {
            if (!_isEnabled.Value)
            {
                return;
            }
            
            var direction = _targetPoint.Value - _transform.Value;
            _rotateAction.Invoke(direction);
        }
    }
}