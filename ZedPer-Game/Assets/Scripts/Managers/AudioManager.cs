using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class AudioManager : BaseManager
{
    private List<AudioSource> m_PlayedSounds;
    private List<AudioSource> m_PlayingSounds;

	public delegate void StopSoundCallback(AudioSource audioSource);
	public StopSoundCallback OnSoundStop;

    public static AudioManager Instance
    {
        get
        {
            if (!m_Instance)
            {
                m_Instance = FindObjectOfType<AudioManager>();
            }

            return m_Instance as AudioManager;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        m_PlayedSounds = new List<AudioSource>();
        m_PlayingSounds = new List<AudioSource>();
    }

    private void Update()
    {
		// Iterate each playing sounds and shot a callback when the sound is done.  PS: Only works for non Looping sounds. 
        AudioSource source;
        for (int i = m_PlayingSounds.Count - 1; i > 0; i--)
        {
            source = m_PlayingSounds[i];
			if (!source.isPlaying)
			{
                m_PlayingSounds.Remove(source);
				if (OnSoundStop != null)
				{
					OnSoundStop(source);
				}
			}
        }
    }

    /// <summary> Play a single sound by it's name, only .mp3.
    /// <para> This does not loop, it only play once. </para>
    /// <param name = "soundName"> The name of the sound. </param>
    /// <param name = "volume"> Volume of the sound. </param>
    /// </summary>
    public AudioSource PlaySound(string soundName, float volume)
    {
        GameObject audioObject = new GameObject();
        AudioSource source = audioObject.AddComponent<AudioSource>();
        source.clip = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Sounds/" + soundName + ".mp3");
        source.loop = false;
        source.volume = volume;

        audioObject.name = source.clip.name;
        audioObject.transform.parent = gameObject.transform;
        source.Play();

		if (!m_PlayingSounds.Contains(source))
		{
			m_PlayingSounds.Add(source);
		}

        if (!m_PlayedSounds.Contains(source))
        {
            m_PlayedSounds.Add(source);
        }

        return source;
    }

    /// <summary> Stop a sound by name.
    /// <param name = "soundName"> The name of the sound. </param>
    /// </summary>
	public void StopSound(string soundName)
	{
        foreach (AudioSource source in m_PlayingSounds)
        {
            if (source.isPlaying && source.name == soundName)
            {
                source.Stop();
                break;
            }
        }
	}
}
