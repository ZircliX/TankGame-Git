using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/BulletType", order = 1)]

public class BulletType: ScriptableObject
{
    [Header("Infos")]
        public string bulletName;

    [Header("Defaults")] 
        public float bulletSpeed;
        public float bulletDamage;
        public float resetTime;
    
    [Header("Ability")]
        public int numOfBounce;
        public bool isDuplicable;

    [Header("Reference")] 
        public GameObject bulletPrefab;
}