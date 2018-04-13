using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZP.StateMachine;

public class Player : MonoBehaviour
{
    private enum States
    {
        Idle = 0,
        Walk = 1,
        Run = 2,
        // And so on
    }

    private bool m_IsPlaying;
    private States m_State;
    private StateMachine m_StateMachine;
    // Use this for initialization
    private void Start()
    {
        CharacterData taMere = GameData.GetData<CharacterData>();
        Debug.Log(taMere.name);

        AudioManager.Instance.OnSoundStop += OnSoundStop;

        m_StateMachine = new StateMachine();
        m_StateMachine.AddState((int)States.Idle,
        new Dictionary<State.CallbackType, State.StateCallBack>
        {
            {State.CallbackType.OnEnter, OnEnterIdle},
            {State.CallbackType.OnUpdate, OnUpdateIdle},
            {State.CallbackType.OnExit, OnExitIdle}
        });

        m_StateMachine.AddState((int)States.Walk,
        new Dictionary<State.CallbackType, State.StateCallBack>
        {
            {State.CallbackType.OnEnter, OnEnterWalk},
            {State.CallbackType.OnUpdate, OnUpdateWalk},
            {State.CallbackType.OnExit, OnExitWalk}
        });

        m_StateMachine.AddState((int)States.Run,
        new Dictionary<State.CallbackType, State.StateCallBack>
        {
            {State.CallbackType.OnEnter, OnEnterRun},
            {State.CallbackType.OnUpdate, OnUpdateRun},
            {State.CallbackType.OnExit, OnExitRun}
        });
        m_State = (int)States.Idle;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // m_IsPlaying = !m_IsPlaying;
            // if (m_IsPlaying)
            // {
            //     AudioManager.Instance.PlaySound("SawingWood", 0.5f);
            // }
            // else
            // {
            //     AudioManager.Instance.StopSound("SawingWood");
            // }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            m_State = (m_State++ >= States.Run ? States.Idle : m_State);
            m_StateMachine.SetState((int)m_State);
        }

        if (m_StateMachine != null)
        {
            m_StateMachine.Update();
        }
    }

#region SM Callbacks
    private void OnEnterIdle()
    {
        Debug.Log("OnEnterIdle");
    }

    private void OnUpdateIdle()
    {
        Debug.Log("OnUpdateIdle");
    }

    private void OnExitIdle()
    {
        Debug.Log("OnExitIdle");
    }

    private void OnEnterWalk()
    {
        Debug.Log("OnEnterWalk");
    }

    private void OnUpdateWalk()
    {
        Debug.Log("OnUpdateWalk");
    }

    private void OnExitWalk()
    {
        Debug.Log("OnExitWalk");
    }

    private void OnEnterRun()
    {
        Debug.Log("OnEnterRun");
    }

    private void OnUpdateRun()
    {
        Debug.Log("OnUpdateRun");
    }

    private void OnExitRun()
    {
        Debug.Log("OnExitRun");
    }
#endregion

    private void OnSoundStop(AudioSource source)
    {
        Debug.Log(source.name);
    }
}
