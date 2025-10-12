using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthManager : MonoBehaviour, IDamageable
{
    [Header("Health")]
    public float Health { get; set; }
    [field: SerializeField]
    public float MaxHealth { get; set; }
    public UnityEvent OnDeath { get; set; } = new UnityEvent();

    public bool deathSequenceStarted;

    private void Start()
    {
        Health = MaxHealth;
    }

    public void Ignite()
    {
        StartCoroutine(Burn(4));
    }

    IEnumerator Burn(float amount)
    {
    
        TakeDamage(amount);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Burn(amount));
    }
    public void TakeDamage(float value)
    {
        Health = Mathf.Max(Health - value, 0);

        if (Health == 0 && !deathSequenceStarted)
        {
            OnDeath.Invoke();
            deathSequenceStarted = true;
            Destroy(gameObject);

            //keeps track of how many enemies are alive
            if (enemySpawner.Instance != null)
            enemySpawner.Instance.enemiesAlive--;
        }
    }

    public void SetHealthValue(float value)
    {
        Health = value;
    }

    public void Heal(float value)
    {
        Health = Mathf.Min(MaxHealth, Health + value);
    }
}
