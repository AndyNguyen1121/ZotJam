using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponProfile weaponProfile;
    public GameObject gunTip;

    public void Equip()
    {
        if (weaponProfile == null)
        {
            Debug.Log("No weapon profile detected");
        }

        // set base attributes
        PlayerManager.instance.weaponBehavior = weaponProfile.weaponBehavior;
        PlayerManager.instance.baseDamage = weaponProfile.damage; 
        PlayerManager.instance.baseFireRate = weaponProfile.fireRate;
        PlayerManager.instance.baseRange = weaponProfile.range;
        PlayerManager.instance.canSpread = weaponProfile.spread;

        // adjust actual stats
        PlayerManager.instance.damage = weaponProfile.damage * PlayerManager.instance.damageMultiplier;
        PlayerManager.instance.fireRate = weaponProfile.fireRate * PlayerManager.instance.fireRateMultiplier;
        PlayerManager.instance.range = weaponProfile.range * PlayerManager.instance.rangeMultiplier;
    }
}
