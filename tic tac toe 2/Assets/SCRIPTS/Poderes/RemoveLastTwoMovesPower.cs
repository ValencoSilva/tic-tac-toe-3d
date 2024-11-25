using System.Collections;
using UnityEngine;

public class RemoveLastTwoMovesPower : MonoBehaviour
{
    public GMTeste gameManager;               // Reference to GMTeste
    public GlobalPowerLimit globalPowerLimit; // Reference to the global power limit

    public void ActivateRemoveLastTwoMovesPower()
    {
        if (!globalPowerLimit.CanUsePower(gameManager.currentTurn))
        {
            Debug.Log("Power limit reached for " + gameManager.currentTurn.ToString());
            globalPowerLimit.DisplayWarning();
            return;
        }

        if (gameManager.lastPlayedCubes.Count == 0)
        {
            Debug.LogWarning("No moves to remove. Queue is empty.");
            return;
        }

        StartCoroutine(RemoveLastTwoMoves());
    }

    private IEnumerator RemoveLastTwoMoves()
    {
        foreach (GameObject cube in gameManager.lastPlayedCubes)
        {
            if (cube != null)
            {
                Debug.Log($"Resetting cube at position {System.Array.IndexOf(gameManager.clickableObjects, cube)}");

                cube.GetComponent<Renderer>().material.color = Color.white; // Reset color to unplayed
                cube.SetActive(true); // Ensure the cube is active if needed
            }
        }

        gameManager.lastPlayedCubes.Clear(); // Clear the tracked moves after resetting
        globalPowerLimit.UsePower(gameManager.currentTurn);

        yield return null;
    }
}
