using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    private float m_Health;
    [SerializeField]
    private float m_Attack;
    [SerializeField]
    private float m_Defense;
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_Luck;

    public float Health
    {
        get { return m_Health; }
    }
}
