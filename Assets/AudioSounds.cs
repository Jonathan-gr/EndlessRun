using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public static PlayerAudio Instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip coinSound;

    [SerializeField] private AudioClip wallCrashSound;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayCoinSound()
    {
        audioSource.PlayOneShot(coinSound);
    }

    public void PlayCrashSound()
    {
        audioSource.PlayOneShot(wallCrashSound);
    }
}