using System;
using Atomic.Elements;
using Atomic.Objects;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private AtomicEntity _entity;

        private IAtomicAction _shootRequest;

        private void Awake()
        {
            _shootRequest = _entity.Get<IAtomicAction>(ShootAPI.SHOOT_REQUEST);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _shootRequest.Invoke();
            }
        }
    }
}