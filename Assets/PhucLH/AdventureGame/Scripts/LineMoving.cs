using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class LineMoving : MonoBehaviour
    {
        public Direction moveDirection;
        public float movingDistance;
        public float speed;
        public bool isOnlyUp;
        public bool isAuto;

        private Vector2 destination;
        private Vector3 backDirection;
        private Vector3 startPosition;
        private Rigidbody2D m_rb;

        public Vector2 Destination { get => destination;}
        public Vector3 BackDirection { get => backDirection;}

        private void Awake()
        {
            m_rb = GetComponent<Rigidbody2D>();
            startPosition = transform.position;
        }
        private void Update()
        {
            backDirection = startPosition - transform.position;
            backDirection.Normalize();
        }
        private void FixedUpdate()
        {
            if (!isAuto) return;
            Move();
            SwitchDirectionChecking();
        }
        public void GetMovingDestination()
        {
            switch (moveDirection)
            {
                case Direction.Left:
                    destination = new Vector2(startPosition.x - movingDistance, transform.position.y);
                    break; 
                case Direction.Right:
                    destination = new Vector2(startPosition.x + movingDistance, transform.position.y);
                    break;
                case Direction.Up:
                    destination = new Vector2(transform.position.x, startPosition.y + movingDistance);
                    break;
                case Direction.Down:
                    destination = new Vector2(transform.position.x, startPosition.y - movingDistance);
                    break;
            }
        }
        public bool IsReachToDest()
        {
            float distance1 = Vector2.Distance(startPosition, transform.position);
            float distance2 = Vector2.Distance(startPosition, destination); 
            return distance1> movingDistance;
        }
        public void SwitchDirection(Vector2 direct)
        {
            if(moveDirection == Direction.Left || moveDirection == Direction.Right)
            {
                moveDirection = direct.x < 0 ? Direction.Left : Direction.Right;
            }    
            else if(moveDirection == Direction.Up || moveDirection == Direction.Down)
            {
                moveDirection = direct.y < 0 ? Direction.Down : Direction.Up;
            }    
        }
        public void SwitchDirectionChecking()
        {
            if(IsReachToDest())
            {
                SwitchDirection(backDirection);
                GetMovingDestination();
            }   
        }
        public void Move()
        {
            switch (moveDirection)
            {
                case Direction.Left:
                    m_rb.velocity = new Vector2(-speed, m_rb.velocity.y);                
                    break;
                case Direction.Right:
                    m_rb.velocity = new Vector2(speed, m_rb.velocity.y);
                    break;
                case Direction.Up:
                    m_rb.velocity = new Vector2(m_rb.velocity.x, speed);
                    transform.position = new Vector2(startPosition.x, transform.position.y);
                    break;
                case Direction.Down:
                    transform.position = new Vector2(startPosition.x, transform.position.y);
                    if (isOnlyUp) return;
                    m_rb.velocity = new Vector2(m_rb.velocity.x, -speed);
                    
                    break;
            }
        }

    }
}

