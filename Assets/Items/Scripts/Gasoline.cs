using UnityEngine;

public class Gasoline : Item
{
    public override void Activate()
    {
        PlayerManager.instance.fireChance += 2;
    }
}
