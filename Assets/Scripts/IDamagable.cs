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

    public void TakeDamage(float damage, Transform enemyCamPos)
    {
        health -= damage;

        if (health <= 0f && gameObject.layer == 9)
        {
            gameObject.GetComponent<TankScript>().cameraTargetPosition = enemyCamPos;
        }
        else if (health <= 0f)
        {
            Destroy(transform.root.gameObject);
        }
    }
}