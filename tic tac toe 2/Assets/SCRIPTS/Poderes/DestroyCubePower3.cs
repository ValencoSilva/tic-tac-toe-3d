using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCubePower3 : MonoBehaviour
{
    public VictoryCheckTeste victoryCheckScript; // Reference to the VictoryCheck script
    public GameObject[] allCubes;               // Array of all cubes in the game board
    public GameObject[] relatedSquares;         // Array of all related squares on the board
    public GlobalPowerLimit globalPowerLimit;   // Reference to the global power limit
    public GMTeste gameManager;                 // Reference to the game manager
    public ScoreManager scoreManager;           // Reference to the scoring system

    public int powerCost = 50; // Cost of activating this power

    public void ActivateDestroyAnyCubePower()
    {
        // Check if the player has enough points to activate the power
        if (!scoreManager.CanAffordPower(powerCost, gameManager.currentTurn))
        {
            Debug.Log("Not enough points to activate Destroy Any Cube Power.");
            return;
        }

        // Check if the current player can still use a power
        if (!globalPowerLimit.CanUsePower(gameManager.currentTurn))
        {
            Debug.Log("Power limit reached for " + gameManager.currentTurn.ToString());
            globalPowerLimit.DisplayWarning();
            return;
        }

        // Deduct the points for activating the power
        scoreManager.DeductPoints(powerCost, gameManager.currentTurn);

        StartCoroutine(SelectAndDestroyAnyCube());

        // Mark the power as used for the current player
        globalPowerLimit.UsePower(gameManager.currentTurn);
    }

    private IEnumerator SelectAndDestroyAnyCube()
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

        // Filter out cubes that are already inactive
        List<GameObject> activeCubes = new List<GameObject>();
        foreach (GameObject cube in allCubes)
        {
            if (cube != null && cube.activeSelf)
            {
                activeCubes.Add(cube);
            }
        }

        // Ensure there are active cubes to destroy
        if (activeCubes.Count == 0)
        {
            Debug.LogWarning("No active cubes left to destroy.");
            yield break;
        }

        // Randomly select an active cube to destroy
        GameObject randomCube = activeCubes[Random.Range(0, activeCubes.Count)];
        int cubeIndex = System.Array.IndexOf(allCubes, randomCube); // Get the index of the selected cube

        if (randomCube == null || cubeIndex < 0)
        {
            Debug.LogError("Selected cube is null or index is invalid.");
            yield break;
        }

        // Mark the cube as "destroyed" in the VictoryCheck system
        Debug.Log("Destroying Cube " + cubeIndex + " (Played or Unplayed)");
        victoryCheckScript.MarkCubeAsDestroyed(randomCube); // Ensure this method exists in VictoryCheckTeste

        // Set the cube and its corresponding square as inactive
        randomCube.GetComponent<Renderer>().material.color = Color.white; // Reset to neutral color if played
        randomCube.SetActive(false); // Hide or "destroy" the cube

        GameObject relatedSquare = relatedSquares[cubeIndex];
        if (relatedSquare != null)
        {
            relatedSquare.SetActive(false);
            Debug.Log("Destroying related Square " + cubeIndex);
        }

        yield return null;
    }
}
