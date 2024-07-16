using System.Collections;
using System.Collections.Generic;
using Assets.Codebase.Mechanics.MoveSystem;
using UnityEngine;

namespace Assets.Codebase.Infrastructure
{
    public class Character : MonoBehaviour
    {
        public void Move(Vector2 direction, IMovable movable)
        {
            movable.Turn(direction);
        }
        public void Move(Vector2 direction)
        {
            GetComponent<IMovable>().Turn(direction);
        }
    }
}