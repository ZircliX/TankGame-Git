using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        PlayMusic("Theme");
    }
    
    private static event Action<string> PlayerMusic;
    private static event Action<string> PlayerSFX;
    private static event Action<string> StoperMusic;
    
    public static void InvokeMusic(string name)
    {
        PlayerMusic?.Invoke(name);
    }

    public static void InvokeSFX(string name)
    {
        PlayerSFX?.Invoke(name);
    }
    
    public static void InvokeStopMusic(string name)
    {
        StoperMusic?.Invoke(name);
    }

    private void OnEnable()
    {
        PlayerMusic += PlayMusic;
        StoperMusic += StopMusic;
        PlayerSFX += PlaySFX;
    }

    private void OnDisable()
    {
        PlayerMusic -= PlayMusic;
        StoperMusic -= StopMusic;
        PlayerSFX -= PlaySFX;
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