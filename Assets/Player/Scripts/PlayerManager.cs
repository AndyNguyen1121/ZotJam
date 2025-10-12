using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum PlayerLocation
{
    Overworld,
    Hell
}
public class PlayerManager : MonoBehaviour, IDamageable
{
    public static PlayerManager instance;
    public PlayerLocation currentLocation;

    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public PlayerCombatManager playerCombatManager;
    [HideInInspector] public PlayerUIManager playerUIManager;
    [HideInInspector] public PlayerMovementManager playerMovementManager;
    [HideInInspector] public Camera mainCam;

    // Private variables
    [SerializeField] private float _damageMultipler = 1f;
    [SerializeField] private float _fireRateMultiplier = 1f;
    [SerializeField] private float _rangeMultiplier = 1f;

    [Header("Attributes")]
    public WeaponBehavior weaponBehavior;

    public float damageMultiplier
    {
        get => _damageMultipler;
        set
        {
            damage = baseDamage * value;
        }
    }
    public float fireRateMultiplier
    {
        get => _fireRateMultiplier;
        set
        {
            fireRate = baseFireRate * value;
        }
    }
    public float rangeMultiplier
    {
        get => _rangeMultiplier;
        set
        {
            range = baseRange * value;
        }
    }

    public float fireChance = 0;
    public float explosionChance = 0;

    public float jumpHeight;
    public float maxMovementSpeed;

    
    public float damage;
    public float fireRate;
    public float range;
    public bool canSpread;

    // Do not modify outside of equipping weapon
    [HideInInspector] public float baseDamage;
    [HideInInspector] public float baseFireRate;
    [HideInInspector] public float baseRange;

    

    [Header("Health")]
    public float Health { get; set; }
    [field: SerializeField]
    public float MaxHealth { get; set; }
    public UnityEvent OnDeath { get; set; } = new UnityEvent();


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
    public CinemachineImpulseSource screenShake;

    [Header("Weapon Types")]
    public GameObject testWeapon;
    public GameObject testWeapon2;

    [Header("Vignette")]
    [SerializeField] private Volume _volume;
    [HideInInspector] public Vignette _vignette;
    private Coroutine vignetteFlash;
    private float _vignetteFlashDuration = 0.35f;
    public Color hellColor;

    [Header("Death Sequence")]
    public GameObject deathCutscene;
    public bool deathSequenceStarted;
    public RenderTexture renderTexture;

    [Header("Audio")]
    public AudioSource overworldTheme;
    public AudioSource underworldTheme;


    public void Ignite()
    {
        
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log("There is more than one player in the scene.");

        characterController = GetComponent<CharacterController>();
        playerCombatManager = GetComponent<PlayerCombatManager>();
        playerUIManager = GetComponent<PlayerUIManager>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
        Health = MaxHealth;
        _volume.profile.TryGet(out _vignette);
    }

    private void Start()
    {
        mainCam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        EquipWeapon(testWeapon);

        renderTexture = mainCam.targetTexture;
    }

    private void Update()
    {
        GroundCheck();

        if (Input.GetKeyDown(KeyCode.J))
        {
            EquipWeapon(testWeapon);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            EquipWeapon(testWeapon2);
            TakeDamage(10);
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
            //playerUIManager.EnableDeathMenu();
            deathSequenceStarted = true;

            if (currentLocation == PlayerLocation.Overworld)
                PlayRikaCutscene();
            else
                playerUIManager.EnableDeathMenu();

        }
        else
        {

            playerUIManager.UpdateHealthSliders(Health, MaxHealth);
            screenShake.GenerateImpulseAt(transform.position, new Vector3(2, 2, 2));

            if (vignetteFlash != null)
            {
                StopCoroutine(vignetteFlash);
            }

            vignetteFlash = StartCoroutine(DamageVignette());
        }
    }

    public void SetHealthValue(float value)
    {
        Health = value;
        playerUIManager.UpdateHealthSliders(Health, MaxHealth);
    }

    public void Heal(float value)
    {
        Health = Mathf.Min(MaxHealth, Health + value);
        playerUIManager.UpdateHealthSliders(Health, MaxHealth);
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

    private IEnumerator DamageVignette()
    {
        float elapsedTime = 0;
        _vignette.color.value = Color.black;

        Color originalColor = Color.black;
        if (currentLocation == PlayerLocation.Hell)
        {
            originalColor = hellColor;
        }

        while (elapsedTime < (_vignetteFlashDuration / 2))
        {
            _vignette.color.value = Color.Lerp(originalColor, Color.red, elapsedTime / (_vignetteFlashDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        elapsedTime = 0;

        while (elapsedTime < (_vignetteFlashDuration / 2))
        {
            _vignette.color.value = Color.Lerp(Color.red, originalColor, elapsedTime / (_vignetteFlashDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        _vignette.color.value = originalColor;
    }

    private void PlayRikaCutscene()
    {
        Vector3 position = transform.position;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity, whatIsGround))
        {
            position = hit.point;
            position.y += 1f;
        }

        Instantiate(deathCutscene, position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position +groundCheckOffset, groundCheckRadius);
    }
}
