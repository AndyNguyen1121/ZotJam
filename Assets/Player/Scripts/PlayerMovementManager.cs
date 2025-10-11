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
    public float movementSpeed = 5f;

    [Header("Rotation")]
    public float minPitch;
    public float maxPitch;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = PlayerManager.instance.characterController;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement(); 
        HandleCameraRotations();
    }

    void HandleMovement()
    {
        if (PlayerInputManager.instance.movementInput == Vector2.zero)
            return;

        Vector3 movementDir = PlayerInputManager.instance.GetMovementDirectionRelativeToCamera();
        movementDir.y = 0;
        movementDir.Normalize();


        characterController.Move(movementDir * movementSpeed * Time.deltaTime);
        
    }

    void HandleCameraRotations()
    {
        if (PlayerInputManager.instance.cameraInput == Vector2.zero)
            return;

        Vector2 input = PlayerInputManager.instance.cameraInput;

        yaw += input.x;
        pitch -= input.y;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        rotationObject.transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
