using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCubesPower : MonoBehaviour
{
    public GMTeste gameManager;  // Reference to your game manager script (GMTeste)
    public GameObject[] allCubes;  // Array of all cubes in the game
    public GlobalPowerLimit globalPowerLimit;  // Reference to GlobalPowerLimit script for power usage tracking
    private bool player1PowerUsed = false;  // Track if Player 1 has used their swap power
    private bool player2PowerUsed = false;  // Track if Player 2 has used their swap power

    public VictoryCheckTeste victoryCheck;  // Reference to VictoryCheckTeste for colors
    public ScoreManager scoreManager;       // Reference to ScoreManager for managing points
    public int powerCost = 150;             // Cost for using the power

    // Method to activate the power that swaps Player 1 and Player 2 cubes
    public void ActivateSwapCubesPower()
    {
        // Check if the player has enough points
        if (!scoreManager.CanAffordPower(powerCost, gameManager.currentTurn))
        {
            Debug.LogWarning($"Player {gameManager.currentTurn} does not have enough points to use this power.");
            return;
        }

        // Check if Player 1 or Player 2 has exceeded the global power limit
        if (!globalPowerLimit.CanUsePower(gameManager.currentTurn))
        {
            Debug.Log("Power limit reached. No more powers can be used.");
            globalPowerLimit.DisplayWarning();
            return;  // Exit if the player has reached their power limit
        }

        if (gameManager.currentTurn == GMTeste.PlayerType.Human)
        {
            if (player1PowerUsed)
            {
                Debug.Log("Player 1 (Human) has already used their swap cubes power.");
                return;  // Player 1 already used the power
            }

            SwapCubes();  // Swap the cubes
            player1PowerUsed = true;  // Mark Player 1's power as used
            Debug.Log("Player 1 (Human) has used the swap cubes power.");
        }
        else if (gameManager.currentTurn == GMTeste.PlayerType.Human2)
        {
            if (player2PowerUsed)
            {
                Debug.Log("Player 2 (Human2) has already used their swap cubes power.");
                return;  // Player 2 already used the power
            }

            SwapCubes();  // Swap the cubes
            player2PowerUsed = true;  // Mark Player 2's power as used
            Debug.Log("Player 2 (Human2) has used the swap cubes power.");
        }

        // Deduct points for using the power
        scoreManager.DeductPoints(powerCost, gameManager.currentTurn);

        // After using the power, mark it in the global power limit system
        globalPowerLimit.UsePower(gameManager.currentTurn);
    }

    // Helper method to swap the cubes between Player 1 and Player 2
    private void SwapCubes()
    {
        Color player1Color = victoryCheck.humanColor;  // Color for Player 1
        Color player2Color = victoryCheck.aiColor;     // Color for Player 2

        // Iterate through all the cubes and swap Player 1 and Player 2 cubes
        foreach (GameObject cube in allCubes)
        {
            Renderer cubeRenderer = cube.GetComponent<Renderer>();

            // Swap Player 1's cubes to Player 2's color
            if (cubeRenderer.material.color == player1Color)
            {
                cubeRenderer.material.color = player2Color;
            }
            // Swap Player 2's cubes to Player 1's color
            else if (cubeRenderer.material.color == player2Color)
            {
                cubeRenderer.material.color = player1Color;
            }
        }

        Debug.Log("Cubes swapped between Player 1 and Player 2.");
    }
}
