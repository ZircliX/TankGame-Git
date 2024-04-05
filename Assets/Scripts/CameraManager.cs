using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform targetPos;
    [SerializeField] private float cameraFollowSpeed;
    
    [SerializeField] private Transform _camera;
    public static CameraManager Instance;
    
    private void Awake() => Instance = this;

    public static void Shake(float duration, float strenght) => Instance.CameraShake(duration, strenght);
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
