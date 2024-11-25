using UnityEngine;

public class ZeroTimePower : MonoBehaviour
{
    public GMTeste gameManager;  // Reference to GMTeste script
    private bool player1PowerUsed = false;  // Track if Player 1 has used the power
    private bool player2PowerUsed = false;  // Track if Player 2 has used the power

    public bool skipNextTurn = false;  // Flag to control turn skipping
    public GlobalPowerLimit globalPowerLimit;  // Reference to the global power limit

    private int maxRoundsToUse = 3;  // Restriction: Power can only be used in the first 3 rounds

    public void ActivateZeroTimePower()
    {
        // Check if the power usage is within the allowed rounds
        if (gameManager.GetCurrentRound() > maxRoundsToUse)
        {
            Debug.LogWarning("Zero Time Power can only be used during the first 3 rounds.");
            return;
        }

        // Check global power limit
        if (!globalPowerLimit.CanUsePower(gameManager.currentTurn))
        {
            Debug.Log("Power limit reached for " + gameManager.currentTurn.ToString());
            globalPowerLimit.DisplayWarning();
            return;
        }

        // Check if the player has already used the power
        if (gameManager.currentTurn == GMTeste.PlayerType.Human && player1PowerUsed)
        {
            Debug.Log("Player 1 (Human) has already used Zero Time Power.");
            return;
        }
        else if (gameManager.currentTurn == GMTeste.PlayerType.Human2 && player2PowerUsed)
        {
            Debug.Log("Player 2 (Human2) has already used Zero Time Power.");
            return;
        }

        // Mark power as used for the respective player
        if (gameManager.currentTurn == GMTeste.PlayerType.Human)
        {
            player1PowerUsed = true;
        }
        else if (gameManager.currentTurn == GMTeste.PlayerType.Human2)
        {
            player2PowerUsed = true;
        }

        // Set the flag to skip the next turn switch
        skipNextTurn = true;
        gameManager.remainingTime = gameManager.turnDuration; // Reset the timer for the extra turn
        Debug.Log("Zero Time Power activated: Current player will play again.");
        globalPowerLimit.UsePower(gameManager.currentTurn);
    }
}
