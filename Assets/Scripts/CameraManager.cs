using System;
using DG.Tweening;
using Google.Protobuf.WellKnownTypes;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform targetPos;
    [SerializeField] private float cameraFollowSpeed;
    [SerializeField] private Transform _camera;
    
    private static event Action<float, float> Shake;

    public static void InvokeShake(float duration, float strenght)
    {
        Shake?.Invoke(duration, strenght);
    }

    private void OnEnable() => Shake += CameraShake;
    private void OnDisable() => Shake -= CameraShake;

    private void CameraShake(float duration, float strenght)
    {
        _camera.DOComplete();
        _camera.DOShakePosition(duration, strenght);
        _camera.DOShakeRotation(duration, strenght);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position, targetPos.position, cameraFollowSpeed * Time.deltaTime);
    }
}