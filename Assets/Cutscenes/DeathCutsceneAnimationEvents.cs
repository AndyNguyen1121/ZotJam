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

    //public void 
}
