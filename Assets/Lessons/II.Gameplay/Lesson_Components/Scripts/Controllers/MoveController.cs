using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private AtomicEntity _entity;

        private IAtomicVariable<Vector3> _moveDirection;

        private void Awake()
        {
            _moveDirection = _entity.Get<IAtomicVariable<Vector3>>(MoveAPI.MOVE_DIRECTION);
        }

        private void Update()
        {
            HandleKeyboard();
        }

        private void HandleKeyboard()
        {
            Move(Vector3.zero);
            
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.Move(Vector3.forward);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                this.Move(Vector3.back);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.Move(Vector3.left);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.Move(Vector3.right);
            }
        }
        
        private void Move(Vector3 direction)
        {
            if (_entity.TryGet(MoveAPI.MOVE_DIRECTION, out IAtomicVariable<Vector3> moveDirection))
            {
                moveDirection.Value = direction;
            }
        }
    }
}