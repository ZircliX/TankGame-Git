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
            GAME_MANAGER.Instance.UpdateGameState(GAME_MANAGER.GameState.PlayerDead);
            gameObject.GetComponent<TankScript>().cameraTargetPosition =
                tankBullet.GetComponent<EnemyTankAccess>().cameraPos.position;
        }
        else if (health <= 0f)
        {
            Destroy(transform.root.gameObject);
        }
    }
}