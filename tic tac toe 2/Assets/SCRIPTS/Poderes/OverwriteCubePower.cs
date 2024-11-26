using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverwriteCubePower : MonoBehaviour
{
    public GMTeste gameManager;  // Reference to the game manager script
    public int maxRounds = 3;    // Restriction to use the power within the first 3 rounds
    public int powerCost = 100;  // Cost to use this power
    public GlobalPowerLimit globalPowerLimit; // Reference to the global power limit
    public ScoreManager scoreManager;         // Reference to the ScoreManager for managing points

    public void ActivateOverridePower()
    {
        // Check if the power is within the allowed round limit
        if (gameManager.moveLog.Count / 2 >= maxRounds)
        {
            Debug.LogWarning("Cannot use Override Power after round " + maxRounds);
            return;
        }

        // Check if the player has enough points
        if (!scoreManager.CanAffordPower(powerCost, gameManager.currentTurn))
        {
            Debug.LogWarning("Player " + gameManager.currentTurn + " does not have enough points to activate this power.");
            return;
        }

        // Check if the player has exceeded the global power limit
        if (!globalPowerLimit.CanUsePower(gameManager.currentTurn))
        {
            Debug.LogWarning("Power limit reached for " + gameManager.currentTurn.ToString());
            globalPowerLimit.DisplayWarning();
            return;
        }

        // Deduct points for using the power
        scoreManager.DeductPoints(powerCost, gameManager.currentTurn);

        // Activate the power: Allow the player to choose an opponent's cube
        StartCoroutine(SelectAndOverrideCube());
    }

    private IEnumerator SelectAndOverrideCube()
    {
        Debug.Log("Select an opponent's cube to override. Waiting for input...");

        bool cubeSelected = false;
        while (!cubeSelected)
        {
            if (Input.GetMouseButtonDown(0)) // Detect mouse click
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject selectedCube = hit.transform.gameObject;

                    // Ensure the selected cube belongs to the opponent
                    Color opponentColor = gameManager.currentTurn == GMTeste.PlayerType.Human
                        ? gameManager.ScriptA.aiColor
                        : gameManager.ScriptA.humanColor;

                    if (selectedCube.GetComponent<Renderer>().material.color == opponentColor)
                    {
                        // Change the color of the cube to the current player's color
                        selectedCube.GetComponent<Renderer>().material.color = gameManager.currentTurn == GMTeste.PlayerType.Human
                            ? gameManager.ScriptA.humanColor
                            : gameManager.ScriptA.aiColor;

                        Debug.Log("Cube overridden at position: " + selectedCube.transform.position);

                        // Log the override move
                        gameManager.LogMove(gameManager.currentTurn, selectedCube);

                        cubeSelected = true;

                        // Deduct power usage
                        globalPowerLimit.UsePower(gameManager.currentTurn);
                    }
                    else
                    {
                        Debug.LogWarning("You can only override an opponent's cube!");
                    }
                }
            }

            yield return null; // Wait for the next frame
        }
    }
}
