using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZP.StateMachine
{
    /// <summary>
    ///  Use this when you need multiple states in a script, for example a character with multiple actions.
    ///  This has nothing to do with animation state machine.
    ///  Add state using the AddState function.
    /// </summary>
    public class StateMachine
    {
        private Dictionary<int, State> m_States = new Dictionary<int, State>();
        private State m_CurrentState;
        private State m_PreviousState;

        public State CurrentState
        {
            get { return m_CurrentState; }
        }

        public State PreviousState
        {
            get { return m_PreviousState; }
        }

        public void Update()
        {
            if (CanCallCallback(State.CallbackType.OnUpdate))
            {
                m_CurrentState.m_CallbacksByType[State.CallbackType.OnUpdate]();
            }
        }

        public void FixedUpdate()
        {
            if (CanCallCallback(State.CallbackType.OnFixedUpdate))
            {
                m_CurrentState.m_CallbacksByType[State.CallbackType.OnFixedUpdate]();
            }
        }

        public void LateUpdate()
        {
            if (CanCallCallback(State.CallbackType.OnLateUpdate))
            {
                m_CurrentState.m_CallbacksByType[State.CallbackType.OnLateUpdate]();
            }
        }

        /// <summary>
        ///  Set the current of the state machine, calling the exit on the old one and the enter of the new one.
        /// </summary>
        /// <param name="stateId"> "The wanted state Id"</param>
        public void SetState(int stateId)
        {
            if (!m_States.ContainsKey(stateId))
            {
                Debug.LogError("There is no " + stateId + " in this StateMachine, aborting.");
                return;
            }

            m_PreviousState = m_CurrentState;

            if (CanCallCallback(State.CallbackType.OnExit))
            {
                m_CurrentState.m_CallbacksByType[State.CallbackType.OnExit]();
            }

            m_CurrentState = m_States[stateId];
            m_CurrentState.m_CallbacksByType[State.CallbackType.OnEnter]();
        }

        /// <summary>
        /// This adds a new state at the state machine, 
        /// </summary>
        /// <param name="stateId"> "The 'Id' of the State."</param>
        /// <param name="callbacks"> "Callbacks you want for this state."</param>
        public void AddState(int stateId, Dictionary<State.CallbackType, State.StateCallBack> callbacks)
        {
            if (m_States.ContainsKey(stateId))
            {
                Debug.LogError("State " + stateId + " already exist inthis StateMachine, NOT adding.");
                return;
            }
            m_States.Add(stateId, new State(callbacks));
        }

        /// <summary>
        ///  Is Tornadable !?!?!?!?!?!? O_o 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool CanCallCallback(State.CallbackType type)
        {
            return m_CurrentState != null && m_CurrentState.m_CallbacksByType.ContainsKey(type);
        }
    }
}
