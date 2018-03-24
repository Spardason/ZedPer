﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class ApplicationManagers : MonoBehaviour
{
    [SerializeField]
    private List<BaseManager> m_Managers = new List<BaseManager>();

    private static ApplicationManagers m_Instance;

    private void Awake()
    {
        foreach (BaseManager manager in m_Managers)
        {
            if (!manager.Exist)
            {
                GameObject.Instantiate(manager.gameObject).transform.parent = gameObject.transform;
            }
        }
    }

    public static ApplicationManagers Instance
    {
        get
        {
            if (!m_Instance)
            {
                m_Instance = FindObjectOfType<ApplicationManagers>();
            }

            return m_Instance;
        }
    }

    public static T GetManager<T>() where T : BaseManager
    {
        FieldInfo[] infos = typeof(ApplicationManagers).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        List<BaseManager> datas = new List<BaseManager>();
        for (int i = 0; i < infos.Length; i++)
        {
            if (infos[i].FieldType == typeof(List<BaseManager>))
            {
                datas = infos[i].GetValue(Instance) as List<BaseManager>;
            }
        }

        foreach (BaseManager manager in datas)
        {
            if (manager.GetType() == typeof(T))
            {
                return manager as T;
            }
        }

        return null;
    }
}
