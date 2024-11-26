using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullifyLastMovePower : MonoBehaviour
{
    public GMTeste gameManager;               // Reference to GMTeste
    public GlobalPowerLimit globalPowerLimit; // Reference to the global power limit
    public ScoreManager scoreManager;         // Reference to the scoring system

    public int powerCost = 50;                // Cost to use the power
    public int maxRounds = 3;                 // Maximum rounds during which the power can be used

    public void ActivateNullifyLastMovePower()
    {
        // Check if it's within the allowed rounds
        if (gameManager.moveLog.Count / 2 >= maxRounds) // Divide by 2 since each round includes two moves
        {
            Debug.LogWarning("This power can only be used during the first " + maxRounds + " rounds.");
            return;
        }

        // Check if the player has enough points
        if (!scoreManager.CanAffordPower(powerCost, gameManager.currentTurn))
        {
            Debug.LogWarning("Player " + gameManager.currentTurn + " does not have enough points to activate this power.");
            return;
        }

        // Check if the global power limit allows using the power
        if (!globalPowerLimit.CanUsePower(gameManager.currentTurn))
        {
            Debug.Log("Power limit reached for " + gameManager.currentTurn.ToString());
            globalPowerLimit.DisplayWarning();
            return;
        }

        // Check if there are moves to nullify
        if (gameManager.lastPlayedCubes == null || gameManager.lastPlayedCubes.Count == 0)
        {
            Debug.LogWarning("No moves to nullify. Queue is empty.");
            return;
        }

        // Deduct points for using the power
        scoreManager.DeductPoints(powerCost, gameManager.currentTurn);

        // Nullify the opponent's last move
        StartCoroutine(NullifyLastMove());
    }

    private IEnumerator NullifyLastMove()
    {
        GameObject lastOpponentCube = null;

        // Identify the last move made by the opponent
        if (gameManager.lastPlayedCubes.Count > 0)
        {
            lastOpponentCube = gameManager.lastPlayedCubes[gameManager.lastPlayedCubes.Count - 1]; // Get the most recent move

            if (lastOpponentCube != null)
            {
                Debug.Log("Nullifying the opponent's last move at cube position: " +
                          System.Array.IndexOf(gameManager.clickableObjects, lastOpponentCube));

                // Reset cube color and make it "unplayed"
                lastOpponentCube.GetComponent<Renderer>().material.color = Color.white;
                lastOpponentCube.SetActive(true);

                // Remove the last move from the log
                gameManager.lastPlayedCubes.Remove(lastOpponentCube);
            }
        }

        // If no opponent's move was found
        if (lastOpponentCube == null)
        {
            Debug.LogWarning("Failed to find an opponent's last move to nullify.");
        }

        // Use the power and apply the global limit
        globalPowerLimit.UsePower(gameManager.currentTurn);

        yield return null;
    }
}
