using UnityEngine;

public class WSpeed : Item
{
    public override void Activate()
    {
        PlayerManager.instance.maxMovementSpeed *= 1.05f;
    }
}
