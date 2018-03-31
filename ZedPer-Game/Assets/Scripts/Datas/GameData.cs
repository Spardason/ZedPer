using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

public class GameData : ScriptableObject
{
	private const string GAMEDATAS_PATH = "Assets/ScriptableObjects/Datas/GameData.asset";

    [SerializeField]
    private List<ScriptableObject> m_Datas = new List<ScriptableObject>();

	private static GameData m_Instance;

	public static GameData GetInstance()
	{
		if (!m_Instance)
		{
			m_Instance = AssetDatabase.LoadAssetAtPath<GameData>(GAMEDATAS_PATH);
		}

		return m_Instance;
	}

    public static T GetData<T>() where T : ScriptableObject
    {
        FieldInfo[] infos = typeof(GameData).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        List<ScriptableObject> datas = new List<ScriptableObject>();
        for (int i = 0; i < infos.Length; i++)
        {
            if (infos[i].FieldType == typeof(List<ScriptableObject>))
            {
                datas = infos[i].GetValue(GetInstance()) as List<ScriptableObject>;
            }
        }

        foreach (ScriptableObject data in datas)
        {
            if (data.GetType() == typeof(T))
            {
                return data as T;
            }
        }

        return null;
    }
}
