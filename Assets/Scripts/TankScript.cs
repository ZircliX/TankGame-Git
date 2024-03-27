using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class TankScript : MonoBehaviour
{
    #region Variable Declaration
    
    [Header("Scriptable Objects")]
        [SerializeField] internal TankType tankScriptable;
        [SerializeField] internal BulletType bulletScriptable;
    
    [Header("Movement")] 
        private Vector2 inputDirection;
        private Vector3 moveDirection;
        private bool isMoving;

    [Header("Shoot")]
        private bool canShoot;
        private bool isShooting;

    [Header("Rotation")]
        private Vector2 aimRotation;
        private float angleRotation;
        private bool isAiming;

    [Header("Camera")]
        [SerializeField] internal new Transform camera;
        [SerializeField] internal float cameraFollowSpeed;
        private Camera cam;

    [Header("References")]
        [SerializeField] internal Rigidbody rb;

    [Header("Tank Parts")] 
        private GameObject tank;
        
    #endregion

    #region Unity Events => Auto Called Func

    void Awake()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        tank = Instantiate(tankScriptable.tankPrefab, transform.position, Quaternion.identity);
        tank.transform.parent = transform;
    }

    void Update()
    {
        CameraMovement();
        RotateTank();
        RotateCanon();
        Shoot();
    }

    void FixedUpdate()
    {
        rb.AddForce(moveDirection * tankScriptable.moveSpeed, ForceMode.Force);

        rb.velocity /= isMoving ? 1f : tankScriptable.decelerateSpeed;
    }

    #endregion
    
    #region Unity Events => Input System

    bool SwitchContextPhase(InputAction.CallbackContext context, bool isAction)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                isAction = true;
                break;

            case InputActionPhase.Performed:
                isAction = context.interaction is not SlowTapInteraction;
                break;

            case InputActionPhase.Canceled:
                isAction = false;
                break;
        }

        return isAction;
    }

    public void HandleMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();

        moveDirection = (Vector3.forward * inputDirection.x + Vector3.right * inputDirection.y).normalized;

        isMoving = SwitchContextPhase(context, isMoving);
    }

    public void HandleRotate(InputAction.CallbackContext context)
    {
        //Get Mouse position on screen
        aimRotation = context.ReadValue<Vector2>();

        //Calculate the angle if Vector > dead zone
        if (aimRotation.magnitude > Vector2.one.magnitude / 8f)
        {
            angleRotation = Mathf.RoundToInt(Mathf.Atan2(aimRotation.y, aimRotation.x) * Mathf.Rad2Deg);
        }

        isAiming = SwitchContextPhase(context, isAiming);
    }

    public void HandleShoot(InputAction.CallbackContext context)
    {
        isShooting = SwitchContextPhase(context, isShooting);
    }
    
    #endregion

    #region Self Func
    
    void CameraMovement()
    {
        camera.position = Vector3.Lerp(
            camera.position, tank.transform.GetChild(5).position, cameraFollowSpeed * Time.deltaTime);
    }
    
    void RotateTank()
    {
        if (isMoving)
        {
            float angle = Mathf.Atan2(inputDirection.x, -inputDirection.y) * Mathf.Rad2Deg;
            Vector3 rotationTarget = new Vector3(0f, angle, 0f);

            //Rotate tank to move direction
            tank.transform.rotation = Quaternion.Slerp(tank.transform.rotation, 
                Quaternion.Euler(rotationTarget), tankScriptable.tankRotateSpeed * Time.deltaTime);
        }
    }
    
    void RotateCanon()
    {
        if (isAiming)
        {
            //Rotate Canon to aim direction
            tank.transform.GetChild(1).rotation = Quaternion.RotateTowards(
                tank.transform.GetChild(1).rotation, Quaternion.Euler(0, -angleRotation + 90f, 0),
                Time.deltaTime * tankScriptable.rotationSpeed);
        }
    }

    void Shoot()
    {
        if (canShoot && isShooting)
        {
            tankScriptable.ShootBullet(bulletScriptable, tank.transform.GetChild(4));

            //rb.AddForce(-shootDirection * 2f, ForceMode.Impulse);

            StartCoroutine(ResetShoot(bulletScriptable.resetTime));
        }
    }

    private IEnumerator ResetShoot(float shootDelay)
    {
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
    
    #endregion
}