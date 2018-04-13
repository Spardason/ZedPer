using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

/// <summary>
/// This class manages all sounds
/// TODO: Sounds Category
/// TODO: Multiple Play Functions, for queu, with different parameters
/// TODO: Volume thingies.  
/// /// </summary>
public class AudioManager : BaseManager
{
    public enum SoundCategory
    {
        Ambient,
        Music,
        VO,
        SFX
    }

    private List<AudioSource> m_PlayedSounds;
    private Dictionary<AudioSource, SoundCategory> m_PlayingSoundsByType;

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
        m_PlayingSoundsByType = new Dictionary<AudioSource, SoundCategory>();
    }

    private void Update()
    {
        // Iterate each playing sounds and shot a callback when the sound is done.
        AudioSource source;
        foreach (KeyValuePair<AudioSource, SoundCategory> keyValuePair in m_PlayingSoundsByType)
        {
            source = keyValuePair.Key;
            if (!source.isPlaying)
            {
                m_PlayingSoundsByType.Remove(source);
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
    public AudioSource PlaySound(string soundName, float volume, SoundCategory category)
    {
        foreach (AudioSource audioSource in m_PlayedSounds)
        {
            if (!audioSource.isPlaying && audioSource.name == soundName)
            {
                audioSource.loop = false;
                audioSource.volume = volume;
                audioSource.Play();

                AddSoundToTypeList(audioSource, category);

                return audioSource;
            }
        }

        GameObject audioObject = new GameObject();
        AudioSource source = audioObject.AddComponent<AudioSource>();
        source.clip = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Sounds/" + soundName + ".mp3");
        source.loop = false;
        source.volume = volume;

        audioObject.name = source.clip.name;
        audioObject.transform.parent = gameObject.transform;
        source.Play();

        AddSoundToTypeList(source, category);

        if (!m_PlayedSounds.Contains(source))
        {
            m_PlayedSounds.Add(source);
        }

        return source;
    }

    /// <summary>
    /// Play a sound and make it loop forever, you will have to stop it manualy.
    /// </summary>
    /// <param name="soundName"></param>
    /// <param name="volume"></param>
    /// <returns></returns>
    public AudioSource PlayLoopSound(string soundName, float volume, SoundCategory category)
    {
        AudioSource source = PlaySound(soundName, volume, category);
        source.loop = true;

        return source;
    }

    public AudioSource Play3DSound(string soundName, float volume, SoundCategory category, Transform parent)
    {
        AudioSource source = PlaySound(soundName, volume, category);
        source.transform.parent = parent;

        return source;
    }

    /// <summary> Stop a sound by name.
    /// <param name = "soundName"> The name of the sound. </param>
    /// </summary>
	public void StopSound(string soundName)
    {
        foreach (KeyValuePair<AudioSource, SoundCategory> keyValuePair in m_PlayingSoundsByType)
        {
            AudioSource source = keyValuePair.Key;
            if (source.isPlaying && source.name == soundName)
            {
                source.Stop();
                break;
            }
        }
    }
    /// <summary>
    /// Stop a sound by source.
    /// </summary>
    /// <param name="source"> The audio source of the sound. </param>
    public void StopSound(AudioSource source)
    {
        StopSound(source.name);
    }

    /// <summary>
    /// Stop ALL playing sounds.
    /// </summary>
    public void StopAllSounds()
    {
        foreach (KeyValuePair<AudioSource, SoundCategory> keyValuePair in m_PlayingSoundsByType)
        {
            keyValuePair.Key.Stop();
        }
    }

    /// <summary>
    /// Stop all sounds of a category.
    /// </summary>
    /// <param name="category"></param>
    public void StopAllCategorySounds(SoundCategory category)
    {
        foreach (KeyValuePair<AudioSource, SoundCategory> keyValuePair in m_PlayingSoundsByType)
        {
            if (keyValuePair.Value == category)
            {
                keyValuePair.Key.Stop();
            }
        }
    }

    /// <summary>
    /// Set the volume for ALL sounds.
    /// </summary>
    /// <param name="volume"></param>
    public void SetGlobalVolume(float volume)
    {
        foreach (KeyValuePair<AudioSource, SoundCategory> keyValuePair in m_PlayingSoundsByType)
        {
            keyValuePair.Key.volume = volume;
        }
    }

    /// <summary>
    /// Set the volume in all said category.
    /// </summary>
    /// <param name="volume"></param>
    /// <param name="category"></param>
    public void SetCategoryVolume(float volume, SoundCategory category)
    {
        foreach (KeyValuePair<AudioSource, SoundCategory> keyValuePair in m_PlayingSoundsByType)
        {
            if (keyValuePair.Value == category)
            {
                keyValuePair.Key.volume = volume;
            }
        }
    }

    // Local Utils
    private void AddSoundToTypeList(AudioSource source, SoundCategory type)
    {
        if (!m_PlayingSoundsByType.ContainsKey(source))
        {
            m_PlayingSoundsByType.Add(source, type);
        }
    }
}
