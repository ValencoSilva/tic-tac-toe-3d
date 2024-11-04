using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverwriteCubePower : MonoBehaviour
{
    public GMTeste gameManager;  // Reference to your game manager script (GMTeste)
    public GameObject[] allCubes; // Reference to all cubes in the game
    public GlobalPowerLimit globalPowerLimit;  // Reference to GlobalPowerLimit script for power usage tracking
    public VictoryCheckTeste victoryCheck;  // Reference to VictoryCheckTeste for colors

    private bool player1PowerUsed = false;  // Track if Player 1 has used their overwrite power
    private bool player2PowerUsed = false;  // Track if Player 2 has used their overwrite power
    private bool isOverwriteActive = false; // Whether the overwrite power is currently being used

    // Method to activate the "overwrite cube" power
    public void ActivateOverwriteCubePower()
    {
        // Check if the player has reached their global power limit
        if (!globalPowerLimit.CanUsePower(gameManager.currentTurn))
        {
            Debug.Log("Power limit reached. No more powers can be used.");
            return;  // Exit if the player has reached their power limit
        }

        // Check if the player has already used this power
        if (gameManager.currentTurn == GMTeste.PlayerType.Human && player1PowerUsed)
        {
            Debug.Log("Player 1 (Human) has already used their overwrite cube power.");
            return;  // Player 1 already used the power
        }
        else if (gameManager.currentTurn == GMTeste.PlayerType.Human2 && player2PowerUsed)
        {
            Debug.Log("Player 2 (Human2) has already used their overwrite cube power.");
            return;  // Player 2 already used the power
        }

        Debug.Log("Overwrite power activated. Choose a cube to overwrite.");
        isOverwriteActive = true;  // Set the overwrite mode active

        // The player will now click a cube to choose which one to overwrite
    }

    private void Update()
    {
        if (isOverwriteActive && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject selectedCube = hit.transform.gameObject;

                // Ensure the selected cube is valid (it belongs to the opponent)
                if (IsValidCubeToOverwrite(selectedCube))
                {
                    // Overwrite the cube
                    OverwriteSelectedCube(selectedCube);

                    // After using the power, mark it in the global power limit system and switch turn
                    globalPowerLimit.UsePower(gameManager.currentTurn);

                    if (gameManager.currentTurn == GMTeste.PlayerType.Human)
                    {
                        player1PowerUsed = true;
                    }
                    else if (gameManager.currentTurn == GMTeste.PlayerType.Human2)
                    {
                        player2PowerUsed = true;
                    }

                    // Switch turn to the opponent
                    gameManager.ChangeTurn();

                    // End overwrite mode
                    isOverwriteActive = false;
                }
                else
                {
                    Debug.Log("Invalid cube selected. You can only overwrite an opponent's cube.");
                }
            }
        }
    }

    // Helper function to check if the selected cube belongs to the opponent
    private bool IsValidCubeToOverwrite(GameObject selectedCube)
    {
        if (gameManager.currentTurn == GMTeste.PlayerType.Human)
        {
            // Player 1 can only overwrite Player 2's cubes (which are aiColor)
            return selectedCube.GetComponent<Renderer>().material.color == victoryCheck.aiColor;
        }
        else if (gameManager.currentTurn == GMTeste.PlayerType.Human2)
        {
            // Player 2 can only overwrite Player 1's cubes (which are humanColor)
            return selectedCube.GetComponent<Renderer>().material.color == victoryCheck.humanColor;
        }

        return false;
    }

    // Helper function to overwrite the selected cube with the current player's color
    private void OverwriteSelectedCube(GameObject selectedCube)
    {
        if (gameManager.currentTurn == GMTeste.PlayerType.Human)
        {
            selectedCube.GetComponent<Renderer>().material.color = victoryCheck.humanColor;  // Player 1's color
            Debug.Log("Player 1 has overwritten a cube.");
        }
        else if (gameManager.currentTurn == GMTeste.PlayerType.Human2)
        {
            selectedCube.GetComponent<Renderer>().material.color = victoryCheck.aiColor;  // Player 2's color
            Debug.Log("Player 2 has overwritten a cube.");
        }
    }
}
