using UnityEngine;

public enum SoundType
{
     GUNSHOT,
    BLOOD_PISTOL,
    HELL_DRAG,
    EERIE,
    EVIL_BALL,
    EXPLOSION,
    HIT_FX,
    LAVA_LAUNCHER,
    MONSTER_ATK,
    MONSTER_SCREAM,
    SKULL_GUN,
    ZOMBIE_GROAN
}

[RequireComponent(typeof(AudioSource))]

public class soundManager : MonoBehaviour
{

    [SerializeField] private AudioClip[] soundList;
    public static soundManager instance;
    private AudioSource audioSource;
    [SerializeField] AudioSource soundPositionPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundType Sound, float volume = 1)
    {
        audioSource.PlayOneShot(soundList[(int)Sound], volume);
    }

    public void PlaySoundAtPosition(SoundType Sound, Vector3 position, float volume = 1)
    {
        AudioSource.PlayClipAtPoint(soundList[(int)Sound], position, volume);
    }
}
