using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JogadaAleatoria : MonoBehaviour
{
    private VictoryCheckTeste ScriptA; // Reference to the victory check system
    public GameObject[] clickableObjects; // Assign clickableObjects in Unity Inspector
    public GMTeste gameManager;  // Reference to your game manager script (GMTeste)
    public GlobalPowerLimit globalPowerLimit;  // Reference to GlobalPowerLimit script

    public GMTeste.PlayerType currentPlayer; // Reference to know whose turn it is

    void Start()
    {
        // Find the VictoryCheckTeste script in the scene
        ScriptA = GameObject.FindObjectOfType<VictoryCheckTeste>();

        // Ensure clickableObjects are assigned, otherwise log an error
        if (ScriptA == null)
        {
            Debug.LogError("VictoryCheckTeste script not found. Make sure it is assigned in the scene.");
        }

        if (clickableObjects == null || clickableObjects.Length == 0)
        {
            Debug.LogError("Clickable objects are not assigned. Please assign them in the Inspector.");
        }
    }

    public void ExecuteEasyAITurn()
    {
        // Get the list of available (unoccupied) spots
        List<GameObject> availableSpots = clickableObjects.Where(obj => obj.GetComponent<Renderer>().material.color == Color.white).ToList();
        if (availableSpots.Count > 0)
        {
            GameObject randomSpot = availableSpots[Random.Range(0, availableSpots.Count)];

            // Apply the correct color based on the current player
            if (currentPlayer == GMTeste.PlayerType.Human)
            {
                randomSpot.GetComponent<Renderer>().material.color = ScriptA.humanColor; // Player 1's color
            }
            else if (currentPlayer == GMTeste.PlayerType.Human2)
            {
                randomSpot.GetComponent<Renderer>().material.color = ScriptA.aiColor; // Player 2's color
            }

            Debug.Log("AI move made by power.");
        }
        else
        {
            Debug.Log("No available spots left.");
        }
    }

    public void OnPoder1(GMTeste.PlayerType player)
    {
        // Check if the player can use their power based on the global power limit
        if (!globalPowerLimit.CanUsePower(player))
        {
            Debug.Log("Power limit reached for " + player.ToString());
            globalPowerLimit.DisplayWarning();
            return;  // Exit if the player has already used their power
        }

        // Check whose turn it is and if their power has been used
        if (player == GMTeste.PlayerType.Human)
        {
            // Human (Player 1) uses the power
            Debug.Log("Human (Player 1) activated their power.");
            currentPlayer = GMTeste.PlayerType.Human; // Set current player to Human
            ExecuteEasyAITurn();
        }
        else if (player == GMTeste.PlayerType.Human2)
        {
            // Human2 (Player 2) uses the power
            Debug.Log("Human2 (Player 2) activated their power.");
            currentPlayer = GMTeste.PlayerType.Human2; // Set current player to Human2
            ExecuteEasyAITurn();
        }

        // Mark the power as used for the current player
        globalPowerLimit.UsePower(player);
    }
}
