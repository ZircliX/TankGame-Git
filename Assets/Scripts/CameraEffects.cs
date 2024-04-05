using DG.Tweening;
using System;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    public static CameraEffects Instance;
    
    private void Awake() => Instance = this;

    public static void Shake(float duration, float strenght) => Instance.CameraShake(duration, strenght);
    private void CameraShake(float duration, float strenght)
    {
        _camera.DOComplete();
        _camera.DOShakePosition(duration, strenght);
        _camera.DOShakeRotation(duration, strenght);
    }
}
