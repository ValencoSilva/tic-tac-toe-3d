using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCubePower : MonoBehaviour
{
    public GMTeste gameManager;               // Reference to your game manager script (GMTeste)
    public GameObject[] allCubes;             // Reference to all cubes in the game
    public GlobalPowerLimit globalPowerLimit; // Reference to the global power limit
    public ScoreManager scoreManager;         // Reference to the ScoreManager for managing points
    public int powerCost = 50;                // Cost to activate this power

    // Method to activate the "remove cube" power
    public void ActivateRemoveOpponentCubePower()
    {
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

        // Deduct points for using the power
        scoreManager.DeductPoints(powerCost, gameManager.currentTurn);

        // Apply the power based on the current player
        if (gameManager.currentTurn == GMTeste.PlayerType.Human)
        {
            // Randomly remove a cube played by Player 2 (Human2)
            RemoveOpponentCube(gameManager.ScriptA.aiColor);  // Assuming Player 2 is using aiColor
            Debug.Log("Player 1 (Human) has used the remove cube power.");
        }
        else if (gameManager.currentTurn == GMTeste.PlayerType.Human2)
        {
            // Randomly remove a cube played by Player 1 (Human)
            RemoveOpponentCube(gameManager.ScriptA.humanColor);  // Assuming Player 1 is using humanColor
            Debug.Log("Player 2 (Human2) has used the remove cube power.");
        }

        // Mark that the current player has used their power
        globalPowerLimit.UsePower(gameManager.currentTurn);
    }

    // Helper function to remove a random cube played by the opponent
    private void RemoveOpponentCube(Color opponentColor)
    {
        // Find all cubes that match the opponent's color (i.e., cubes played by the opponent)
        List<GameObject> opponentCubes = new List<GameObject>();
        foreach (GameObject cube in allCubes)
        {
            if (cube.GetComponent<Renderer>().material.color == opponentColor)
            {
                opponentCubes.Add(cube);
            }
        }

        // Check if there are any cubes to remove
        if (opponentCubes.Count > 0)
        {
            // Randomly select one cube from the opponent's cubes
            GameObject randomCube = opponentCubes[Random.Range(0, opponentCubes.Count)];
            // Reset the cube's color (assuming white is the neutral color)
            randomCube.GetComponent<Renderer>().material.color = Color.white;
            Debug.Log("Removed opponent's cube at position: " + randomCube.transform.position);
        }
        else
        {
            Debug.Log("No cubes to remove for the opponent.");
        }
    }
}
