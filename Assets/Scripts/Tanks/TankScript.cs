using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class TankScript : MonoBehaviour
{
    #region Variable Declaration
    
    [Header("Scriptable Objects")]
        [SerializeField] internal TankType[] tankScriptables;
    
    [Header("Movement")] 
        private Vector2 inputDirection;
        private Vector3 moveDirection;
        private bool isMoving;
        [Space]
        private float angleTank;

    [Header("Rotation")]
        private Vector2 aimRotation;
        private float angleRotation;
        private bool isAiming;

    [Header("References")]
        [SerializeField] internal Rigidbody rb;
        [SerializeField] internal Shoot shoot;
        [SerializeField] internal CameraManager cm;
        [SerializeField] internal GameObject[] tanks;

    [Header("Tank Parts")]
        [SerializeField] private float tankSwitchTimer;
        private bool canSwitch;
        
        private int currentTankIndex;
        private TankPrefabAccess TPA;
        private TankType currentTankData;
        private GameObject currentTankObj;
        
    #endregion

    #region Unity Events => Auto Called Func

    void Awake()
    {
        SetupTank();
    }

    private void Start()
    {
        isMoving = false;
        isAiming = false;

        canSwitch = true;
        currentTankIndex = 0;
    }

    void Update()
    {
        RotateTank();
        RotateCanon();
    }

    void FixedUpdate()
    {
        rb.AddForce(moveDirection * currentTankData.moveSpeed, ForceMode.Force);

        rb.velocity /= isMoving ? 1f : currentTankData.decelerateSpeed;
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
        aimRotation = context.ReadValue<Vector2>();

        //Calculate the angle if Vector > dead zone
        if (aimRotation.sqrMagnitude > Vector2.one.sqrMagnitude / 5f)
        {
            angleRotation = Mathf.RoundToInt(Mathf.Atan2(aimRotation.y, aimRotation.x) * Mathf.Rad2Deg);
        }

        SwitchContextPhase(context, endValue => isAiming = endValue);
    }

    public void HandleShoot(InputAction.CallbackContext context)
    {
        SwitchContextPhase(context, endValue => shoot.isShooting = endValue);
    }

    public void HandleTankChange(InputAction.CallbackContext context)
    {
        if (!canSwitch)
            return;
        
        tanks[currentTankIndex].SetActive(false);
        
        //Update the tank index
        currentTankIndex += 1;
        if (currentTankIndex == 2)
            currentTankIndex = 0;
        
        //Setup the new one
        SetupTank();
    }
    
    #endregion

    #region Self Func

    void RotateTank()
    {
        if (isMoving)
        {
            angleTank = Mathf.Atan2(inputDirection.x, -inputDirection.y) * Mathf.Rad2Deg;

            //Rotate tank to move direction
            TPA.tankBase.transform.rotation = Quaternion.Slerp(TPA.tankBase.transform.rotation, 
                Quaternion.Euler(0, angleTank, 0), currentTankData.tankRotateSpeed * Time.deltaTime);
        }
    }
    
    void RotateCanon()
    {
        if (isAiming)
        {
            //Rotate Canon to aim direction
            TPA.tankTower.transform.rotation = Quaternion.RotateTowards(
                TPA.tankTower.transform.rotation, Quaternion.Euler(0, -angleRotation + 90f, 0),
                Time.deltaTime * currentTankData.rotationSpeed);
        }
    }
    
    void SetupTank()
    {
        currentTankData = tankScriptables[currentTankIndex];
        currentTankObj = tanks[currentTankIndex];
        currentTankObj.SetActive(true);

        TPA = currentTankObj.GetComponent<TankPrefabAccess>();

        shoot.currentTankData = currentTankData;
        shoot.tpa = TPA;
        
        //AudioManager.Instance.PlaySFX("Go");
        
        StartCoroutine(StaticResetBool.ResetBool(endValue => canSwitch = endValue, tankSwitchTimer));
    }
    
    #endregion
}