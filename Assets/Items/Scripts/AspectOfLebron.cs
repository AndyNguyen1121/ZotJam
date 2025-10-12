using UnityEngine;

public class AspectOfLebron : Item
{
    public override void Activate()
    {
        PlayerManager.instance.jumpHeight *= 1.5f;
    }
}
