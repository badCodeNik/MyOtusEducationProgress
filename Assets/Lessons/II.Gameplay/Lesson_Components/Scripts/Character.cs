using System;
using Atomic.Elements;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class MoveAPI
    {
        public const string MOVE_DIRECTION = nameof(MOVE_DIRECTION);
    }

    public class ShootAPI
    {
        public const string SHOOT_REQUEST = nameof(SHOOT_REQUEST);
    }

    public class LifeAPI
    {
        public const string TAKE_DAMAGE_ACTION = nameof(TAKE_DAMAGE_ACTION);
    }
    
    //Facade
    public class Character : AtomicEntity
    {
        [Get(MoveAPI.MOVE_DIRECTION)]
        public IAtomicVariable<Vector3> MoveDirection => _characterCore.MoveComponent.MoveDirection;

        [Get(ShootAPI.SHOOT_REQUEST)] 
        public IAtomicAction ShootRequest => _characterCore.ShootComponent.ShootRequest;

        [Get(LifeAPI.TAKE_DAMAGE_ACTION)]
        public IAtomicAction<int> TakeDamageAction => _characterCore.LifeComponent.TakeDamageAction;
        
        //Секции
        [SerializeField] private CharacterCore _characterCore;
        
        //View
        [SerializeField] private CharacterAnimation _characterAnimation;
        [SerializeField] private CharacterVfx _vfx;
        [SerializeField] private CharacterAudio _audio;

        private void Awake()
        {
            _characterCore.Compose();
            _characterAnimation.Compose(_characterCore);
            _vfx.Compose(_characterCore);
            _audio.Compose(_characterCore);
        }

        private void OnEnable()
        {
            _characterAnimation.OnEnable();
            _vfx.OnEnable();
            _audio.OnEnable();
        }

        private void OnDisable()
        {
            _characterAnimation.OnDisable();
            _vfx.OnDisable();
            _audio.OnDisable();
        }

        private void Update()
        {
            _characterCore.Update(Time.deltaTime);
        }
    }
}
