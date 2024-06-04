using System;
using Atomic.Elements;
using UnityEngine;

namespace Lessons.Lesson_Components.Components
{
    [Serializable]
    public class RotationComponent
    {
        public Transform RotationRoot => _rotationRoot;
        
        [SerializeField] private Transform _rotationRoot;
        [SerializeField] private Vector3 _rotateDirection;
        [SerializeField] private float _rotateRate;
        [SerializeField] private bool _canRotate;
        
        private readonly CompositeCondition _condition = new();

        public AtomicAction<Vector3> RotateAction;

        public void Construct()
        {
            RotateAction.Compose(Rotate);
        }
        
        public void Rotate(Vector3 forwardDirection)
        {
            _rotateDirection = forwardDirection;

            if (!_canRotate || !_condition.IsTrue())
            {
                return;
            }

            if (forwardDirection == Vector3.zero)
            {
                return;
            }

            var targetRotation = Quaternion.LookRotation(_rotateDirection, Vector3.up);
            _rotationRoot.rotation = Quaternion.Lerp(_rotationRoot.rotation, targetRotation, _rotateRate);
        }
        
        public void AppendCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }
    }
}