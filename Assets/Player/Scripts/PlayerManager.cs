using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public CharacterController characterController;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log("There is more than one player in the scene.");

        characterController = GetComponent<CharacterController>();
    }
}
