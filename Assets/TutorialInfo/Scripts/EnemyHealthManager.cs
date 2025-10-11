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
    public void TakeDamage(float value)
    {
        Health = Mathf.Max(Health - value, 0);

        if (Health == 0 && !deathSequenceStarted)
        {
            OnDeath.Invoke();
            deathSequenceStarted = true;
            Destroy(gameObject);
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
