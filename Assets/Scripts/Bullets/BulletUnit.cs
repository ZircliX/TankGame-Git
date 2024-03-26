using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/BulletType", order = 1)]

public class BulletUnit : ScriptableObject
{
    [Header("Infos")]
        [SerializeField] protected string bulletName;

    [Header("Attributes")] 
        [SerializeField] protected float bulletDamage;
        [SerializeField] protected float bulletSpeed;
        [SerializeField] protected int numberOfBullet;
        [Space] 
        public float shootDelay;
    
    [Header("References")]
        [SerializeField] protected GameObject bulletPrefab;
        
    public void ShootBullet(Vector3 shootDirection, Transform spawnPoint)
    {
        //Spawn the bullet
        GameObject spawnedBullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        
        //Add force to the bullet
        spawnedBullet.GetComponent<Rigidbody>().AddForce(shootDirection * bulletSpeed, ForceMode.Impulse);
    }
}