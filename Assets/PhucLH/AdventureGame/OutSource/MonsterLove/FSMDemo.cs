using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

namespace PhucLH.AdventureGame
{
    public class FSMDemo : MonoBehaviour
    {
        public enum StateDemo
        {
            State1,
            State2,
            State3
        }

        private StateMachine<StateDemo> m_fsm;

        private void Awake()
        {
            m_fsm = StateMachine<StateDemo>.Initialize(this);
            m_fsm.ChangeState(StateDemo.State1);
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_fsm.ChangeState(StateDemo.State2);
            }
        }
        #region FSM
        void State1_Enter()
        {
            Debug.Log("State 1 enter");
        }
        void State1_Update()
        {
            Debug.Log("State 1 update");
        }
        void State1_FixedUpdate()
        {
            Debug.Log("State 1 fixed update");
        }
        void State1_Exit()
        {
            Debug.Log("State 1 exit");
        }
        void State1_Finally()
        {
            Debug.Log("State 1 final");
        }

        void State2_Enter()
        {
            Debug.Log("State 2 enter");
        }
        void State2_Update()
        {
            Debug.Log("State 2 update");
        }
        void State2_FixedUpdate()
        {
            Debug.Log("State 2 fixed update");
        }
        void State2_Exit()
        {
            Debug.Log("State 2 exit");
        }
        void State2_Finally()
        {
            Debug.Log("State 2 final");
        }
        #endregion
    }
}

