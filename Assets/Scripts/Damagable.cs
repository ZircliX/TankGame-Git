using UnityEngine;

public class Damagable : MonoBehaviour
{
    [Header("Properties")]
        [SerializeField] internal float maxHealth;
        private float health;
        
    void Start()
    {
        health = maxHealth;
    }


    void TakeDamage(float damage, Transform enemyCamPos)
    {
        health -= damage;

        switch (health)
        {
            case <= 0f when gameObject.CompareTag("Player"):
                gameObject.GetComponent<TankScript>().cm.targetPos = enemyCamPos;
                transform.root.gameObject.SetActive(false);
                break;
            case <= 0f:
                Destroy(transform.root.gameObject);
                break;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        TakeDamage(other.gameObject.GetComponent<BulletManager>().bulletData.bulletDamage,
            other.transform.root.GetChild(0).GetComponent<TankPrefabAccess>().cameraPos);
    }
}