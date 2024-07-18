using System.Collections;
using System.Collections.Generic;
using Assets.Codebase.Mechanics.MoveSystem;
using UnityEngine;

namespace Assets.Codebase.Infrastructure
{
    public class Character : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Move(Vector2 direction, IMovable movable)
        {
            movable.Turn(direction);
        }
        public void Move(Vector2 direction)
        {
            GetComponent<IMovable>().Turn(direction);
        }

        public void SetAnimationTrigger(string trigger_name)
        {
            _animator?.SetTrigger(trigger_name);
        }
        public void SetAnimationBoolean(string bool_name, bool value) 
        {
            _animator?.SetBool(bool_name, value);
        }
        public void SetMoveValue(Vector2 value)
        {
            _animator?.SetFloat("horizontal", value.x);
            _animator?.SetFloat("vertical", value.y);
        }
    }
}