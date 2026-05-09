using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public static PlayerAudio Instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip coinSound;

    [SerializeField] private AudioClip wallCrashSound;

    [SerializeField] private AudioClip shieldSound;
    [SerializeField] private AudioClip shieldDownSound;

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

    public void PlayShieldSound()
    {
        audioSource.PlayOneShot(shieldSound);
    }
    public void PlayShieldDownSound()
    {
        audioSource.PlayOneShot(shieldDownSound);
    }
}