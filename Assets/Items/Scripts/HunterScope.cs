using UnityEngine;

public class HunterScope : Item
{
    public override void Activate()
    {
        PlayerManager.instance.critChance += 2;
    }
}
