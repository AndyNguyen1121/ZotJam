using UnityEngine;

public class Pill : Item
{
  
    public override void Activate()
    {
        PlayerManager.instance.Heal(10);
    }
}
