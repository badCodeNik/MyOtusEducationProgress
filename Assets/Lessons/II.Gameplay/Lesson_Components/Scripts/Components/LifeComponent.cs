using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components.Components
{
    [Serializable]
    public class LifeComponent
    {
        public AtomicEvent<int> TakeDamageAction;
        public AtomicEvent<int> TakeDamageEvent;
        public AtomicVariable<bool> IsDead;
        [SerializeField] private int _hitPoints;

        public void Compose()
        {
            TakeDamageAction.Subscribe(TakeDamage);
        }
        
        public bool IsAlive()
        {
            return !IsDead.Value;
        }
        
        [Button]
        public void TakeDamage(int damage)
        {
            if (IsDead.Value)
            {
                return;
            }
            
            _hitPoints -= damage;
            TakeDamageEvent.Invoke(damage);
            Debug.Log($"Take damage = {damage}");
            
            if (_hitPoints <= 0)
            {
                IsDead.Value = true;
            }
        }
    }
}