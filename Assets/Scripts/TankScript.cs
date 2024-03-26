using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class TankScript : MonoBehaviour
{
    [Header("Movement")] [SerializeField] internal float moveSpeed = 50f;
    [SerializeField] internal float decelerateSpeed = 1.01f;

    [Space] private Vector2 inputDirection;
    private bool isMoving = false;
    [SerializeField] internal float tankRotationSpeed;

    [Header("Input System")] private PlayerControls controls;

    [Header("Shoot")] [SerializeField] internal Transform shootPosition;
    [SerializeField] internal BulletUnit bulletType;
    private bool canShoot = true;
    private bool isShooting = false;

    [Header("Rotation")] [SerializeField] internal float rotationSpeed;
    private Vector2 aimRotation;
    private float angleRotation;
    private float lastAngle;
    [SerializeField] internal Transform tower;
    private bool isAiming = false;

    [Header("Camera")] [SerializeField] internal float cameraSpeedFollow = 5f;

    [Header("References")] [SerializeField]
    internal Rigidbody rb;

    [SerializeField] internal new Transform camera;
    [SerializeField] internal Transform tankCameraPosition;
    [SerializeField] internal Transform tank;

    private Vector3 moveDirection;
    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);

        rb.velocity /= isMoving ? 1f : decelerateSpeed;
    }

    void CameraMovement()
    {
        camera.position =
            Vector3.Lerp(camera.position, tankCameraPosition.position, cameraSpeedFollow * Time.deltaTime);
    }

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

    void RotateTank()
    {
        if (isMoving)
        {
            float angle = Mathf.Atan2(inputDirection.x, -inputDirection.y) * Mathf.Rad2Deg;
            Vector3 rotationTarget = new Vector3(0f, angle, 0f);

            //Rotate tank to move direction
            tank.rotation = Quaternion.Slerp(tank.rotation, Quaternion.Euler(rotationTarget),
                tankRotationSpeed * Time.deltaTime);
        }
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
        //mousePos = Input.mousePosition - cam.WorldToScreenPoint(transform.position);
        aimRotation = context.ReadValue<Vector2>();

        //Calculate the angle
        if (aimRotation.magnitude > Vector2.one.magnitude / 8f)
        {
            angleRotation = Mathf.RoundToInt(Mathf.Atan2(aimRotation.y, aimRotation.x) * Mathf.Rad2Deg);
            //lastAngle = angleRotation;
        }

        isAiming = SwitchContextPhase(context, isAiming);
    }

    void RotateCanon()
    {
        if (isAiming)
        {
            //Rotate Canon to aim direction
            //tower.rotation = Quaternion.AngleAxis(-lastAngle + 90f, Vector3.up);
            tower.rotation = Quaternion.RotateTowards(tower.rotation, Quaternion.Euler(0, -angleRotation + 90f, 0),
                Time.deltaTime * rotationSpeed);
        }
    }

    public void HandleShoot(InputAction.CallbackContext context)
    {
        isShooting = SwitchContextPhase(context, isShooting);
    }

    private void Shoot()
    {
        if (canShoot && isShooting)
        {
            //Calculates the direction
            Vector3 shootDirection = shootPosition.rotation * Vector3.forward;
            bulletType.ShootBullet(shootDirection, shootPosition);

            rb.AddForce(-shootDirection * 2f, ForceMode.Impulse);

            StartCoroutine(ResetShoot(bulletType.shootDelay));
        }
    }

    private IEnumerator ResetShoot(float shootDelay)
    {
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}