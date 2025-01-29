using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;  // Make sure you have this if you're using UI elements like buttons

public class MusicManager : MonoBehaviour
{
    private PersistentMusicManager musicManager;
    private bool isPlaying = true;  // Track whether the music is currently playing
    public Button musicButton;      // Reference to the UI Button
    public Text buttonText;         // Reference to the button text (if you want to change it)

    void Start()
    {
        // Find the PersistentMusicManager in the current scene (since it's not destroyed)
        StartCoroutine(FindMusicManager());

        // Set the button text to "Stop Music" if it's playing, or "Play Music" if it's stopped
        UpdateButtonText();
    }

    IEnumerator FindMusicManager()
    {
        yield return new WaitForSeconds(0.2f); // Small delay to allow PersistentMusicManager to initialize
        musicManager = PersistentMusicManager.instance;

        if (musicManager == null)
        {
            Debug.LogError("PersistentMusicManager is STILL not found!");
        }
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

    public void VolumeMusical(float value)
    {
        if (musicManager == null)
        {
            Debug.LogError("MusicManager is NULL! Make sure PersistentMusicManager exists in the scene.");
            return;
        }
        musicManager.Volume = value;
    }

    // Update the button text depending on the music state
    private void UpdateButtonText()
    {
        if (buttonText != null)
        {
            buttonText.text = isPlaying ? "Música on" : "Música off";
        }
    }
}
