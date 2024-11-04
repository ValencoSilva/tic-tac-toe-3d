using System.Collections;
using UnityEngine;

public class BlockClicks : MonoBehaviour
{
    private bool inputDisabled = false;

    // Call this function to disable input for a specific duration
    public void DisableInputForSeconds(float duration)
    {
        StartCoroutine(DisableInputCoroutine(duration));
    }

    // Coroutine that disables input for a set duration
    private IEnumerator DisableInputCoroutine(float duration)
    {
        inputDisabled = true; // Disable input
        yield return new WaitForSeconds(duration); // Wait for the specified time
        inputDisabled = false; // Re-enable input
    }

    // Example to block clicks and other inputs while input is disabled
    void Update()
    {
        if (inputDisabled)
        {
            // Block all input processing here
            return;
        }

        // Handle other input, like mouse clicks or key presses, if input is not disabled
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Screen clicked!");
        }
    }
}
