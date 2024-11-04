using UnityEngine;
using UnityEngine.UI;  // Make sure you have this if you're using UI elements like buttons

public class MusicManager : MonoBehaviour
{
    private PersistentMusicManager musicManager;
    private bool isPlaying = true;  // Track whether the music is currently playing
    public Button musicButton;      // Reference to the UI Button
    public Text buttonText;         // Reference to the button text (if you want to change it)

    void Start()
    {
        // Find the PersistentMusicManager in the current scene (since it's not destroyed)
        musicManager = FindObjectOfType<PersistentMusicManager>();
        
        if (musicManager == null)
        {
            Debug.LogError("PersistentMusicManager not found in the scene!");
        }

        // Set the button text to "Stop Music" if it's playing, or "Play Music" if it's stopped
        UpdateButtonText();
    }

    // Method to toggle music
    public void ToggleMusic()
    {
        if (musicManager != null)
        {
            if (isPlaying)
            {
                musicManager.StopMusic();
            }
            else
            {
                musicManager.PlayMusic();
            }

            // Toggle the playing state
            isPlaying = !isPlaying;

            // Update the button text to reflect the new state
            UpdateButtonText();
        }
    }

    // Update the button text depending on the music state
    private void UpdateButtonText()
    {
        if (buttonText != null)
        {
            buttonText.text = isPlaying ? "Música off" : "Música on";
        }
    }
}
