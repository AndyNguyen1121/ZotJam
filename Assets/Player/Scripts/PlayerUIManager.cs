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

    [Header("Sliders")]
    public Slider healthSlider;
    public Slider trailingHealthSlider;
    public Transform healthCanvasGroup;

    private DG.Tweening.Sequence sliderUpdateSequence;
    private Tween sliderShake;
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

}