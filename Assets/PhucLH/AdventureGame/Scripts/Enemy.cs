using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using System;

namespace PhucLH.AdventureGame
{
    public class Enemy : Actor
    {
        [Header("Moving: ")]
        public float movingDistance;

        protected PlayerDetect playerDetect;
        protected EnemyStat currentStat;
        protected Vector2 movingDirection;
        protected Vector2 movingDirectionBackup;
        protected Vector2 startingPosition;
        protected Vector2 targetDirection;

        protected StateMachine<EnemyAnimState> fsm;

        public bool IsDead
        {
            get => fsm.State == EnemyAnimState.Dead;
        }
        protected override void Awake()
        {
            base.Awake();
            playerDetect = GetComponent<PlayerDetect>();
            startingPosition = transform.position;

        }
        protected void FSMInit(MonoBehaviour behaviour)
        {
            fsm = StateMachine<EnemyAnimState>.Initialize(behaviour);
            fsm.ChangeState(EnemyAnimState.Moving);
        }

        protected override void Init()
        {
            if(stat !=null)
            {
                currentStat = (EnemyStat)stat;
            }
        }
        protected virtual void Update()
        {
            if(IsDead)
            {
                fsm.ChangeState(EnemyAnimState.Dead);
            }
            if (m_isKnockBack || IsDead) return;

            if (playerDetect.IsDetected)
            {
                fsm.ChangeState(EnemyAnimState.Chasing);
            }
            else
            {
                fsm.ChangeState(EnemyAnimState.Moving);
            }
            if(m_rb.velocity.y<-50)
            {
                Dead();
            }
        }
        protected virtual void FixedUpdate()
        {
            if (m_isKnockBack || IsDead) return;

            Move();
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        protected override void Dead()
        {
            base.Dead();
            fsm.ChangeState(EnemyAnimState.Dead);
        }
        protected void GetTargetDirection()
        {
            targetDirection = playerDetect.Target.transform.position - transform.position;
            targetDirection.Normalize();
        }
        public override void TakeDamage(int dmg, Actor whoHit = null)
        {
            base.TakeDamage(dmg, whoHit);
            if (m_curHp > 0 && !m_isInvincible)
            {
                fsm.ChangeState(EnemyAnimState.GotHit);
            }
        }
        #region FSM
        protected virtual void Moving_Enter() { }
        protected virtual void Moving_Update() {
            m_curSpeed = currentStat.moveSpeed;
            Helper.PlayAnim(m_anim, EnemyAnimState.Moving.ToString());
        }
        protected virtual void Moving_Exit() { }
        protected virtual void Chasing_Enter() { }
        protected virtual void Chasing_Update() {
            m_curSpeed = currentStat.chasingSpeed;
            Helper.PlayAnim(m_anim, EnemyAnimState.Chasing.ToString());
        }
        protected virtual void Chasing_Exit() { }
        protected virtual void GotHit_Enter() { }
        protected virtual void GotHit_Update() { }
        protected virtual void GotHit_Exit() { }
        protected virtual void Dead_Enter() {
            if(deadVfxPb)
            {
                Instantiate(deadVfxPb, transform.position, Quaternion.identity);
            }
            gameObject.SetActive(false);
        }
        protected virtual void Dead_Update() { }
        protected virtual void Dead_Exit() { }
        #endregion

    }
}

