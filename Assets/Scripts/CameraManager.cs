using System;
using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float cameraFollowSpeed;
    [SerializeField] private Transform _camera;
    
    private static event Action<float, float> Shake;

    public static void InvokeShake(float duration, float strength)
    {
        Shake?.Invoke(duration, strength);
    }

    private void Start()
    {
        Shake += CameraShake;
    }

    private void CameraShake(float duration, float strength)
    {
        _camera.DOComplete();
        _camera.DOShakePosition(duration, strength);
        _camera.DOShakeRotation(duration, strength);
    }
}