using Assets.Codebase.Infrastructure;
using Assets.Codebase.Mechanics.MoveSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Codebase.Mechanics.ControllSystem
{
    public class Player : MonoBehaviour,IControllable
    {
        private Main inputAction;

        private void Awake()
        {
            inputAction = new Main();

            inputAction.Move.Dash.performed += context => Dash(inputAction.Move.Move.ReadValue<Vector2>());
            inputAction.Move.Jump.performed += context => Jump(inputAction.Move.Move.ReadValue<Vector2>());
            inputAction.Move.Attack.performed += context => Attack();
            inputAction.Move.Shield.performed += context => Shield();
        }
        private void OnEnable()
        {
            inputAction.Enable();
        }
        private void OnDisable()
        {
            inputAction.Disable();
        }

        private void FixedUpdate()
        {
            ControllMove(inputAction.Move.Move.ReadValue<Vector2>());
        }

        public void ControllMove(Vector2 direction)
        {
            GetComponent<Character>()?.Move(direction, GetComponent<Walk>());
        }
        private void Jump(Vector2 direction) 
        {
            GetComponent<Character>()?.Move(direction, GetComponent<Jump>());
        }
        private void Dash(Vector2 direction)
        {
            GetComponent<Character>()?.Move(direction, GetComponent<Dash>());
        }
        private void Attack()
        {
            Debug.Log("Player is attacking");
        }
        private void Shield()
        {
            Debug.Log("Player is using shield");
        }
    }
}