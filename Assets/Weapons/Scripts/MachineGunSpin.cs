using DG.Tweening;
using UnityEngine;

public class MachineGunSpin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Tween spinTween;
    void Start()
    {
        if (PlayerManager.instance != null)
        {
            spinTween = transform.DOLocalRotate(
                new Vector3(0, 0, 360),
                1,
                RotateMode.FastBeyond360
                )
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float currentFireRate = PlayerManager.instance.fireRate; // e.g. 0.04f = 25 shots/sec

        // timeScale controls how fast the tween plays
        // The base duration was made for 25 shots/sec, so scale accordingly:
        spinTween.timeScale = currentFireRate;
    }

    private void OnDestroy()
    {
        if (spinTween != null)
        {
            spinTween.Kill();
        }
    }
}
