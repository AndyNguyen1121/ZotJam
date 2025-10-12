using UnityEngine;

public class DeathCutsceneAnimationEvents : MonoBehaviour
{
    public Camera cutsceneCamera;
    public void DeactivatePlayerCamera()
    {
        if (PlayerManager.instance != null)
        {
            PlayerManager.instance.mainCam.enabled = false;
            PlayerManager.instance.mainCam.targetTexture = null;
            cutsceneCamera.enabled = true;
            cutsceneCamera.targetTexture = PlayerManager.instance.renderTexture;
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
        PlayerManager.instance.mainCam.targetTexture = PlayerManager.instance.renderTexture;
        PlayerManager.instance.SetHealthValue(50);
        PlayerManager.instance.deathSequenceStarted = false;
        
        Destroy(gameObject);
        
    }
}
