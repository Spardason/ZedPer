using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
		  CharacterData taMere = GameDatas.GetData<CharacterData>();
		  Debug.Log(taMere.Health);
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
