using UnityEngine;

public class RazorBlade : Item
{
    public override void Activate()
    {
        PlayerManager.instance.critMultiplier ++;
    }
}
