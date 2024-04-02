using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] internal BulletType bulletData;
    IDamagable isDamagable;

    private void OnCollisionEnter(Collision other)
    {
        isDamagable = other.gameObject.GetComponent<IDamagable>();
        
        if (other.gameObject.CompareTag("KillBullet"))
        {
            Destroy(gameObject);
        }
        
        if (isDamagable is not null)
            isDamagable.TakeDamage(bulletData.bulletDamage, transform.root.GetChild(0).GetComponent<TankPrefabAccess>().cameraPos);
    }
}
