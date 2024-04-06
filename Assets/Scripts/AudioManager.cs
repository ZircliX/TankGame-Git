using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
             Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        musicSource.clip = s.clip;
        musicSource.Play();
    }
    
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        sfxSource.clip = s.clip;
        sfxSource.Play();
    }

    public void StopMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        
        musicSource.clip = s.clip;
        musicSource.Stop();
    }
}