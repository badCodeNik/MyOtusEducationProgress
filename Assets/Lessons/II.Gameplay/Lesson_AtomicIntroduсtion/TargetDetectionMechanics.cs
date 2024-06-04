using Atomic.Elements;
using UnityEngine;

namespace Lessons.Lesson_AtomicIntroduсtion
{
    public class TargetDetectionMechanics
    {
        private readonly IAtomicVariable<GameObject> _target;
        private readonly IAtomicValue<float> _radius;
        private readonly IAtomicValue<Vector3> _myPosition;
        private readonly IAtomicValue<LayerMask> _layerMask;

        public TargetDetectionMechanics(
            IAtomicValue<float> radius,
            IAtomicValue<Vector3> myPosition,
            IAtomicVariable<GameObject> target,
            IAtomicValue<LayerMask> layerMask)
        {
            _radius = radius;
            _myPosition = myPosition;
            _target = target;
            _layerMask = layerMask;
        }

        public void FixedUpdate()
        {
            var colliders = Physics.OverlapSphere(_myPosition.Value, _radius.Value, _layerMask.Value);
            var minDistance = float.MaxValue;
            GameObject target = null;
            
            foreach (var collider in colliders)
            {
                var obj = collider.gameObject;
                var distance = (obj.transform.position - _myPosition.Value).sqrMagnitude;
                
                if (distance <= minDistance)
                {
                    minDistance = distance;
                    target = collider.gameObject;
                }
            }
            
            _target.Value = target;
        }
    }
}
