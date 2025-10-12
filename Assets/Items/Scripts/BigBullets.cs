using UnityEngine;

public class BigBullets : Item
{
    public override void Activate()
    {
        PlayerManager.instance.damage *= 1.1f;
    }
}
