using DG.Tweening;
using System.Collections;
using UnityEngine;

public class SecondWindAnimation : MonoBehaviour
{
    public GameObject second;
    public GameObject wind;
    public GameObject secondWind;

    private Coroutine secondWindCoroutine;

    private void OnEnable()
    {
        if (secondWindCoroutine != null)
        {
            StopCoroutine(secondWindCoroutine);
        }
        secondWindCoroutine = StartCoroutine(AnimateText());
    }

    public IEnumerator AnimateText()
    {
        yield return new WaitForSeconds(0.25f);
        second.SetActive(true);
        wind.SetActive(false);
        secondWind.SetActive(false);
        PlayerManager.instance.screenShake.GenerateImpulseAt(PlayerManager.instance.transform.position,
            new Vector3(1.5f, 1.5f, 1.5f));

        yield return new WaitForSeconds(0.75f);

        second.SetActive(false);
        wind.SetActive(true);
        secondWind.SetActive(false);
        PlayerManager.instance.screenShake.GenerateImpulseAt(PlayerManager.instance.transform.position,
            new Vector3(1.5f, 1.5f, 1.5f));

        yield return new WaitForSeconds(0.75f);

        second.SetActive(false);
        wind.SetActive(false);
        secondWind.SetActive(true);
        PlayerManager.instance.screenShake.GenerateImpulseAt(PlayerManager.instance.transform.position,
            new Vector3(2, 2, 2));

    }
}
