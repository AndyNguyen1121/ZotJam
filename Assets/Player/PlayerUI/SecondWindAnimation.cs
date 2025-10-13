using DG.Tweening;
using System.Collections;
using UnityEngine;

public class SecondWindAnimation : MonoBehaviour
{
    public GameObject welcome;
    public GameObject to;
    public GameObject hell;
    public GameObject welcomeToHell;

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
        welcome.SetActive(true);
        to.SetActive(false);
        hell.SetActive(false);
        welcomeToHell.SetActive(false);
        PlayerManager.instance.screenShake.GenerateImpulseAt(PlayerManager.instance.transform.position,
            new Vector3(1.5f, 1.5f, 1.5f));

        yield return new WaitForSeconds(0.75f);

        welcome.SetActive(false);
        to.SetActive(true);
        hell.SetActive(false);
        welcomeToHell.SetActive(false);
        PlayerManager.instance.screenShake.GenerateImpulseAt(PlayerManager.instance.transform.position,
            new Vector3(1.5f, 1.5f, 1.5f));

        yield return new WaitForSeconds(0.75f);

        welcome.SetActive(false);
        to.SetActive(false);
        hell.SetActive(true);
        welcomeToHell.SetActive(false);
        PlayerManager.instance.screenShake.GenerateImpulseAt(PlayerManager.instance.transform.position,
            new Vector3(2, 2, 2));

        yield return new WaitForSeconds(0.75f);

        welcome.SetActive(false);
        to.SetActive(false);
        hell.SetActive(false);
        welcomeToHell.SetActive(true);
        PlayerManager.instance.screenShake.GenerateImpulseAt(PlayerManager.instance.transform.position,
            new Vector3(2, 2, 2));

    }
}
