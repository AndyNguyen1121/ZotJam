using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class PlayerMovementManager : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField]
    private GameObject rotationObject;
    float pitch;
    float yaw;

    [Header("Movement")]
    public float gravity = -9;
    private Vector3 verticalVelocity;

    public float jumpCooldown = 0.2f;
    private float timeElapsedSinceLastJump;
    private float timeOnGround;

    public bool doubleJumpEnabled;
    public bool canDoubleJump;

    [Header("Rotation")]
    public float minPitch;
    public float maxPitch;

    [Header("Acceleration")]
    public float acceleration;
    public float deceleration;
    public float currentSpeed;
    public Vector3 lastMoveVelocity;

    [Header("JUICE")]
    public float minFOV;
    public float maxFOV;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = PlayerManager.instance.characterController;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.deathSequenceStarted)
            return;

        HandleMovement(); 
        HandleCameraRotations();
        HandleGravity();

        timeElapsedSinceLastJump += Time.deltaTime;
        timeOnGround += Time.deltaTime;
    }


    void HandleMovement()
    {
        Vector3 movementDir = PlayerInputManager.instance.GetMovementDirectionRelativeToCamera();
        movementDir.y = 0;
        movementDir.Normalize();
        
        if (PlayerInputManager.instance.movementInput != Vector2.zero)
        {
            if (currentSpeed < PlayerManager.instance.maxMovementSpeed)
                currentSpeed = Mathf.Lerp(currentSpeed, PlayerManager.instance.maxMovementSpeed, acceleration * Time.deltaTime);

            lastMoveVelocity = movementDir;
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, deceleration * Time.deltaTime);
            movementDir = lastMoveVelocity;
        }

        float t = Mathf.Clamp01(currentSpeed / PlayerManager.instance.maxMovementSpeed);
        float targetFOV = Mathf.Lerp(minFOV, maxFOV, t);
        PlayerManager.instance.virtualCamera.Lens.FieldOfView = Mathf.Lerp(PlayerManager.instance.virtualCamera.Lens.FieldOfView, targetFOV, acceleration * Time.deltaTime);
        Vector3 movementAmount = movementDir * currentSpeed;
        characterController.Move(movementAmount * Time.deltaTime);
        
    }

    void HandleGravity()
    {
        // reset gravity when grounded
        if (PlayerManager.instance.isGrounded && timeOnGround > 0.15f)
        {
            verticalVelocity.y = 0;
            canDoubleJump = true;
        }

        verticalVelocity.y += gravity * Time.deltaTime;

        characterController.Move(verticalVelocity * Time.deltaTime);
    }

    void HandleCameraRotations()
    {
        if (PlayerInputManager.instance.cameraInput == Vector2.zero)
            return;

        Vector2 input = PlayerInputManager.instance.cameraInput;

        yaw += input.x * 10 * Time.deltaTime;
        pitch -= input.y * 10 * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(0, yaw, 0f);
        rotationObject.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f); 
    }

    public void AttemptToJump()
    {
        if ((PlayerManager.instance.isGrounded && timeElapsedSinceLastJump > jumpCooldown))
        {
            verticalVelocity.y = Mathf.Sqrt(PlayerManager.instance.jumpHeight * -2.0f * gravity);
            timeOnGround = 0;
        }
        else if (!PlayerManager.instance.isGrounded && doubleJumpEnabled && canDoubleJump && timeElapsedSinceLastJump > jumpCooldown)
        {
            verticalVelocity.y = Mathf.Sqrt(PlayerManager.instance.jumpHeight * -2.0f * gravity);
            timeOnGround = 0;
            canDoubleJump = false;
        }
    }
}
