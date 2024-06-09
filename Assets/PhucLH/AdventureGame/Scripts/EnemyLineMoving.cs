using PhucLH.AdventureGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    [RequireComponent(typeof(LineMoving))]
    public class EnemyLineMoving : Enemy
    {
        private LineMoving lineMoving;

        protected override void Awake()
        {
            base.Awake();
            lineMoving = GetComponent<LineMoving>();
            FSMInit(this);
        }
        public override void Start()
        {
            base.Start();
            movingDistance = lineMoving.movingDistance;
        }
        public override void Move()
        {
            if (m_isKnockBack) return;

            lineMoving.Move();
            Flip(lineMoving.moveDirection);
        }
        #region FSM
        protected override void Moving_Update()
        {
            base.Moving_Update();
            targetDirection = lineMoving.BackDirection;
            lineMoving.speed = m_curSpeed ;
        }
        protected override void Chasing_Enter()
        {
            base.Chasing_Enter();
            GetTargetDirection();
            lineMoving.SwitchDirection(targetDirection); 
        }
        protected override void Chasing_Update()
        {
            base.Chasing_Update();
            GetTargetDirection();
            lineMoving.speed = m_curSpeed;
        }
        protected override void Chasing_Exit()
        {
            base.Chasing_Exit();
            lineMoving.SwitchDirectionChecking();
        }
        protected override void GotHit_Update()
        {
            base.GotHit_Update();
            lineMoving.SwitchDirectionChecking();
            GetTargetDirection();
            if(!m_isKnockBack)
            {
                fsm.ChangeState(EnemyAnimState.Moving);
            }    
        }
        #endregion
    }
}

