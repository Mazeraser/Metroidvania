using UnityEngine;

namespace Assets.Codebase.Mechanics.MoveSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public abstract class MovableParent : MonoBehaviour
    {
        private Collider2D _collider;
        private Rigidbody2D _rb;

        public virtual void Start()
        {
            _collider = GetComponent<Collider2D>();
            _rb = GetComponent<Rigidbody2D>();
        }

        internal void Move(Vector2 direction, float speed)
        {
            //Physical realization, gives momentum to the body and it "rolls"
            //IMPORTANT: CapsuleCollider gives the minimal braking due to frictional force

            Vector2 movement = new Vector2(direction.x, direction.y);

            //_rb.AddForce(movement * Speed); // first variant
            _rb.AddForce(movement * speed, ForceMode2D.Impulse); // second variant


            //Non-physical realization, moves body through coordinate system, uses teleportation
            // so that the speed would be stable in any case
            // and given that we call from FixedUpdate we multiply by fixedDeltaTime. 
            // Thanks to FixedUpdate there will be no twitching when fps drops
            //transform.Translate(movement * speed * Time.fixedDeltaTime);

            /*
             Features:

             -The character stops for a while.

             -The running jump is stronger.

             -The character moves faster in a jump
             */
        }
        internal void Jump(Vector2 direction, float jumpForce)
        {
            _rb.AddForce(direction * jumpForce);
        }
    }
}