using UnityEngine;

public class ZeroTimePower : MonoBehaviour
{
    public GMTeste gameManager;  // Reference to GMTeste script
    public ScoreManager scoreManager;  // Reference to ScoreManager script
    public GlobalPowerLimit globalPowerLimit;  // Reference to the global power limit

    private bool player1PowerUsed = false;  // Track if Player 1 has used the power
    private bool player2PowerUsed = false;  // Track if Player 2 has used the power
    public int powerCost = 20;  // Cost to activate this power
    private int maxRoundsToUse = 3;  // Restriction: Power can only be used in the first 3 rounds

    public bool skipNextTurn = false;  // Flag to control turn skipping

    public void ActivateZeroTimePower()
    {
        // Validate ScoreManager reference
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager is not assigned!");
            return;
        }

        // Restrict to first 3 rounds
        if (gameManager.GetCurrentRound() > maxRoundsToUse)
        {
            Debug.LogWarning("Zero Time Power can only be used during the first 3 rounds.");
            return;
        }

        // Check if the power cost can be paid
        if (!scoreManager.CanAffordPower(powerCost, gameManager.currentTurn))
        {
            Debug.LogWarning("Not enough points to activate Zero Time Power!");
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

        // Deduct points for the power usage
        scoreManager.DeductPoints(powerCost, gameManager.currentTurn);

        // Activate the power
        skipNextTurn = true;
        gameManager.remainingTime = gameManager.turnDuration; // Reset the timer for the extra turn
        Debug.Log("Zero Time Power activated: Current player will play again.");
        globalPowerLimit.UsePower(gameManager.currentTurn);
    }
}
