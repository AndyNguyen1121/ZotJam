using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IDamageable
{
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public UnityEvent OnDeath { get; set; }
    public void TakeDamage(float value);
    public void Heal(float value);
    public void SetHealthValue(float value);
}