using System;
using Atomic.Elements;
using Atomic.Objects;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_Components.Scripts
{
    //Facade
    public class Bullet : AtomicEntity
    {
        [Get(MoveAPI.MOVE_DIRECTION)]
        public IAtomicVariable<Vector3> MoveDirection => MoveComponent.MoveDirection;
        
        [SerializeField] private int _damage = 1;
        [SerializeField] private MoveComponent MoveComponent;
        
        private void Awake()
        {
            MoveComponent.Compose();
        }

        private void Update()
        {
            MoveComponent.Update(Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IAtomicEntity atomicEntity))
            {
                if (atomicEntity.TryGet<IAtomicAction<int>>(LifeAPI.TAKE_DAMAGE_ACTION, out var action))
                {
                    action.Invoke(_damage);
                }
            }
        }
    }
}