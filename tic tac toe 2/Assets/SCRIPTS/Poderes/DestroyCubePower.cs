using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCubePower : MonoBehaviour
{
    public VictoryCheckTeste victoryCheckScript;  // Reference to the VictoryCheck script
    public GameObject[] allCubes;                 // Array of all cubes in the game board
    public GameObject[] relatedSquares;           // Array of all related squares on the board
    public GMTeste gameManager;                   // Reference to the game manager for turn info
    public GlobalPowerLimit globalPowerLimit;     // Reference to the global power limit

    public void ActivateDestroyCubePower()
    {
        // Check if the current player can still use a power
        if (!globalPowerLimit.CanUsePower(gameManager.currentTurn))
        {
            Debug.Log("Power limit reached for " + gameManager.currentTurn.ToString());
            globalPowerLimit.DisplayWarning();
            return;
        }

        StartCoroutine(SelectAndDestroyRandomCube());

        // Mark that the current player has used their power
        globalPowerLimit.UsePower(gameManager.currentTurn);
    }

    private IEnumerator SelectAndDestroyRandomCube()
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

        // Filter out already destroyed or inactive cubes
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
        int cubeIndex = System.Array.IndexOf(allCubes, randomCube);  // Get the index of the selected cube

        if (randomCube == null || cubeIndex < 0)
        {
            Debug.LogError("Selected cube is null or index is invalid.");
            yield break;
        }

        // Mark the cube as "destroyed" in the VictoryCheck system
        Debug.Log("Destroying Cube " + cubeIndex);
        victoryCheckScript.MarkCubeAsDestroyed(randomCube);  // Ensure this method exists in VictoryCheckTeste

        // Set the cube and its corresponding square as inactive
        randomCube.SetActive(false);
        GameObject relatedSquare = relatedSquares[cubeIndex];
        if (relatedSquare != null)
        {
            relatedSquare.SetActive(false);
            Debug.Log("Destroying related Square " + cubeIndex);
        }

        yield return null;
    }
}
