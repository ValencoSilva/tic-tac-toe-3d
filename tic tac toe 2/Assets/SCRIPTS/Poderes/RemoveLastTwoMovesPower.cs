using System.Collections;
using UnityEngine;

public class RemoveLastTwoMovesPower : MonoBehaviour
{
    public GMTeste gameManager;               // Reference to GMTeste
    public GlobalPowerLimit globalPowerLimit; // Reference to the global power limit
    public ScoreManager scoreManager;         // Reference to the ScoreManager for managing points
    public int powerCost = 100;               // Cost to activate this power

    public void ActivateRemoveLastTwoMovesPower()
    {
        // Check if the player can afford the power
        if (!scoreManager.CanAffordPower(powerCost, gameManager.currentTurn))
        {
            Debug.LogWarning($"Player {gameManager.currentTurn} does not have enough points to use this power.");
            return;
        }

        // Check if the power is allowed by the global power limit
        if (!globalPowerLimit.CanUsePower(gameManager.currentTurn))
        {
            Debug.Log("Power limit reached for " + gameManager.currentTurn.ToString());
            globalPowerLimit.DisplayWarning();
            return;
        }

        // Check if there are moves to remove
        if (gameManager.lastPlayedCubes.Count == 0)
        {
            Debug.LogWarning("No moves to remove. Queue is empty.");
            return;
        }

        // Deduct points for using the power
        scoreManager.DeductPoints(powerCost, gameManager.currentTurn);

        // Start the process of removing the last two moves
        StartCoroutine(RemoveLastTwoMoves());
    }

    private IEnumerator RemoveLastTwoMoves()
    {
        int movesToRemove = Mathf.Min(2, gameManager.lastPlayedCubes.Count); // Remove up to the last 2 moves

        for (int i = 0; i < movesToRemove; i++)
        {
            GameObject cube = gameManager.lastPlayedCubes[gameManager.lastPlayedCubes.Count - 1]; // Get the last played cube

            if (cube != null)
            {
                Debug.Log($"Resetting cube at position {System.Array.IndexOf(gameManager.clickableObjects, cube)}");

                // Reset the cube's state
                cube.GetComponent<Renderer>().material.color = Color.white; // Reset color to unplayed
                cube.SetActive(true); // Ensure the cube is active if needed
            }

            gameManager.lastPlayedCubes.RemoveAt(gameManager.lastPlayedCubes.Count - 1); // Remove the cube from the list
        }

        // Apply the global power limit
        globalPowerLimit.UsePower(gameManager.currentTurn);

        yield return null;
    }
}
