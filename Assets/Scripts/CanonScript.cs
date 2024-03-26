using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanonScript : MonoBehaviour
{
    [Header("Shoot")]
        [SerializeField] internal BulletUnit bulletType;
        [SerializeField] internal Transform bulletShootPos;
        private bool canShoot = true;
    
    [Header("Rotation")]
        private Vector2 aimRotation;
        private float angleRotation;
    
    [Header("References")]
        [SerializeField] internal Rigidbody rb;
        [SerializeField] internal Transform pivotPoint;
        private Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        RotateCanon();
    }

    public void Shoot()
    {
        if (canShoot)
        {
            //Calculates the direction
            Vector3 shootDirection = pivotPoint.rotation * Vector3.forward;
            bulletType.ShootBullet(shootDirection, bulletShootPos);

            rb.AddForce(-shootDirection * 10f, ForceMode.Impulse);

            StartCoroutine(ResetShoot(bulletType.shootDelay));
        }
    }
    
    private IEnumerator ResetShoot(float shootDelay)
    {
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }

    public void HandleRotation(InputAction.CallbackContext context)
    {
        //Get Mouse position on screen
        //mousePos = Input.mousePosition - cam.WorldToScreenPoint(transform.position);
        aimRotation = context.ReadValue<Vector2>();
        
        //Calculate the angle 
        angleRotation = Mathf.Atan2(aimRotation.y, aimRotation.x) * Mathf.Rad2Deg;
    }

    private void RotateCanon()
    {
        //Rotate Canon to aim direction
        pivotPoint.rotation = Quaternion.AngleAxis(-angleRotation + 90f, Vector3.up);
    }
}