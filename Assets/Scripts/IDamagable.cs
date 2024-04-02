using UnityEngine;

public class IDamagable : MonoBehaviour
{
    [Header("Properties")]
        [SerializeField] internal float maxHealth;
        private float health;
        
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage, GameObject tankBullet)
    {
        health -= damage;

        if (health <= 0f && gameObject.CompareTag("Player"))
        {
            //gameObject.GetComponent<TankScript>().cameraTargetPosition =
                //tankBullet.GetComponent<TankPrefabAccess>().cameraPos.position;
        }
        else if (health <= 0f)
        {
            Destroy(transform.root.gameObject);
        }
    }
}