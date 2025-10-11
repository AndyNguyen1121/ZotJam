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

        PlayerManager.instance.weaponBehavior = weaponProfile.weaponBehavior;
        PlayerManager.instance.damage = weaponProfile.damage; 
        PlayerManager.instance.fireRate = weaponProfile.fireRate;
        PlayerManager.instance.range = weaponProfile.range;
    }
}
