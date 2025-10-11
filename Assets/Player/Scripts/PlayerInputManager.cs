using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;
    public PlayerControls playerControls;
    private Camera mainCamera;

    [Header("Input")]
    public Vector2 movementInput;
    public Vector2 cameraInput;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("More than one input manager in scene");
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public Vector3 GetMovementDirectionRelativeToCamera()
    {
        Vector3 direction = mainCamera.transform.right * movementInput.x;
        direction += mainCamera.transform.forward * movementInput.y;

        return direction.normalized;
    }

    private void OnEnable()
    {
        if (playerControls == null)
            playerControls = new PlayerControls();

        // Bind input actions
        playerControls.Action.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        playerControls.Action.Camera.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
