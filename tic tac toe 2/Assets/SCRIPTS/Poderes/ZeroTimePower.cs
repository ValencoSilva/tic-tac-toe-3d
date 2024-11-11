using UnityEngine;

public class ZeroTimePower : MonoBehaviour
{
    public GMTeste gameManager;  // Reference to GMTeste script
    private bool player1PowerUsed = false;  // Track if Player 1 has used the power
    private bool player2PowerUsed = false;  // Track if Player 2 has used the power

    public bool skipNextTurn = false;  // Flag to control turn skipping
    public GlobalPowerLimit globalPowerLimit;  // Reference to the global power limit

    public void ActivateZeroTimePower()
    {
        if (!globalPowerLimit.CanUsePower(gameManager.currentTurn))
        {
            Debug.Log("Power limit reached for " + gameManager.currentTurn.ToString());
            globalPowerLimit.DisplayWarning();
            return;
        }
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
