using UnityEngine;

public enum WeaponBehavior
{
    Auto,
    Single
}

[CreateAssetMenu(fileName = "WeaponProfile", menuName = "CreateWeaponProfile", order = 0)]
public class WeaponProfile : ScriptableObject
{
    public WeaponBehavior weaponBehavior;
    public float damage;
    public float fireRate;
    public float range;
    public bool spread;
    
}
