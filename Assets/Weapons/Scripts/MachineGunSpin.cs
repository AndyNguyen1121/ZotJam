using DG.Tweening;
using UnityEngine;

public class MachineGunSpin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerManager.instance != null) 
            transform.DOLocalRotate(new Vector3(0, 0, 60), PlayerManager.instance.fireRate, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
