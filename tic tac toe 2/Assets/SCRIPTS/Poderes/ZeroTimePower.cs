using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroTimePower : MonoBehaviour
{
    public GMTeste gameManager;  // Reference to your game manager script (GMTeste)
    public GlobalPowerLimit globalPowerLimit;  // Reference to GlobalPowerLimit script

    private bool player1PowerUsed = false;  // Track if Player 1 has used the Zero Time power
    private bool player2PowerUsed = false;  // Track if Player 2 has used the Zero Time power

    private float originalTurnDuration;  // Store the original turn duration to restore after the power is used

    // Method to activate the Zero Time power
    public void ActivateZeroTimePower()
    {
        // Check if the player has reached their global power limit
        if (!globalPowerLimit.CanUsePower(gameManager.currentTurn))
        {
            Debug.Log("Power limit reached. No more powers can be used.");
            return;
        }

        // Check if the current player has already used this power
        if (gameManager.currentTurn == GMTeste.PlayerType.Human && player1PowerUsed)
        {
            Debug.Log("Player 1 (Human) has already used the Zero Time power.");
            return;
        }
        else if (gameManager.currentTurn == GMTeste.PlayerType.Human2 && player2PowerUsed)
        {
            Debug.Log("Player 2 (Human2) has already used the Zero Time power.");
            return;
        }

        // Store the original turn duration to restore it later
        originalTurnDuration = gameManager.turnDuration;

        // Apply the Zero Time effect to all players except the current one
        ApplyZeroTimeEffect();

        // Mark the power as used
        if (gameManager.currentTurn == GMTeste.PlayerType.Human)
        {
            player1PowerUsed = true;
        }
        else if (gameManager.currentTurn == GMTeste.PlayerType.Human2)
        {
            player2PowerUsed = true;
        }

        // Register the power usage in the global limit
        globalPowerLimit.UsePower(gameManager.currentTurn);
    }

    // Function to set the time of other players' turns to zero for one round
    private void ApplyZeroTimeEffect()
    {
        // Immediately set the time for the next player’s turn to zero
        StartCoroutine(ApplyZeroTimeCoroutine());
    }

    private IEnumerator ApplyZeroTimeCoroutine()
    {
        // Immediately end the next turn by setting time to zero for all opponents
        if (gameManager.currentTurn == GMTeste.PlayerType.Human)
        {
            // Set time for Player 2's next turn to zero
            Debug.Log("Player 2 will have zero time on their next turn.");
            gameManager.turnDuration = 0f;
        }
        else if (gameManager.currentTurn == GMTeste.PlayerType.Human2)
        {
            // Set time for Player 1's next turn to zero
            Debug.Log("Player 1 will have zero time on their next turn.");
            gameManager.turnDuration = 0f;
        }

        // Wait until the turn is forced to end
        yield return new WaitForSeconds(0.1f);

        // After the affected player’s turn ends, restore the original turn duration
        gameManager.turnDuration = originalTurnDuration;
    }
}
