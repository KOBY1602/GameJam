using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance; // Singleton instance

    [SerializeField] private AudioSource musicSource; // The AudioSource component for playing music
    [SerializeField] private AudioClip[] musicClips; // Array of background music clips

    private int currentMusicIndex = 0; // Index of the current music clip

    private float volume = 0.3f; // Hardcoded volume level (set it to your desired value)

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of MusicManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy this object when loading new scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic();
    }

    // Method to play music
    public void PlayMusic()
    {
        if (musicClips.Length > 0)
        {
            musicSource.clip = musicClips[currentMusicIndex];
            musicSource.volume = volume; // Set the volume
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("No music clips assigned to the MusicManager!");
        }
    }

    // Method to change music to the next clip in the array
    public void NextMusic()
    {
        currentMusicIndex = (currentMusicIndex + 1) % musicClips.Length;
        PlayMusic();
    }

    // Method to change music to a specific clip
    public void PlaySpecificMusic(int index)
    {
        if (index >= 0 && index < musicClips.Length)
        {
            currentMusicIndex = index;
            PlayMusic();
        }
        else
        {
            Debug.LogWarning("Invalid music index!");
        }
    }

    // Method to stop the music
    public void StopMusic()
    {
        musicSource.Stop();
    }
}
