using UnityEngine;

public enum SoundType
{
    GUNSHOT
}

public class soundManager : MonoBehaviour
{

    [SerializeField] private AudioClip[] soundList;
    private static soundManager instance;
    private AudioSource audioSource;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType Sound, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)Sound], volume);
    }
}
