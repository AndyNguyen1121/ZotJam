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

        if (location == PlayerLocation.Overworld)
        {
            PlayerManager.instance.transform.position = playerSpawnOverworld.transform.position;
            PlayerManager.instance.currentLocation = location;
        }
        else
        {

            PlayerManager.instance.transform.position = playerSpawnHell.transform.position;
            Debug.Log("Spawn in hell");
            PlayerManager.instance.currentLocation = location;
        }

        PlayerManager.instance.characterController.enabled = true;
    }
}
