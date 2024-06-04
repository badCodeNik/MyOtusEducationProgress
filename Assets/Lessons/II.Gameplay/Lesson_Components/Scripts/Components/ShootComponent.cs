using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Lessons.Lesson_Components.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components.Components
{
    [Serializable]
    public class ShootComponent
    {
        public AtomicEvent ShootRequest;
        public AtomicEvent ShootAction;
        public AtomicEvent ShootEvent;

        public AtomicFunction<bool> CanFire;
        
        [SerializeField] private float _reloadTime = 2f;
        [SerializeField] private bool _isReloading;
        [SerializeField] private bool _canFire;
        [SerializeField] private AtomicEntity _bulletPrefab;
        [SerializeField] private Transform _firePoint;
        
        [ShowInInspector, ReadOnly]
        private float _reloadTimer;
        
        private readonly CompositeCondition _condition = new();

        public void Construct()
        {
            ShootAction?.Subscribe(Shoot);
            CanFire.Compose(()=> _canFire && !_isReloading);
        }

        public void Update(float deltaTime)
        {
            if (_isReloading)
            {
                _reloadTimer -= deltaTime;
                
                if (_reloadTimer <= 0)
                {
                    _isReloading = false;
                }
            }
        }
        
        public void Shoot()
        {
            if (!CanFire.Value)
            {
                return;
            }

            var bullet = GameObject.Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

            if (bullet.TryGetVariable<Vector3>(MoveAPI.MOVE_DIRECTION, out var moveDirection))
            {
                moveDirection.Value = _firePoint.forward;
            }
            
            _reloadTimer = _reloadTime;
            _isReloading = true;
            
            ShootEvent.Invoke();
            Debug.Log("Fire!");
        }
        
        public void AppendCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }
    }
}