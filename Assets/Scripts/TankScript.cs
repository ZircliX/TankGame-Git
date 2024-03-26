using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankScript : MonoBehaviour
{
    [Header("Movement")] 
        [SerializeField] internal float moveSpeed = 50f;
        [SerializeField] internal float decelerateSpeed = 1.01f;
        
        [Space]
        private Vector2 inputDirection;
        private bool isMoving = false;
        [SerializeField] internal float tankRotationSpeed;
        

    [Header("Camera")]
        [SerializeField] internal float cameraSpeedFollow = 5f;

    [Header("References")] 
        [SerializeField] internal Rigidbody rb;
        [SerializeField] internal new Transform camera;
        [SerializeField] internal Transform tankCameraPosition;
        [SerializeField] internal Transform tank;
        
        private CanonScript canonScript;

    private Vector3 moveDirection;
    private Camera cam;

    void Awake()
    {
        canonScript = GetComponent<CanonScript>();
        cam = Camera.main;
    }

    void Update()
    {
        CameraMovement();
        RotateTank();
        
        isMoving = inputDirection != Vector2.zero;
    }

    private void RotateTank()
    {
        if (isMoving)
        {
            float angle = Mathf.Atan2(inputDirection.x, -inputDirection.y) * Mathf.Rad2Deg;
            Vector3 rotationTarget = new Vector3(0f, angle, 0f);
            
            //Rotate tank to move direction
            tank.rotation = Quaternion.Slerp(tank.rotation, Quaternion.Euler(rotationTarget), tankRotationSpeed * Time.deltaTime);
        }
    }

    private void CameraMovement()
    {
        camera.position = Vector3.Lerp(camera.position, tankCameraPosition.position, cameraSpeedFollow * Time.deltaTime);
    }

    public void HandleMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
        
        moveDirection = (Vector3.forward * inputDirection.x + Vector3.right * inputDirection.y).normalized;
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);
        
        rb.velocity /= isMoving ? 1f : decelerateSpeed;
    }

    public void HandleShoot(InputAction.CallbackContext context)
    {
        canonScript.Shoot();
    }
}