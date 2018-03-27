using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool m_IsPlaying;
    // Use this for initialization
    private void Start()
    {
        CharacterData taMere = GameDatas.GetData<CharacterData>();
        Debug.Log(taMere.name);

        AudioManager.Instance.OnSoundStop += OnSoundStop;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            m_IsPlaying = !m_IsPlaying;
            if (m_IsPlaying)
            {
                AudioManager.Instance.PlaySound("SawingWood", 0.5f);
            }
            else
            {
                AudioManager.Instance.StopSound("SawingWood");
            }
        }
    }

    private void OnSoundStop(AudioSource source)
    {
        Debug.Log(source.name);
    }
}
