using UnityEngine;

public class WeaponEquip : Item
{
    public GameObject prefab;
    public override void Activate()
    {
        PlayerManager.instance.EquipWeapon(prefab);
    }
}
