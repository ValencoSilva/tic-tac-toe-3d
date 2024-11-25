using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTurnPower : MonoBehaviour
{
    public GMTeste gameManager;  // Reference to your game manager script
    public GlobalPowerLimit globalPowerLimit;  // Reference to the global power limit
    private int roundLimit = 3;  // Restriction: Can only be used in the first 3 rounds
    private bool powerUsed = false;  // Tracks if the power has already been used this round

    public void ActivateDoubleTurnPower()
    {
        // Check global power limit
        if (!globalPowerLimit.CanUsePower(gameManager.currentTurn))
        {
            Debug.Log("Power limit reached for " + gameManager.currentTurn.ToString());
            globalPowerLimit.DisplayWarning();
            return;
        }

        // Restrict usage to the first 3 rounds
        if (gameManager.moveLog.Count / 2 >= roundLimit) // moveLog.Count / 2 estimates the round number
        {
            Debug.LogWarning("Double Turn Power can only be used in the first 3 rounds.");
            return;
        }

        // Check if the power has already been used this round
        if (powerUsed)
        {
            Debug.LogWarning("Double Turn Power has already been used this round.");
            return;
        }

        // Allow the current player to play twice in a row
        powerUsed = true;  // Mark the power as used
        globalPowerLimit.UsePower(gameManager.currentTurn);  // Mark the power globally
        Debug.Log(gameManager.currentTurn + " has activated Double Turn Power! Play twice in a row.");

        // Skip the turn change in the game manager
        gameManager.remainingTime = gameManager.turnDuration;  // Reset timer for the extra turn
        gameManager.UpdateTurnIndicator();  // Update the UI for the current player's extra turn
    }

    // Reset the power usage flag for the next round
    public void ResetPowerUsage()
    {
        powerUsed = false;
    }
}
