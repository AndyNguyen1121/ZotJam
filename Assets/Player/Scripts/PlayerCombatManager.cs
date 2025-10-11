using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatManager : MonoBehaviour
{
    private bool isFiring;
    private float nextFireTime;
    public TrailRenderer bulletTrail;

    [SerializeField]
    private Vector3 bulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);

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

        Vector3 dir = GetDirection();

        if (Physics.Raycast(PlayerManager.instance.mainCam.transform.position,
            dir,
            out hit,
            PlayerManager.instance.range,
            PlayerManager.instance.whatIsDamageable))
        {
            endPos = hit.point;
            Destroy(hit.collider.gameObject);
        }
        else
        {
            endPos = startPos + dir * PlayerManager.instance.range;
        }

        TrailRenderer trailRenderer = Instantiate(bulletTrail, startPos, Quaternion.identity);

        if (trailRenderer != null)
        {
            trailRenderer.AddPosition(startPos);
            trailRenderer.transform.position = endPos;
            Destroy(trailRenderer.gameObject, 0.1f);
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = PlayerManager.instance.mainCam.transform.forward;

        if (PlayerManager.instance.canSpread)
        {
            direction += new Vector3(
                Random.Range(-bulletSpreadVariance.x, bulletSpreadVariance.x),
                Random.Range(-bulletSpreadVariance.y, bulletSpreadVariance.y),
                Random.Range(-bulletSpreadVariance.z, bulletSpreadVariance.z)
            );

            direction.Normalize();
        }

        return direction;
    }

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
