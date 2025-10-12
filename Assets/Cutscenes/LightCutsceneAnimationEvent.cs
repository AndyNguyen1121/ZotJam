using UnityEngine;

public class LightCutsceneAnimationEvent : MonoBehaviour
{
    public void SwitchToOverworld()
    {
        AreaManager.instance.SwitchPlayerLocation(PlayerLocation.Overworld);
    }

    public void DestroyTheCutscene()
    {
        Destroy(gameObject);
    }
}
