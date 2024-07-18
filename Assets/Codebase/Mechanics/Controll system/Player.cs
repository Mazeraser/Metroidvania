using Assets.Codebase.Infrastructure;
using Assets.Codebase.Mechanics.MoveSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Codebase.Mechanics.ControllSystem
{
    public class Player : MonoBehaviour,IControllable
    {
        private Main inputAction;

        private Character _character;

        [SerializeField]
        private float _jumpTime;
        private float timer;

        private bool _jumped;

        private bool _isTurnedOnRight;

        private void Awake()
        {
            inputAction = new Main();

            inputAction.Move.Jump.performed += context => _character.SetAnimationTrigger("Jump");
            inputAction.Move.Dash.performed += context => Dash();
            inputAction.Move.Attack.performed += context => Attack();
            inputAction.Move.Shield.performed += context => Shield(); 
            
            GetComponent<Jump>().Grounded += delegate { _character.SetAnimationTrigger("Idle"); };
        }
        private void OnEnable()
        {
            inputAction.Enable();
        }
        private void OnDisable()
        {
            inputAction.Disable();
        }

        private void Start()
        {
            _character = GetComponent<Character>();
        }

        private void FixedUpdate()
        {
            _character.SetAnimationBoolean("IsTurnedOnRight", _isTurnedOnRight);
            _character.SetAnimationBoolean("Grounded", GetComponent<Jump>().IsGrounded);

            ControllMove(inputAction.Move.Move.ReadValue<Vector2>());

            if (inputAction.Move.Jump.ReadValue<float>() != 0f && !_jumped)
            {
                timer += Time.fixedDeltaTime;
                Jump(inputAction.Move.Move.ReadValue<Vector2>());
            }
            else if (!GetComponent<Jump>().IsGrounded && inputAction.Move.Jump.ReadValue<float>() == 0)
            {
                _jumped = true;
            }
            else if(GetComponent<Jump>().IsGrounded)
            {
                timer = 0;
                _jumped = false;
            }
        }

        public void ControllMove(Vector2 direction)
        {
            _character.Move(direction, GetComponent<Walk>());

            _character.SetMoveValue(GetComponent<Rigidbody2D>().velocity);
            if (direction.x > 0)
                _isTurnedOnRight = true;
            else if (direction.x == 0)
                _character.SetAnimationTrigger("Idle");
            else
                _isTurnedOnRight = false;
        }
        private void Jump(Vector2 direction)
        {
            if (timer < _jumpTime)
            {
                _character.Move(direction, GetComponent<Jump>());
            }
        }
        private void Dash()
        {
            _character.Move(new Vector2(_isTurnedOnRight?1:-1,0), GetComponent<Dash>()); 
            _character.SetAnimationTrigger("Dash");
        }
        private void Attack()
        {
            Debug.Log("Player is attacking"); 
            _character.SetAnimationTrigger("Attack");
        }
        private void Shield()
        {
            Debug.Log("Player is using shield");
            _character.SetAnimationTrigger("Shield");
        }
    }
}