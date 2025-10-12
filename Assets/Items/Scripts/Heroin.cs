using UnityEngine;

public class Heroin : Item
{
    public override void Activate()
    {
        PlayerManager.instance.fireRate *= 1.2f;
    }
}
