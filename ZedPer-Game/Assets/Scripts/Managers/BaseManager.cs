using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    protected static BaseManager m_Instance;

	public bool Exist
	{
		get 
		{
			return m_Instance != null;
		}
	}

    protected virtual void Awake()
    {

    }
}
