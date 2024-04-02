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
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (health <= 0f)
        {
            Destroy(transform.root.gameObject);
        }
    }
}