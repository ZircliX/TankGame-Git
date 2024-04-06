using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [Header("Audio Variables")] 
        public AudioMixer[] audioMixers;
        public Slider[] audioSliders;

    [Header("Controls Variables")]
        public Slider[] controlSliders;
        public string[] controlNames;
        public float[] controlDefaultValues;
    
        private float currentRate;
        private int currentResIndex;

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            //Audio
            audioSliders[i].value = PlayerPrefs.GetFloat("Volume" + i, 0f);
            audioMixers[i].SetFloat("MasterVolume",audioSliders[i].value);
            
            //Controls
            controlSliders[i].value = PlayerPrefs.GetFloat(controlNames[i], controlDefaultValues[i]);
        }
    }

    public void SetVolume(int index)
    {
        //Set the game's global volume by the player's chosen amount
        audioMixers[index].SetFloat("MasterVolume", audioSliders[index].value);
        PlayerPrefs.SetFloat("Volume" + index, audioSliders[index].value);
    }

    public void UpdateControls(int i)
    {
        PlayerPrefs.SetFloat(controlNames[i], controlSliders[i].value);
    }

    public void ResetOptions(int op)
    {
        if (op == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                controlSliders[i].value = controlDefaultValues[i];
                PlayerPrefs.SetFloat(controlNames[i], controlSliders[i].value);
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                audioSliders[i].value = 0f;
                audioMixers[i].SetFloat("MasterVolume",audioSliders[i].value);
            }
        }
    }
}