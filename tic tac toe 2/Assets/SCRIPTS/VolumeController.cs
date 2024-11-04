using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;    // Reference to the UI slider
    private AudioSource audioSource;

    void Start()
    {
        // Find the AudioSource in the Persistent Music Manager or any other music source
        audioSource = FindObjectOfType<PersistentMusicManager>().GetComponent<AudioSource>();

        // Set the slider's value to the current audio volume
        if (audioSource != null)
        {
            volumeSlider.value = audioSource.volume;
        }

        // Add a listener to the slider to call the OnVolumeChanged function when the slider's value is changed
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    // This function is called when the slider value is changed
    public void OnVolumeChanged(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = volumeSlider.value;  // Update the audio source volume
        }
    }
}
