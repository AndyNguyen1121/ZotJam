using UnityEngine;

public class DeathCutsceneAnimationEvents : MonoBehaviour
{
    public Camera cutsceneCamera;
    public void DeactivatePlayerCamera()
    {
        if (PlayerManager.instance != null)
        {
            PlayerManager.instance.mainCam.enabled = false;
            cutsceneCamera.enabled = true;
        }
    }

    public void EndCutscene()
    {
        if (PlayerManager.instance == null || AreaManager.instance == null)
        {
            Debug.LogError("Null reference in end cutscene animation event");
        }
        AreaManager.instance.SwitchPlayerLocation(PlayerLocation.Hell);
        PlayerManager.instance.mainCam.enabled = true;
        PlayerManager.instance.SetHealthValue(50);
        PlayerManager.instance.deathSequenceStarted = false;
        
        Destroy(gameObject);
        
    }
}
