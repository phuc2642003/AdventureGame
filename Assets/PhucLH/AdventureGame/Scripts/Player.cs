using PhucLH.AdventureGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using System.Resources;
using UnityEngine.Experimental.AI;
using System.Security.Cryptography;

namespace PhucLH.AdventureGame
{
    public class Player : Actor
    {
        private StateMachine<PlayerAnimState> fsm;
        [Header("Smooth Jumping Setting")]
        [Range(0f, 5f)]
        public float jumpingFallingMultipler = 2.5f;
        [Range(0f, 5f)]
        public float lowJumpingMultipler = 2.5f;

        [Header("Reference:")]
        public SpriteRenderer sp;
        public ObstacleChecker obstacleChecker;
        public CapsuleCollider2D defaultColider;
        public CapsuleCollider2D flyingColider;
        public CapsuleCollider2D inWaterColider;

        private PlayerStat currentStat;
        private PlayerAnimState previousState;
        private float waterFallingTime = 1f;
        private float attackTime;
        private bool isAttacked;

        private bool IsDead
        {
            get => fsm.State == PlayerAnimState.Dead || previousState == PlayerAnimState.Dead;
        }    
        private bool IsJumping
        {
            get => fsm.State == PlayerAnimState.Jump || fsm.State == PlayerAnimState.Land || fsm.State == PlayerAnimState.Onair;
        }    
        private bool IsFlying
        {
            get => fsm.State == PlayerAnimState.Onair || fsm.State == PlayerAnimState.Fly || fsm.State == PlayerAnimState.FlyOnAir;
        }
        private bool IsAttacking
        {
            get => fsm.State == PlayerAnimState.HammerAttack || fsm.State == PlayerAnimState.FireBullet;
        }

        protected override void Awake()
        {
            base.Awake();
            fsm = StateMachine<PlayerAnimState>.Initialize(this);
            fsm.ChangeState(PlayerAnimState.Idle);
        }

        protected override void Init()
        {
            base.Init();
            if(stat!=null)
            {
                currentStat = (PlayerStat)stat;
            }    
        }
        private void Update()
        {
            if (IsAttacking || m_isKnockBack) return;

            ActionHandle();
        }
        private void ActionHandle()
        {
            if(fsm.State!= PlayerAnimState.OnLadder && fsm.State != PlayerAnimState.LadderIIdle && obstacleChecker.IsOnLadder)
            {
                ChangeState(PlayerAnimState.LadderIIdle);
            }
        }
        public void ChangeState(PlayerAnimState state)
        {
            previousState = fsm.State;
            fsm.ChangeState(state);
        }

        private IEnumerator ChangeStateDelayCourotine(PlayerAnimState newState, float extraTime=0)
        {
            var animClip = Helper.GetClip(m_anim, fsm.State.ToString());
            if(animClip!=null)
            {
                yield return new WaitForSeconds(animClip.length + extraTime);
                ChangeState(newState);
            }
            yield return null;
        }

        private void ChangeStateDelay(PlayerAnimState newState, float extraTime = 0)
        {
            StartCoroutine(ChangeStateDelayCourotine(newState, extraTime));
        }
        private void ActiveColider(PlayerCollider colider)
        {
            if (defaultColider)
            {
                defaultColider.enabled = colider == PlayerCollider.Default;
            }
            if (flyingColider)
            {
                flyingColider.enabled = colider == PlayerCollider.Flying;
            }
            if (inWaterColider)
            {
                inWaterColider.enabled = colider == PlayerCollider.InWater;
            }
        }
        
        protected override void Dead()
        {
            base.Dead();

        }
        private void Move(Direction direction)
        {
            if (m_isKnockBack) return;

            m_rb.isKinematic = false;

            if(direction == Direction.Left|| direction == Direction.Right)
            {
                Flip(direction);
                m_hozDir = direction == Direction.Left ? -1 : 1;
                m_rb.velocity = new Vector2(m_hozDir * m_curSpeed, m_rb.velocity.y);
            } 
            else if(direction == Direction.Up || direction== Direction.Down)
            {
                m_vertDir = direction == Direction.Down ? -1 : 1;
                m_rb.velocity = new Vector2(m_rb.velocity.x, m_vertDir * m_curSpeed);
            }    
        }   
        private void Jump()
        {
            GamepadController.Ins.CanJump = false;
            m_rb.velocity = new Vector2(m_rb.velocity.x, 0f);
            m_rb.isKinematic = false;
            m_rb.gravityScale = m_startingGrav;
            m_rb.velocity = new Vector2(m_rb.velocity.x, currentStat.jumpForce);
        }    
        private void HorizontalMove()
        {
            if (GamepadController.Ins.CanMoveLeft)
            {
                Move(Direction.Left);
            }
            else if (GamepadController.Ins.CanMoveRight)
            {
                Move(Direction.Right);
            }
        } 
        private void VerticalMove()
        {
            if (IsJumping) return;

            if (GamepadController.Ins.CanMoveUp)
            {
                Move(Direction.Up);
            }
            else if (GamepadController.Ins.CanMoveDown)
            {
                Move(Direction.Down);
            }
            GamepadController.Ins.CanFly = false;
        }
        public void WaterChecking()
        {
            if (obstacleChecker.IsOnLadder) return;

            if(obstacleChecker.IsOnDeepWater)
            {

            }   
            else if(obstacleChecker.IsOnWater && !IsJumping)
            {

            }    
        }    
        #region FSM
        void SayHi_Enter() { }
        void SayHi_Update() 
        {
            Helper.PlayAnim(m_anim, PlayerAnimState.SayHi.ToString());
        }
        void SayHi_Exit() { }
        void Walk_Enter() {
            ActiveColider(PlayerCollider.Default);
            m_curSpeed = stat.moveSpeed;
        }
        void Walk_Update() {
            HorizontalMove();
            if(!GamepadController.Ins.CanMoveLeft&&!GamepadController.Ins.CanMoveRight)
            {
                ChangeState(PlayerAnimState.Idle);
            }
            if(GamepadController.Ins.CanJump)
            {
                Jump();
                ChangeState(PlayerAnimState.Jump);
            }    
            Helper.PlayAnim(m_anim, PlayerAnimState.Walk.ToString());
        }
        void Walk_Exit() { }
        void Jump_Enter() {
            ActiveColider(PlayerCollider.Default);
        }
        void Jump_Update() {
            m_rb.isKinematic = false;
            if(m_rb.velocity.y<0 && !obstacleChecker.IsOnGround)
            {   
                ChangeState(PlayerAnimState.Onair);
            }
            HorizontalMove();
            Helper.PlayAnim(m_anim, PlayerAnimState.Jump.ToString());
        }
        void Jump_Exit() { }
        void Onair_Enter() {
    
            ActiveColider(PlayerCollider.Default);
            
        }
        void Onair_Update() {
            m_rb.gravityScale = m_startingGrav;
            if (obstacleChecker.IsOnGround)
            {
                ChangeState(PlayerAnimState.Land);
            }
            if(GamepadController.Ins.CanFly)
            {
                ChangeState(PlayerAnimState.Fly);
            }
            Helper.PlayAnim(m_anim, PlayerAnimState.Onair.ToString());
        }
        void Onair_Exit() { }
        void Land_Enter() {
            ActiveColider(PlayerCollider.Default);
            ChangeStateDelay(PlayerAnimState.Idle);
        }
        void Land_Update() {
            m_rb.velocity = Vector2.zero;
            Helper.PlayAnim(m_anim, PlayerAnimState.Land.ToString());
        }
        void Land_Exit() { }
        void Swim_Enter() { }
        void Swim_Update() {
            Helper.PlayAnim(m_anim, PlayerAnimState.Swim.ToString());
        }
        void Swim_Exit() { }
        void FireBullet_Enter() { }
        void FireBullet_Update() {
            Helper.PlayAnim(m_anim, PlayerAnimState.FireBullet.ToString());
        }
        void FireBullet_Exit() { }
        void Fly_Enter() {
            ActiveColider(PlayerCollider.Flying);
            ChangeStateDelay(PlayerAnimState.FlyOnAir);
        }
        void Fly_Update() {
            HorizontalMove();
            m_rb.velocity = new Vector2(m_rb.velocity.x,-currentStat.flyingSpeed);
            Helper.PlayAnim(m_anim, PlayerAnimState.Fly.ToString());
        }
        void Fly_Exit() { }
        void FlyOnAir_Enter() {
            ActiveColider(PlayerCollider.Flying);
        }
        void FlyOnAir_Update() {
            HorizontalMove();
            m_rb.velocity = new Vector2(m_rb.velocity.x, -currentStat.flyingSpeed);
            if(!GamepadController.Ins.CanFly)
            {
                ChangeState(PlayerAnimState.Onair);
            }
            if(obstacleChecker.IsOnGround)
            {
                ChangeState(PlayerAnimState.Land);
            }
            Helper.PlayAnim(m_anim, PlayerAnimState.FlyOnAir.ToString());
        }
        void FlyOnAir_Exit() { }
        void SwimOnDeep_Enter() { }
        void SwimOnDeep_Update() {
            Helper.PlayAnim(m_anim, PlayerAnimState.SwimOnDeep.ToString());
        }
        void SwimOnDeep_Exit() { }
        void OnLadder_Enter() {
            ActiveColider(PlayerCollider.Default);
            m_rb.velocity = Vector2.zero;

        }
        void OnLadder_Update() {
            VerticalMove();
            HorizontalMove();
            if(!GamepadController.Ins.CanMoveDown && !GamepadController.Ins.CanMoveUp)
            {
                m_rb.velocity = new Vector2(m_rb.velocity.x, 0f);
                ChangeState(PlayerAnimState.LadderIIdle);
            }
            if(!obstacleChecker.IsOnLadder)
            {
                ChangeState(PlayerAnimState.Onair);
            }
            GamepadController.Ins.CanFly = false;
            m_rb.gravityScale = 0;
            Helper.PlayAnim(m_anim, PlayerAnimState.OnLadder.ToString());
        }
        void OnLadder_Exit() { }
        void Dead_Enter() { }
        void Dead_Update() {
            Helper.PlayAnim(m_anim, PlayerAnimState.Dead.ToString());
        }
        void Dead_Exit() { }
        void Idle_Enter() {
            ActiveColider(PlayerCollider.Default);
        }
        void Idle_Update() {
            if (GamepadController.Ins.CanJump)
            {
                Jump();
                ChangeState(PlayerAnimState.Jump);
            }
            if(GamepadController.Ins.CanMoveLeft|| GamepadController.Ins.CanMoveRight)
            {
                ChangeState(PlayerAnimState.Walk);
            }    
            Helper.PlayAnim(m_anim, PlayerAnimState.Idle.ToString());
        }
        void Idle_Exit() { }
        void LadderIIdle_Enter() {
            m_rb.velocity = Vector2.zero;
            ActiveColider(PlayerCollider.Default);
            m_curSpeed = currentStat.ladderSpeed;
        }
        void LadderIIdle_Update() {
            if(GamepadController.Ins.CanMoveDown|| GamepadController.Ins.CanMoveUp)
            {
                ChangeState(PlayerAnimState.OnLadder);
            }
            if(!obstacleChecker.IsOnLadder)
            {
                ChangeState(PlayerAnimState.Onair);
            }
            GamepadController.Ins.CanFly = false;
            m_rb.gravityScale = 0;
            HorizontalMove();
            Helper.PlayAnim(m_anim, PlayerAnimState.LadderIIdle.ToString());
        }
        void LadderIIdle_Exit() { }
        void HammerAttack_Enter() { }
        void HammerAttack_Update() {
            Helper.PlayAnim(m_anim, PlayerAnimState.HammerAttack.ToString());
        }
        void HammerAttack_Exit() { }
        void GotHit_Enter() { }
        void GotHit_Update() {
            Helper.PlayAnim(m_anim, PlayerAnimState.GotHit.ToString());
        }
        void GotHit_Exit() { }

        #endregion
    }
}

