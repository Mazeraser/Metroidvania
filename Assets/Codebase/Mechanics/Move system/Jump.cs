using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Mechanics.MoveSystem
{
    public class Jump : MovableParent, IMovable
    {
        //TODO: add jump counter

        [SerializeField]
        private float _jumpPower;

        [SerializeField]
        private int _maxJumpCount;
        private int _jumpCount;

        private bool _isGrounded;

        public float Speed
        {
            get { return _jumpPower; }
        }

        public void Turn(Vector2 direction)
        {
            if (_isGrounded)
            {
                Jump(new Vector2(0, 1), _jumpPower);
                _jumpCount++;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _isGrounded = collision.gameObject.tag == "Ground";
            Debug.Log(_isGrounded);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            _isGrounded = collision.gameObject.tag != "Ground";
            Debug.Log(_isGrounded);
        }
    }
}
    
