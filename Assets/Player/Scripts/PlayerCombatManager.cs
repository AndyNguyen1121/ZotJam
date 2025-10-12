using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatManager : MonoBehaviour
{
    private bool isFiring;
    private float nextFireTime;
    public TrailRenderer bulletTrail;
    public Animator gunAnimator;
    public AnimationClip singleShootClip;
    public AnimationClip autoShootClip;
    public float explosionRadius = 3f;
    public GameObject explosionParticle;
    public GameObject bloodParticle;

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
        if (PlayerManager.instance.weaponBehavior == WeaponBehavior.Single && Time.time >= nextFireTime)
        {
            gunAnimator.Play("SingleShoot", 0, 0);
            nextFireTime = Time.time + 1f / PlayerManager.instance.fireRate;
        }
        else if (PlayerManager.instance.weaponBehavior == WeaponBehavior.Auto)
        {
            gunAnimator.Play("AutoShoot", 0, 0);
        }
    }

    public void ShootBullet()
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

            IDamageable damageScript = hit.collider.gameObject.GetComponent<IDamageable>();
            if (damageScript != null)
            {

                Instantiate(bloodParticle, hit.point, Quaternion.identity);
                damageScript.TakeDamage(PlayerManager.instance.damage);
                if(Random.Range (0,101) < PlayerManager.instance.fireChance)
                {
                    damageScript.Ignite();
                }
            }

          
            if (Random.Range(0, 101) < PlayerManager.instance.explosionChance)
            {
                Collider[] enemyExplosion = Physics.OverlapSphere(hit.point, explosionRadius, PlayerManager.instance.whatIsDamageable);
                Instantiate(explosionParticle, hit.point, Quaternion.identity);
                foreach (Collider collider in enemyExplosion)
                {
                    IDamageable damage = collider.gameObject.GetComponent<IDamageable>();
                    if (damage != null)
                    {
                        damage.TakeDamage(PlayerManager.instance.damage * 1.5f);

                        //DebugDrawSphere(hit.point, explosionRadius, Color.green, 2f);
                    }
                }
            }
            
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

    /*void DebugDrawSphere(Vector3 center, float radius, Color color, float duration = 0f, int segments = 16)
    {
        for (int i = 0; i < segments; i++)
        {
            float theta1 = (i / (float)segments) * 2 * Mathf.PI;
            float theta2 = ((i + 1) / (float)segments) * 2 * Mathf.PI;

            // Draw circles on 3 planes
            Vector3 p1 = center + new Vector3(Mathf.Cos(theta1) * radius, Mathf.Sin(theta1) * radius, 0);
            Vector3 p2 = center + new Vector3(Mathf.Cos(theta2) * radius, Mathf.Sin(theta2) * radius, 0);
            Debug.DrawLine(p1, p2, color, duration);

            p1 = center + new Vector3(Mathf.Cos(theta1) * radius, 0, Mathf.Sin(theta1) * radius);
            p2 = center + new Vector3(Mathf.Cos(theta2) * radius, 0, Mathf.Sin(theta2) * radius);
            Debug.DrawLine(p1, p2, color, duration);

            p1 = center + new Vector3(0, Mathf.Cos(theta1) * radius, Mathf.Sin(theta1) * radius);
            p2 = center + new Vector3(0, Mathf.Cos(theta2) * radius, Mathf.Sin(theta2) * radius);
            Debug.DrawLine(p1, p2, color, duration);
        }
    }*/
}
