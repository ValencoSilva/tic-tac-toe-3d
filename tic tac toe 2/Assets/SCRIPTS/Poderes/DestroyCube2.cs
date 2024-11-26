using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube2 : MonoBehaviour
{
    public VictoryCheckTeste victoryCheckScript; // Reference to the VictoryCheck script
    public GameObject[] allCubes;               // Array of all cubes in the game board
    public GameObject[] relatedSquares;         // Array of all related squares on the board
    public GlobalPowerLimit globalPowerLimit;   // Reference to the global power limit
    public GMTeste gameManager;                 // Reference to the game manager
    public ScoreManager scoreManager;           // Reference to the score manager

    public int powerCost = 30;                  // Cost of using the power

    public void ActivateDestroyPlayedCubePower()
    {
        // Check if the player has enough points
        if (!scoreManager.CanAffordPower(powerCost, gameManager.currentTurn))
        {
            Debug.Log("Not enough points to activate Destroy Cube power.");
            return;
        }

        // Check if the power can be used within the global power limit
        if (!globalPowerLimit.CanUsePower(gameManager.currentTurn))
        {
            Debug.Log("Power limit reached for " + gameManager.currentTurn.ToString());
            globalPowerLimit.DisplayWarning();
            return;
        }

        StartCoroutine(SelectAndDestroyPlayedCube());
    }

    private IEnumerator SelectAndDestroyPlayedCube()
    {
        if (allCubes == null || allCubes.Length == 0)
        {
            Debug.LogError("allCubes array is not assigned or empty.");
            yield break;
        }

        if (relatedSquares == null || relatedSquares.Length != allCubes.Length)
        {
            Debug.LogError("relatedSquares array is not assigned or does not match the length of allCubes.");
            yield break;
        }

        // Filter cubes to only include those that have been played (not white)
        List<GameObject> playedCubes = new List<GameObject>();
        foreach (GameObject cube in allCubes)
        {
            if (cube != null && cube.activeSelf && cube.GetComponent<Renderer>().material.color != Color.white)
            {
                playedCubes.Add(cube);
            }
        }

        // Ensure there are played cubes to destroy
        if (playedCubes.Count == 0)
        {
            Debug.LogWarning("No played cubes left to destroy.");
            yield break;
        }

        // Randomly select a played cube to destroy
        GameObject randomCube = playedCubes[Random.Range(0, playedCubes.Count)];
        int cubeIndex = System.Array.IndexOf(allCubes, randomCube); // Get the index of the selected cube

        if (randomCube == null || cubeIndex < 0)
        {
            Debug.LogError("Selected cube is null or index is invalid.");
            yield break;
        }

        // Mark the cube as "destroyed" in the VictoryCheck system
        Debug.Log("Destroying Played Cube " + cubeIndex);
        victoryCheckScript.MarkCubeAsDestroyed(randomCube); // Ensure this method exists in VictoryCheckTeste

        // Set the cube and its corresponding square as inactive
        randomCube.GetComponent<Renderer>().material.color = Color.white; // Reset to neutral color
        randomCube.SetActive(false); // Hide or "destroy" the cube

        GameObject relatedSquare = relatedSquares[cubeIndex];
        if (relatedSquare != null)
        {
            relatedSquare.SetActive(false);
            Debug.Log("Destroying related Square " + cubeIndex);
        }

        // Deduct the cost of the power from the player's score
        scoreManager.DeductPoints(powerCost, gameManager.currentTurn);

        // Mark the power as used for the current player
        globalPowerLimit.UsePower(gameManager.currentTurn);

        yield return null;
    }
}
