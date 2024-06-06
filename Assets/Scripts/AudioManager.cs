using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();

                if (instance == null)
                {
                    GameObject singleton = new GameObject(typeof(AudioManager).ToString());
                    instance = singleton.AddComponent<AudioManager>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return instance;
        }
    }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }
}
