using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZP.StateMachine
{
    /// <summary>
    /// This is a StateMachine single state, it has 5 states
    /// 					- OnEnter
    /// 					- OnUpdate
    ///                     - OnFixedUpdate
    ///                     - OnLateUpdate
    /// 					- OnExit
    /// </summary>
    public class State
    {
        public enum CallbackType
        {
            OnEnter = 0,
            OnUpdate = 1,
            OnFixedUpdate = 2,
            OnLateUpdate = 3,
            OnExit = 4
        }

        public delegate void StateCallBack();
        public Dictionary<CallbackType, StateCallBack> m_CallbacksByType = new Dictionary<CallbackType, StateCallBack>();

        public State(Dictionary<CallbackType, StateCallBack> callbacks)
        {
            m_CallbacksByType = callbacks;
        }
    }
}
