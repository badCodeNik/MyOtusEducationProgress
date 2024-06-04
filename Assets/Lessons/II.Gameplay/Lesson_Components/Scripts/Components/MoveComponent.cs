using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components.Components
{
    [Serializable]
    public class MoveComponent
    {
        public AtomicVariable<Vector3> MoveDirection;
        public AtomicVariable<bool> IsMoving;

        [SerializeField] private Transform _root;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private bool _canMove;

        private readonly CompositeCondition _condition = new();

        public void Compose()
        {
            MoveDirection.Subscribe(moveDirection =>
            {
                IsMoving.Value = moveDirection != Vector3.zero;
            });
        }

        public void Update(float deltaTime)
        {
            if (_condition.IsTrue() && _canMove)
            {
                _root.position += MoveDirection.Value * _speed * deltaTime;
            }
        }

        public void AppendCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }
    }
}