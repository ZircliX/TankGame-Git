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
                AudioManager.InvokeSFX("Loose");
                AudioManager.InvokeStopMusic("Theme");
                transform.root.gameObject.SetActive(false);
                break;
            case <= 0f:
                Destroy(transform.root.gameObject);
                AudioManager.InvokeSFX("Destroy");
                break;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 13) //Check Bullet
        {
            Destroy(other.gameObject);
            TakeDamage(other.gameObject.GetComponent<BulletManager>().bulletData.bulletDamage,
                other.transform.root.GetChild(0).GetComponent<TankPrefabAccess>().cameraPos);    
        }
    }
}