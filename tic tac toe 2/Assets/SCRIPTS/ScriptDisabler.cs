using System.Collections;
using UnityEngine;

public class ScriptDisabler : MonoBehaviour
{
    // Reference to the script you want to disable
    public MonoBehaviour scriptToDisable;

    // Function to disable the script for a given time duration
    public void DisableScriptForSeconds(float duration)
    {
        StartCoroutine(DisableScriptCoroutine(duration));
    }

    // Coroutine that disables the script for the duration, then re-enables it
    private IEnumerator DisableScriptCoroutine(float duration)
    {
        scriptToDisable.enabled = false;  // Disable the script
        yield return new WaitForSeconds(duration);  // Wait for the specified time
        scriptToDisable.enabled = true;  // Re-enable the script
    }

     // Function to disable the script when button is clicked
    public void DisableScriptOnClick()
    {
        scriptToDisable.enabled = false; // Disable the script indefinitely
    }

    public void EnableScriptOnClick()
    {
        scriptToDisable.enabled = true; // Disable the script indefinitely
    }
}
