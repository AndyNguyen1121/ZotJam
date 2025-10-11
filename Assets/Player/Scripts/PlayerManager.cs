using Unity.Cinemachine;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public CharacterController characterController;
    public bool isGrounded;

    [Header("Ground Check")]
    public float groundCheckRadius;
    public Vector3 groundCheckOffset;
    public LayerMask groundMask;

    [Header("Camera")]
    public CinemachineCamera virtualCamera;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log("There is more than one player in the scene.");

        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        GroundCheck();
    }

    public void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.position + groundCheckOffset, groundCheckRadius, groundMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position +groundCheckOffset, groundCheckRadius);
    }
}
