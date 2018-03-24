using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    protected static BaseManager m_Instance;

    public static BaseManager Instance
    {
        get
        {
            if (!m_Instance)
            {
                m_Instance = FindObjectOfType<BaseManager>();
            }

            return m_Instance;
        }
    }

	public bool Exist
	{
		get 
		{
			return Instance != null;
		}
	}

    protected virtual void Awake()
    {

    }
}
