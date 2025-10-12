using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;
    private PlayerManager playerManager;
    public GameObject secondWindText;

    [Header("Sliders")]
    public Slider healthSlider;
    public Slider trailingHealthSlider;
    public Transform healthCanvasGroup;

    [Header("Death Menu")]
    public GameObject deathMenu;

    [Header("PauseMenu")]
    public GameObject pauseMenu;
    public bool isPaused;

    [Header("Sensitivity")]
    public Slider sensitivitySlider;

    private DG.Tweening.Sequence sliderUpdateSequence;
    private Tween sliderShake;
    private Tween hellSliderShake;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        playerManager = PlayerManager.instance;
        sensitivitySlider.onValueChanged.AddListener(OnSensitivitySliderChanged);

        playerManager.playerMovementManager.sensitivity = PlayerPrefs.GetFloat("Sensitivity", 0.15f);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                ActivatePauseMenu();
            }
            else
            {
                DisablePauseMenu();
            }
        }
    }

    // Update is called once per frame

    public void SetHealthSliderValue(float value)
    {
        healthSlider.value = value;
        trailingHealthSlider.value = value;
    }

    public void UpdateHealthSliders(float health, float maxHealth)
    {
        if (sliderShake != null)
            sliderShake.Rewind();

        if (sliderUpdateSequence != null)
            sliderUpdateSequence.Kill();

        sliderShake = healthCanvasGroup.DOShakePosition(0.5f, 10, 20, 90, false, true, ShakeRandomnessMode.Harmonic);

        sliderUpdateSequence = DOTween.Sequence();
        sliderUpdateSequence
            .Append(healthSlider.DOValue(health / maxHealth, 0.1f))
            .AppendInterval(0.5f)
            .Append(trailingHealthSlider.DOValue(health / maxHealth, 0.1f));
    }

    public void EnableDeathMenu()
    {
        deathMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    private void ActivatePauseMenu()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");

        PlayerInputManager.instance.playerControls.Disable();
    }

    public void DisablePauseMenu()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PlayerInputManager.instance.playerControls.Enable();
    }

    public void OnSensitivitySliderChanged(float value)
    {
        PlayerManager.instance.playerMovementManager.sensitivity = value;
        PlayerPrefs.SetFloat("Sensitivity", value);
        PlayerPrefs.Save();
    }

    public void DisableHellSliderShake()
    {
        if (hellSliderShake != null)
            hellSliderShake.Kill();
    }

    public void ActivateHellSliderShake()
    {
        if (hellSliderShake != null)
            hellSliderShake.Kill();

        hellSliderShake = healthCanvasGroup.DOShakePosition(100000f, 3);
    }

}