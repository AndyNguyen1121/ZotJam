using UnityEngine;

public class GunAnimationEvent : MonoBehaviour
{
    public PlayerCombatManager playerCombatManager;
    
    public void Shoot()
    {
        playerCombatManager.ShootBullet();
    }
}
