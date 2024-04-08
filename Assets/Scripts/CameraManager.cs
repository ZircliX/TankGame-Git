using System;
using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 targetPos;
    public Transform[] enemyPos;
    public Transform playerPos;
    
    [SerializeField] private float cameraFollowSpeed;
    [SerializeField] private Transform _camera;
    
    private static event Action<float, float> Shake;

    public static void InvokeShake(float duration, float strenght)
    {
        Shake?.Invoke(duration, strenght);
    }

    private void OnEnable() => Shake += CameraShake;
    private void OnDisable() => Shake -= CameraShake;

    private void CameraShake(float duration, float strength)
    {
        _camera.DOComplete();
        _camera.DOShakePosition(duration, strength);
        _camera.DOShakeRotation(duration, strength);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position, GetCamPos(), cameraFollowSpeed * Time.deltaTime);
    }

    private Vector3 GetCamPos()
    {
        return new Vector3(0, 50, 0);
    }
}