using UnityEngine;

[CreateAssetMenu(fileName = "Tank", menuName = "ScriptableObjects/TankType", order = 1)]

public class TankType : ScriptableObject
{
    [Header("Infos")]
        public string tankName;

    [Header("Defaults")] 
        public float moveSpeed;
        public float decelerateSpeed;
        public int tankRotateSpeed;
        
    [Header("Rotation")]
        public float rotationSpeed;

    [Header("References")]
        public GameObject tankPrefab;
        
    public void ShootBullet(BulletType bulletScript, Transform[] canonShoot)
    {
        foreach (Transform shootPoint in canonShoot)
        {
            //Calculates the direction
            Vector3 shootDirection = shootPoint.rotation * Vector3.forward;
        
            //Spawn the bullet
            GameObject spawnedBullet = Instantiate(
                bulletScript.bulletPrefab, shootPoint.position, shootPoint.rotation);
    
            //Add force to the bullet
            spawnedBullet.GetComponent<Rigidbody>().AddForce(
                shootDirection * bulletScript.bulletSpeed, ForceMode.Impulse);
        }
    }
}
