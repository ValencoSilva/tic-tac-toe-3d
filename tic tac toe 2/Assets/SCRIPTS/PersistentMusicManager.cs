using UnityEngine;

public class PersistentMusicManager : MonoBehaviour
{
    private static PersistentMusicManager instance = null;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Prevent this object from being destroyed when switching scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject);  // Destroy any duplicate music managers
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();  // Start playing the music if it's not already playing
        }
    }

    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void PauseMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }
}
