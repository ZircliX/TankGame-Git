using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class TankScript : MonoBehaviour
{
    #region Variable Declaration
    
    [Header("Scriptable Objects")]
        [SerializeField] internal TankType[] tankScriptables;
        [SerializeField] internal BulletType bulletScriptable;
    
    [Header("Movement")] 
        private Vector2 inputDirection;
        private Vector3 moveDirection;
        private bool isMoving;
        [Space]
        private float angleTank;

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

    [Header("References")]
        [SerializeField] internal Rigidbody rb;

    [Header("Tank Parts")]
        [SerializeField] private float tankSwitchTimer;
        private bool canSwitch;
        private TankPrefabAccess TPA;
        private int currentTankIndex;
        private TankType currentTank;
        private GameObject currentTankObj;
        
    #endregion

    #region Unity Events => Auto Called Func

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SetupTank();
    }

    private void Start()
    {
        isMoving = false;
        canShoot = true;
        isShooting = false;
        isAiming = false;

        canSwitch = true;
        currentTankIndex = 0;
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
        rb.AddForce(moveDirection * currentTank.moveSpeed, ForceMode.Force);

        rb.velocity /= isMoving ? 1f : currentTank.decelerateSpeed;
    }

    #endregion
    
    #region Unity Events => Input System

    void SwitchContextPhase(InputAction.CallbackContext context, Action<bool> myBool)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                myBool(true);
                break;

            case InputActionPhase.Performed:
                myBool(context.interaction is not SlowTapInteraction);
                break;

            case InputActionPhase.Canceled:
                myBool(false);
                break;
        }
    }

    public void HandleMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();

        moveDirection = (Vector3.forward * inputDirection.x + Vector3.right * inputDirection.y).normalized;

        SwitchContextPhase(context, endValue => isMoving = endValue);
    }

    public void HandleRotate(InputAction.CallbackContext context)
    {
        //Get Mouse position on screen
        aimRotation = context.ReadValue<Vector2>();

        //Calculate the angle if Vector > dead zone
        if (aimRotation.magnitude > Vector2.one.magnitude / 6f)
        {
            angleRotation = Mathf.RoundToInt(Mathf.Atan2(aimRotation.y, aimRotation.x) * Mathf.Rad2Deg);
        }

        SwitchContextPhase(context, endValue => isAiming = endValue);
    }

    public void HandleShoot(InputAction.CallbackContext context)
    {
        SwitchContextPhase(context, endValue => isShooting = endValue);
    }

    public void HandleTankChange(InputAction.CallbackContext context)
    {
        if (!canSwitch)
            return;
        
        //Update the tank index
        currentTankIndex += 1;
        if (currentTankIndex == tankScriptables.Length)
            currentTankIndex = 0;
        
        //Delete the old one
        Destroy(currentTankObj);
        
        //Setup the new one
        SetupTank();
    }
    
    #endregion

    #region Self Func
    
    void CameraMovement()
    {
        camera.position = Vector3.Lerp(
            camera.position, TPA.cameraPos.position, cameraFollowSpeed * Time.deltaTime);
    }
    
    void RotateTank()
    {
        if (isMoving)
        {
            angleTank = Mathf.Atan2(inputDirection.x, -inputDirection.y) * Mathf.Rad2Deg;

            //Rotate tank to move direction
            TPA.tankBase.transform.rotation = Quaternion.Slerp(TPA.tankBase.transform.rotation, 
                Quaternion.Euler(0, angleTank, 0), currentTank.tankRotateSpeed * Time.deltaTime);
        }
    }
    
    void RotateCanon()
    {
        if (isAiming)
        {
            //Rotate Canon to aim direction
            TPA.tankTower.transform.rotation = Quaternion.RotateTowards(
                TPA.tankTower.transform.rotation, Quaternion.Euler(0, -angleRotation + 90f, 0),
                Time.deltaTime * currentTank.rotationSpeed);
        }
    }

    void Shoot()
    {
        if (canShoot && isShooting)
        {
            currentTank.ShootBullet(bulletScriptable, TPA.shootPoints);

            //rb.AddForce(-shootDirection * 2f, ForceMode.Impulse);

            StartCoroutine(ResetBool(
                endValue => canShoot = endValue,
                bulletScriptable.resetTime * currentTank.shootTimer));
        }
    }

    private static IEnumerator ResetBool(System.Action<bool> myBool, float timeDelay)
    {
        myBool(false);
        yield return new WaitForSeconds(timeDelay);
        myBool(true);
    }
    
    void SetupTank()
    {
        currentTank = tankScriptables[currentTankIndex];
        
        currentTankObj = Instantiate(currentTank.tankPrefab, transform.position, transform.rotation);
        currentTankObj.transform.parent = transform;

        TPA = currentTankObj.GetComponent<TankPrefabAccess>();

        TPA.tankTower.transform.rotation = Quaternion.Euler(0, -angleRotation + 90f, 0);
        TPA.tankBase.transform.rotation = Quaternion.Euler(0, angleTank, 0);
        
        StartCoroutine(ResetBool(endValue => canSwitch = endValue, tankSwitchTimer));
    }
    
    #endregion
}