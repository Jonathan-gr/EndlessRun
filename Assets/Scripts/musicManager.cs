using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Audio Components")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] musicTracks;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float masterVolume = 0.5f; // Slider to control "weakness"

    private int currentTrack = 0;

    private void Start()
    {
        // Safety check in case you forgot to drag the AudioSource
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        PlayTrack(currentTrack);
    }

    void Update()
    {
        // This allows you to change the volume slider in the 
        // Inspector and hear it change immediately while playing.
        audioSource.volume = masterVolume;

        // Press E to skip to next track
        if (Input.GetKeyDown(KeyCode.E))
        {
            NextTrack();
        }

        // Press Q to go to previous track (Optional)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PreviousTrack();
        }
    }

    public void NextTrack()
    {
        currentTrack++;

        if (currentTrack >= musicTracks.Length)
            currentTrack = 0;

        PlayTrack(currentTrack);
    }

    public void PreviousTrack()
    {
        currentTrack--;

        if (currentTrack < 0)
            currentTrack = musicTracks.Length - 1;

        PlayTrack(currentTrack);
    }

    void PlayTrack(int index)
    {
        if (musicTracks.Length == 0) return;

        audioSource.Stop();
        audioSource.clip = musicTracks[index];
        audioSource.volume = masterVolume;
        audioSource.Play();
    }
}
