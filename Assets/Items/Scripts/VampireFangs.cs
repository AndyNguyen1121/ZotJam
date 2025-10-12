using UnityEngine;

public class VampireFangs : Item
{
    public override void Activate()
    {
        PlayerManager.instance.lifeStealChance += 1;
    }
}
