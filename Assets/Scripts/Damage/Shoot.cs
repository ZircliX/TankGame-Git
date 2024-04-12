using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Tank")]
        [HideInInspector] public TankType currentTankData;
        [HideInInspector] public TankPrefabAccess tpa;
    
    [Header("Shoot")]
        private bool canShoot;
        [HideInInspector] public bool isShooting;
        public BulletType bulletType;

    void Start()
    {
        canShoot = true;
        isShooting = false;
    }

    void Update()
    {
        if (canShoot && isShooting && GameManager.Instance.state != GameManager.GameState.GamePause)
        {
            currentTankData.ShootBullet(bulletType, tpa.shootPoints);

            StartCoroutine(StaticResetBool.ResetBool(
                endValue => canShoot = endValue,
                bulletType.resetTime * currentTankData.shootTimer));
        }
    }
}