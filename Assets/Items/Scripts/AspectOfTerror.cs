using UnityEngine;

public class AspectOfTerror : Item
{
    public override void Activate()
    {
        PlayerManager.instance.maxMovementSpeed *= 1.5f;
    }
}
