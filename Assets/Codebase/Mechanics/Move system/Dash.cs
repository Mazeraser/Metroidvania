using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Mechanics.MoveSystem
{

    public class Dash : MovableParent, IMovable
    {
        //TODO: add side changer
        
        [SerializeField]
        private float _dashPower;
        public float Speed { get { return _dashPower; } }

        [SerializeField]
        private float _dashCoolDown;

        private float timer;

        private bool _isRecovery;

        public override void Start()
        {
            _isRecovery = false;
            timer = 0;
            base.Start();
        }
        private void Update()
        {
            if (_isRecovery)
            {
                timer += Time.deltaTime;
                if (timer > _dashCoolDown)
                {
                    _isRecovery = false;
                    Debug.Log("Dash is recovered.");
                    timer = 0f;
                }
            }
        }
        public void Turn(Vector2 direction)
        {
            if (!_isRecovery)
            {
                Jump(new Vector2(direction.x>0?1:-1, 0), _dashPower);
                _isRecovery = true;
                timer = 0;
            }
        }
    }
}