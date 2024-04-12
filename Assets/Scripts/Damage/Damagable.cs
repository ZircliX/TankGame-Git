using UnityEngine;

[RequireComponent(typeof(HealthManager))]
public class Damagable : MonoBehaviour
{
    void TakeDamage(float damage)
    {
        gameObject.GetComponent<HealthManager>().UpdateHealth(damage);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 13) //Check Bullet
        {
            Destroy(other.gameObject);
            TakeDamage(other.gameObject.GetComponent<BulletManager>().bulletData.bulletDamage);    
        }
    }
}