using DG.Tweening;
using TMPro;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    private void OnEnable()
    {
        Color targetColor = textMeshProUGUI.color;
        targetColor.a = 1;

        textMeshProUGUI.DOColor(targetColor, 1);
    }

    private void OnDisable()
    {
        Color targetColor = textMeshProUGUI.color;
        targetColor.a = 0;

        textMeshProUGUI.DOColor(targetColor, 1);
    }
    private void Start()
    {
        transform.DOShakePosition(100000f, 4);
    }
}
