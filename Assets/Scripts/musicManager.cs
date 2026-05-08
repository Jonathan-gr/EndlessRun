using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class MusicManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] musicTracks;

    private int currentTrack = 0;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NextTrack();

        }
    }
    private void Start()
    {
        PlayTrack(currentTrack);
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
        audioSource.clip = musicTracks[index];
        audioSource.Play();
    }
}