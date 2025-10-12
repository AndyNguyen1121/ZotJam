using DG.Tweening;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager instance;
    public GameObject overworld;
    public GameObject hell;

    public GameObject playerSpawnOverworld;
    public GameObject playerSpawnHell;  

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("There is more than one area manager in the scene.");
    }

    private void Start()
    {
        SwitchPlayerLocation(PlayerLocation.Overworld);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchPlayerLocation(PlayerLocation location)
    {
        if (PlayerManager.instance == null)
        {
            Debug.LogError("There is no playermanager in the scene.");
        }

        PlayerManager.instance.characterController.enabled = false;
        PlayerManager.instance.playerMovementManager.lastMoveVelocity = Vector3.zero;

        enemySpawner.Instance.KillAllEnemies();
        enemySpawner.Instance.NextWave();

        if (location == PlayerLocation.Overworld)
        {
            PlayerManager.instance.transform.position = playerSpawnOverworld.transform.position;
            PlayerManager.instance.currentLocation = location;

            PlayerManager.instance.overworldTheme.DOFade(1, 1f);
            PlayerManager.instance.underworldTheme.DOFade(0, 1f);

            overworld.SetActive(true);
            hell.SetActive(false);

            PlayerManager.instance.playerUIManager.secondWindText.SetActive(false);
            PlayerManager.instance.playerMovementManager.maxFOV = 75;
            PlayerManager.instance.playerUIManager.DisableHellSliderShake();
            PlayerManager.instance._vignette.color.value = Color.black;
            PlayerManager.instance.ActivateOverworldSkybox();

        }
        else
        {
            PlayerManager.instance.transform.position = playerSpawnHell.transform.position;
            Debug.Log("Spawn in hell");
            PlayerManager.instance.currentLocation = location;

            PlayerManager.instance.overworldTheme.DOFade(0, 1f);
            PlayerManager.instance.underworldTheme.DOFade(1, 1f);

            overworld.SetActive(false);
            hell.SetActive(true);

            PlayerManager.instance.playerUIManager.secondWindText.SetActive(true);
            PlayerManager.instance.playerMovementManager.maxFOV = 85;
            PlayerManager.instance.playerUIManager.ActivateHellSliderShake();
            PlayerManager.instance._vignette.color.value = PlayerManager.instance.hellColor;
            PlayerManager.instance.ActivateHellSkybox();
        }

        Debug.Log("enabled");

        PlayerManager.instance.characterController.enabled = true;

        
    }
}
