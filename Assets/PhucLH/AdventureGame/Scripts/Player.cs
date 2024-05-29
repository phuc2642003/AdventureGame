using PhucLH.AdventureGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using System.Resources;

namespace PhucLH.AdventureGame
{
    public class Player : Actor
    {
        private StateMachine<PlayerAnimState> fsm;

        protected override void Awake()
        {
            base.Awake();
            fsm = StateMachine<PlayerAnimState>.Initialize(this);
            fsm.ChangeState(PlayerAnimState.Idle);
            FSM_MethodGen.Gen<PlayerAnimState>();
        }

        #region FSM

        #endregion
    }
}

