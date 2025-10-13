using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public Tween tween;
    public float shake = 4;
    private void OnEnable()
    {
        Color targetColor = textMeshProUGUI.color;
        targetColor.a = 1;

        textMeshProUGUI.DOColor(targetColor, 1);
        tween = transform.DOShakePosition(100000f, shake);
    }

    private void OnDisable()
    {
        Color targetColor = textMeshProUGUI.color;
        targetColor.a = 0;

        textMeshProUGUI.DOColor(targetColor, 1);

        if (tween != null)
            tween.Kill();
    }
}

