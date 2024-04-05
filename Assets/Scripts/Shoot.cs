using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Tank")]
        public TankType currentTankData;
        public TankPrefabAccess tpa;
    
    [Header("Shoot")]
        private bool canShoot;
        [HideInInspector] public bool isShooting;
        public BulletType bulletType;
    
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && isShooting)
        {
            currentTankData.ShootBullet(bulletType, tpa.shootPoints);

            StartCoroutine(StaticResetBool.ResetBool(
                endValue => canShoot = endValue,
                bulletType.resetTime * currentTankData.shootTimer));
        }
    }
}