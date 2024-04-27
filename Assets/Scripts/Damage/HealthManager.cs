using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth;
    [HideInInspector] public float health;

    private void Start()
    {
        health = maxHealth;
    }

    public void UpdateHealth(float damage)
    {
        health -= damage;
        
        switch (health)
        {
            case <= 0f when gameObject.CompareTag("Player"):
                AudioManager.Instance.PlaySFX("Loose");
                AudioManager.Instance.StopMusic("Theme");
                
                transform.root.gameObject.SetActive(false);
                GameManager.Instance.SwitchState(10);
                break;
            case <= 0f:
                AudioManager.Instance.PlaySFX("Destroy");
                GameManagerEnemies.InvokeEnemyKilled();
                
                Destroy(transform.gameObject);
                break;
        }
    }
}