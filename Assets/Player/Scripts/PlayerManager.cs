using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public static PlayerManager instance;

    [HideInInspector]
    public CharacterController characterController;

    [HideInInspector]
    public PlayerCombatManager playerCombatManager;

    [HideInInspector]
    public Camera mainCam;

    [Header("Attributes")]
    public WeaponBehavior weaponBehavior;
    public float damage;
    public float fireRate;
    public float range;
    public float jumpHeight;
    public float maxMovementSpeed;
    
    [Header("Health")]
    public float Health { get; set; }
    [field: SerializeField]
    public float MaxHealth { get; set; }
    public UnityEvent OnDeath { get; set; } = new UnityEvent();
    private UnityEvent OnHealthChanged = new UnityEvent();


    [Header("Ground Check")]
    public bool isGrounded;
    public float groundCheckRadius;
    public Vector3 groundCheckOffset;
    public LayerMask whatIsGround;

    [Header("Camera")]
    public CinemachineCamera virtualCamera;

    [Header("Weapon")]
    public Transform gunSocket;
    public GameObject currentWeapon;
    public GameObject currentGunTip;
    public LayerMask whatIsDamageable;

    [Header("Weapon Types")]
    public GameObject testWeapon;
    public GameObject testWeapon2;

    private bool deathSequenceStarted;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log("There is more than one player in the scene.");

        characterController = GetComponent<CharacterController>();
        playerCombatManager = GetComponent<PlayerCombatManager>();
        Health = MaxHealth;
    }

    private void Start()
    {
        mainCam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        EquipWeapon(testWeapon);
    }

    private void Update()
    {
        GroundCheck();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(testWeapon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(testWeapon2);
        }

    }

    public void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.position + groundCheckOffset, groundCheckRadius, whatIsGround);
    }

    public void TakeDamage(float value)
    {
        Health = Mathf.Max(Health - value, 0);

        if (Health == 0 && !deathSequenceStarted)
        {
            OnDeath.Invoke();
            deathSequenceStarted = true;
        }

        OnHealthChanged.Invoke();
    }

    public void SetHealthValue(float value)
    {
        Health = value;
        OnHealthChanged.Invoke();
    }

    public void Heal(float value)
    {
        Health = Mathf.Min(MaxHealth, Health + value);
        OnHealthChanged.Invoke();
    }

    public void EquipWeapon(GameObject weaponObject)
    {
        Weapon newWeapon;
        if (weaponObject.TryGetComponent<Weapon>(out newWeapon))
        {
            Destroy(currentWeapon);
            currentWeapon = Instantiate(weaponObject, gunSocket);
            currentGunTip = currentWeapon.GetComponent<Weapon>().gunTip;
            newWeapon.Equip();
        }
        else
        {
            Debug.Log("Weapon script is not detected.");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position +groundCheckOffset, groundCheckRadius);
    }
}
