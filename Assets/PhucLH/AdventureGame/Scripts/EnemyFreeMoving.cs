using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class EnemyFreeMoving : Enemy
    {
        public bool canRotate;
        protected float x_leftPosition;
        protected float x_rightPosition;
        protected float y_topPosition;
        protected float y_downPosition;

        private bool haveMovingPosition;
        private Vector2 movingPosition;

        protected override void Awake()
        {
            base.Awake();
            FSMInit(this);
        }
        protected override void Update()
        {
            base.Update();
            GetTargetDirection();
        }
        public void FindMaxMovingPosition()
        {
            x_leftPosition = startingPosition.x - movingDistance;
            x_rightPosition = startingPosition.x + movingDistance;
            y_topPosition = startingPosition.y + movingDistance;
            y_downPosition = startingPosition.y - movingDistance;
        }
        public override void Move()
        {
            if (m_isKnockBack) return;

            if(!haveMovingPosition)
            {
                float randomPositionX = Random.Range(x_leftPosition,x_rightPosition);
                float randomPositionY = Random.Range(y_downPosition, y_topPosition);
                
                InWaterChecking(ref randomPositionX,ref randomPositionY);
                
                movingPosition = new Vector2(randomPositionX, randomPositionY);
                movingDirection = movingPosition - (Vector2)transform.position;
                movingDirection.Normalize();
                movingDirectionBackup = movingDirection;
                haveMovingPosition = true;
            }
            float angle = 0;
            if(canRotate)
            {
                angle = Mathf.Atan2(movingDirection.y, movingDirection.x) * Mathf.Rad2Deg;
            }
            if(movingDirection.x>0)
            {
                if(canRotate)
                {
                    angle = Mathf.Clamp(angle, -41, 41);
                    transform.rotation  = Quaternion.Euler(0f, 0f, angle);
                }
                Flip(Direction.Right);
            }
            else if (movingDirection.x < 0)
            {
                if(canRotate)
                {
                    float newAngle = angle + 180f;
                    newAngle = Mathf.Clamp(newAngle, 25, 325);
                    transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
                }    
                Flip(Direction.Left);
            }
            CheckDestinationReached();
        }

        private void CheckDestinationReached()
        {
            if(Vector2.Distance(transform.position, movingPosition)<=0.5f)
            {
                haveMovingPosition = false;
            }    
            else
            {
                m_rb.velocity = movingDirection * m_curSpeed;
            }    
        }
        #region FSM
        protected override void Moving_Enter()
        {
            base.Moving_Enter();
            haveMovingPosition = false;
            FindMaxMovingPosition();
        }
        protected override void Chasing_Update()
        {
            base.Chasing_Update();
            movingDirection = targetDirection;
        }
        protected override void GotHit_Update()
        {
            if (m_isKnockBack)
            {
                KnockBackMove(targetDirection.y);
            }    
            else
            {
                fsm.ChangeState(EnemyAnimState.Moving);
            }    
        }
        #endregion

        protected virtual void InWaterChecking(ref float x,ref float y)
        {
            
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + movingDistance, transform.position.y, transform.position.z));

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - movingDistance, transform.position.y, transform.position.z));
        }
        
    }
}

