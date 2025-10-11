using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public float damage = 7f;
    public float speed = 3f;
    public Rigidbody rb;
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (transform.forward * speed * Time.fixedDeltaTime));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable damageScript = other.GetComponent<IDamageable>();
            if (damageScript != null)
            {
                damageScript.TakeDamage(damage);
            }
        }
    }
}
