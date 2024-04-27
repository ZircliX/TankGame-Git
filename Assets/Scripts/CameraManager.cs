using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    private float shakeTime;
    
    private static event Action<float, float> Shake;

    public static void InvokeShake(float duration, float strength)
    {
        Shake?.Invoke(duration, strength);
    }

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        Shake += CameraShake;
    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            if (shakeTime <= 0f)
            {
                CinemachineBasicMultiChannelPerlin multiChannelPerlin =
                    _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                multiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }

    private void CameraShake(float duration, float strength)
    {
        //Shake worked before using Cinemachine
        
        // _camera.DOComplete();
        // _camera.DOShakePosition(duration, strength);
        // _camera.DOShakeRotation(duration, strength);
        
        /* Only works when the level 1 is loaded for the first time for some reasons...
        try
        {
            CinemachineBasicMultiChannelPerlin multiChannelPerlin =
                _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            multiChannelPerlin.m_AmplitudeGain = strength;
            shakeTime = duration;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }*/
    }
}