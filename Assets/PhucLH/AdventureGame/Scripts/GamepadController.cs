using PhucLH.AdventureGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class GamepadController : Singleton<GamepadController>
    {
        public float jumpHoldingTime;
        private bool m_canMoveLeft;
        private bool m_canMoveRight;
        private bool m_canMoveUp;
        private bool m_canMoveDown;
        private bool m_canJump;
        private bool m_isJumpHolding;
        private bool m_canFly;
        private bool m_canFire;
        private bool m_canAttack;

        private bool m_canCheckJumpHolding;
        private float m_curHoldingTime;

        public bool CanMoveLeft { get => m_canMoveLeft; set => m_canMoveLeft = value; }
        public bool CanMoveRight { get => m_canMoveRight; set => m_canMoveRight = value; }
        public bool CanMoveUp { get => m_canMoveUp; set => m_canMoveUp = value; }
        public bool CanMoveDown { get => m_canMoveDown; set => m_canMoveDown = value; }
        public bool CanJump { get => m_canJump; set => m_canJump = value; }
        public bool IsJumpHolding { get => m_isJumpHolding; set => m_isJumpHolding = value; }
        public bool CanFly { get => m_canFly; set => m_canFly = value; }
        public bool CanFire { get => m_canFire; set => m_canFire = value; }
        public bool CanAttack { get => m_canAttack; set => m_canAttack = value; }

        public bool IsStatic
        {
            get => !m_canMoveLeft && !m_canMoveRight && !m_canMoveUp && !m_canMoveDown
                && !m_canJump && !m_canFly && !m_isJumpHolding;
        }

        public override void Awake()
        {
            MakeSingleton(false);
        }

        private void Update()
        {
            if (!GameManager.Ins.setting.isOnMobile)
            {
                float hozCheck = Input.GetAxisRaw("Horizontal");
                float vertCheck = Input.GetAxisRaw("Vertical");
                m_canMoveLeft = hozCheck < 0 ? true : false;
                m_canMoveRight = hozCheck > 0 ? true : false;
                m_canMoveUp = vertCheck > 0 ? true : false;
                m_canMoveDown = vertCheck < 0 ? true : false;
                m_canJump = Input.GetKeyDown(KeyCode.Space);
                m_canFly = Input.GetKey(KeyCode.F);
                m_canFire = Input.GetKeyDown(KeyCode.C);
                m_canAttack = Input.GetKeyDown(KeyCode.V);

                if (m_canJump)
                {
                    m_isJumpHolding = false;
                    m_canCheckJumpHolding = true;
                    m_curHoldingTime = 0;
                }

                if (m_canCheckJumpHolding)
                {
                    m_curHoldingTime += Time.deltaTime;
                    if (m_curHoldingTime > jumpHoldingTime)
                    {
                        m_isJumpHolding = Input.GetKey(KeyCode.Space);
                    }
                }
            }
            else
            {

            }
        }
    }

}
