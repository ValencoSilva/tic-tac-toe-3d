using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierPower : MonoBehaviour
{
    public GameObject[] allCubes;              // Array of all cubes in the game board
    public GlobalPowerLimit globalPowerLimit; // Reference to the global power limit
    public GMTeste gameManager;               // Reference to the game manager
    public ScoreManager scoreManager;         // Reference to the score manager
    public Color highlightColor = new Color(1f, 0.5f, 0f); // RGB for orange (R: 1, G: 0.5, B: 0)

    public int powerCost = 20;                // Cost of using the power
    private bool isHighlightingActive = false; // Flag to track if the power is active

    public void ActivateHighlightPlayedCubePower()
    {
        // Check if the player can afford the power
        if (!scoreManager.CanAffordPower(powerCost, gameManager.currentTurn))
        {
            Debug.Log("Not enough points to activate the power.");
            return;
        }

        // Check global power limit
        if (!globalPowerLimit.CanUsePower(gameManager.currentTurn))
        {
            Debug.Log("Power limit reached for " + gameManager.currentTurn.ToString());
            globalPowerLimit.DisplayWarning();
            return;
        }

        // Enable highlighting mode
        isHighlightingActive = true;
        Debug.Log("Highlight Played Cube Power Activated. Click a cube to highlight.");
    }

    private void Update()
    {
        if (isHighlightingActive && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedCube = hit.transform.gameObject;

                // Check if the clicked object is a played cube (not white)
                if (IsPlayedCube(clickedCube))
                {
                    HighlightCube(clickedCube);
                }
                else
                {
                    Debug.Log("Selected cube has not been played. Please choose a cube with a color other than white.");
                }
            }
        }
    }

    private bool IsPlayedCube(GameObject cube)
    {
        // A cube is considered "played" if its color is not white
        return cube != null && cube.activeSelf && cube.GetComponent<Renderer>().material.color != Color.white;
    }

    private void HighlightCube(GameObject cube)
    {
        cube.GetComponent<Renderer>().material.color = highlightColor;
        Debug.Log("Highlighted Played Cube: " + System.Array.IndexOf(allCubes, cube));

        // Deduct the cost of the power from the player's score
        scoreManager.DeductPoints(powerCost, gameManager.currentTurn);

        // Mark the power as used for the current player
        globalPowerLimit.UsePower(gameManager.currentTurn);

        // Disable highlighting mode
        isHighlightingActive = false;
    }
}
