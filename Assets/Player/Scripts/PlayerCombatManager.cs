using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatManager : MonoBehaviour
{
    private bool isFiring;
    private float nextFireTime;
    public TrailRenderer bulletTrail;
    private void Update()
    {
        if (PlayerManager.instance.weaponBehavior == WeaponBehavior.Auto && isFiring)
        {
            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + 1f / PlayerManager.instance.fireRate;
            }
        }
    }

    public void OnFireStarted(InputAction.CallbackContext context)
    {
        isFiring = true;

        if (PlayerManager.instance.weaponBehavior == WeaponBehavior.Single)
        {
            Fire();
        }
    }

    public void OnFireCanceled(InputAction.CallbackContext context)
    {
        isFiring = false;
    }

    private void Fire()
    {
        RaycastHit hit;
        Vector3 startPos = PlayerManager.instance.currentGunTip.transform.position;
        Vector3 endPos;

        if (Physics.Raycast(PlayerManager.instance.mainCam.transform.position,
            PlayerManager.instance.mainCam.transform.forward,
            out hit,
            PlayerManager.instance.range,
            PlayerManager.instance.whatIsDamageable))
        {
            endPos = hit.point;
            Destroy(hit.collider.gameObject);
        }
        else
        {
            endPos = startPos + PlayerManager.instance.mainCam.transform.forward * PlayerManager.instance.range;
        }

        //StartCoroutine(SpawnTrail(startPos, endPos));

        
        TrailRenderer trailRenderer = Instantiate(bulletTrail, startPos, Quaternion.identity);

        if (trailRenderer != null)
        {
            trailRenderer.AddPosition(startPos);
            trailRenderer.transform.position = endPos;
            Destroy(trailRenderer.gameObject, 0.1f);
        }
    }

    /*private IEnumerator SpawnTrail(Vector3 start, Vector3 end)
    {
        GameObject trail = Instantiate(bulletTrail, start, Quaternion.identity);
        TrailRenderer trailRenderer = trail.GetComponent<TrailRenderer>();

        float time = 0f;
        float duration = 0.05f; // how long the bullet takes to reach the target

        while (time < 1f)
        {
            time += Time.deltaTime / duration;
            if (trail != null)
            {
                trail.transform.position = Vector3.Lerp(start, end, time);
            }
            yield return null;
        }

        if (trailRenderer != null)
            yield return new WaitForSeconds(trailRenderer.time);

        Destroy(trail);
    }
*/
    private void OnDrawGizmos()
    {
        if (PlayerManager.instance != null)
        {

            Gizmos.color = Color.red;
            Gizmos.DrawRay(PlayerManager.instance.mainCam.transform.position,
                PlayerManager.instance.mainCam.transform.forward *
                PlayerManager.instance.range);
        }

    }
}
