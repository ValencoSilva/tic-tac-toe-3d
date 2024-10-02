using UnityEngine;
using UnityEngine.UI;  // Required to handle UI text elements

public class GlobalPowerLimit : MonoBehaviour
{
    public int maxPowersAllowed = 1;  // Total number of powers that can be used by each player
    private int human1PowersUsed = 0;  // Counter to track how many powers Human1 has used
    private int human2PowersUsed = 0;  // Counter to track how many powers Human2 has used

    public Text warningText;  // Reference to the UI text element for warnings
    public float warningDisplayTime = 2f;  // Time to display the warning message

    // Check if any powers are still available to use for the given player
    public bool CanUsePower(GMTeste.PlayerType player)
    {
        if (player == GMTeste.PlayerType.Human)
        {
            return human1PowersUsed < maxPowersAllowed;
        }
        else if (player == GMTeste.PlayerType.Human2)
        {
            return human2PowersUsed < maxPowersAllowed;
        }
        return false;
    }

    // Increment the power usage counter when a power is used for the given player
    public void UsePower(GMTeste.PlayerType player)
    {
        if (player == GMTeste.PlayerType.Human && human1PowersUsed < maxPowersAllowed)
        {
            human1PowersUsed++;
            Debug.Log("Human1 used a power. Total powers used: " + human1PowersUsed);
        }
        else if (player == GMTeste.PlayerType.Human2 && human2PowersUsed < maxPowersAllowed)
        {
            human2PowersUsed++;
            Debug.Log("Human2 used a power. Total powers used: " + human2PowersUsed);
        }
        else
        {
            DisplayWarning();
            Debug.LogWarning("No more powers can be used for " + player.ToString() + "!");
        }
    }

    // Check if the power limit has been reached for the given player
    public bool HasReachedLimit(GMTeste.PlayerType player)
    {
        if (player == GMTeste.PlayerType.Human)
        {
            return human1PowersUsed >= maxPowersAllowed;
        }
        else if (player == GMTeste.PlayerType.Human2)
        {
            return human2PowersUsed >= maxPowersAllowed;
        }

        return true;
    }

    // Display a warning message when the player tries to use a power after reaching their limit
    public void DisplayWarning()
    {
        if (warningText != null)
        {
            warningText.text = " Poder foi utilizado.";
            warningText.gameObject.SetActive(true);
            Invoke(nameof(HideWarning), warningDisplayTime);  // Hide the warning after a short delay
        }
    }

    // Hide the warning text after the delay
    private void HideWarning()
    {
        if (warningText != null)
        {
            warningText.gameObject.SetActive(false);
        }
    }
}
