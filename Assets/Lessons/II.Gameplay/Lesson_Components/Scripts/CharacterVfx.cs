using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    [Serializable]
    public class CharacterVfx
    {
        [SerializeField] private ParticleSystem _muzzleVfx;
        [SerializeField] private ParticleSystem _takeDamageVfx;
        
        private CharacterCore _core;

        public void Compose(CharacterCore core)
        {
            _core = core;
        }

        public void OnEnable()
        {
            _core.ShootComponent.ShootEvent.Subscribe(OnShoot);
            _core.LifeComponent.TakeDamageEvent.Subscribe(OnTakeDamage);
        }

        public void OnDisable()
        {
            _core.ShootComponent.ShootEvent.Unsubscribe(OnShoot);
            _core.LifeComponent.TakeDamageEvent.Unsubscribe(OnTakeDamage);
        }

        private void OnTakeDamage(int damage)
        {
            //Обработка с учетом урона
            _takeDamageVfx.Play();
        }

        private void OnShoot()
        {
            _muzzleVfx.Play();
        }
    }
}