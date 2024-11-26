using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPower : MonoBehaviour
{
    public GameObject[] allCubes;                     // Array of all cubes in the game board
    public GlobalPowerLimit globalPowerLimit;         // Reference to the global power limit
    public GMTeste gameManager;                      // Reference to the game manager
    public VictoryCheckTeste victoryCheckScript;     // Reference to the victory check script
    public ScoreManager scoreManager;                // Reference to the scoring system

    public Color recommendationColor = new Color(0.5f, 0f, 0.5f); // Purple color for recommendation
    public int powerCost = 50;                       // Cost in points to activate the power
    private GameObject recommendedCube;              // Store the recommended cube

    public void ActivateRecommendationPower()
    {
        // Check if the player has enough points
        if (!scoreManager.CanAffordPower(powerCost, gameManager.currentTurn))
        {
            Debug.LogWarning("Not enough points to activate AI Recommendation Power.");
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

        // Reset the previous recommendation if it exists
        if (recommendedCube != null)
        {
            ResetRecommendedCube();
        }

        recommendedCube = GetRecommendedMove();
        if (recommendedCube != null)
        {
            HighlightRecommendedCube(recommendedCube);
            globalPowerLimit.UsePower(gameManager.currentTurn);
        }
        else
        {
            Debug.Log("No valid moves available for recommendation.");
        }
    }

    private GameObject GetRecommendedMove()
    {
        // Step 1: Check for winning moves (simulation without actual impact)
        GameObject winningMove = FindWinningMove(gameManager.currentTurn);
        if (winningMove != null)
        {
            Debug.Log("AI recommends a winning move.");
            return winningMove;
        }

        // Step 2: Check for blocking moves
        GMTeste.PlayerType opponent = gameManager.currentTurn == GMTeste.PlayerType.Human ? GMTeste.PlayerType.Human2 : GMTeste.PlayerType.Human;
        GameObject blockingMove = FindWinningMove(opponent);
        if (blockingMove != null)
        {
            Debug.Log("AI recommends blocking the opponent.");
            return blockingMove;
        }

        // Step 3: Fallback to a random valid move
        List<GameObject> unplayedCubes = new List<GameObject>();
        foreach (GameObject cube in allCubes)
        {
            if (cube != null && cube.activeSelf && cube.GetComponent<Renderer>().material.color == Color.white)
            {
                unplayedCubes.Add(cube);
            }
        }

        if (unplayedCubes.Count > 0)
        {
            return unplayedCubes[Random.Range(0, unplayedCubes.Count)];
        }

        return null; // No valid moves available
    }

    private GameObject FindWinningMove(GMTeste.PlayerType player)
    {
        Color playerColor = player == GMTeste.PlayerType.Human ? victoryCheckScript.humanColor : victoryCheckScript.aiColor;

        foreach (GameObject cube in allCubes)
        {
            if (cube != null && cube.activeSelf && cube.GetComponent<Renderer>().material.color == Color.white)
            {
                // Simulate the move by temporarily storing the state without modifying the game logic
                bool wouldWin = SimulateWinningMove(cube, playerColor);

                if (wouldWin)
                {
                    return cube; // Return the winning move
                }
            }
        }

        return null; // No winning move found
    }

    private bool SimulateWinningMove(GameObject cube, Color playerColor)
    {
        // Temporarily store the original color
        Color originalColor = cube.GetComponent<Renderer>().material.color;

        // Simulate the move
        cube.GetComponent<Renderer>().material.color = playerColor;
        bool wouldWin = victoryCheckScript.UpdateAndCheckGameEnd();

        // Revert to the original color
        cube.GetComponent<Renderer>().material.color = originalColor;

        return wouldWin;
    }

    private void HighlightRecommendedCube(GameObject cube)
    {
        cube.GetComponent<Renderer>().material.color = recommendationColor;
        Debug.Log("AI Recommended Cube: " + System.Array.IndexOf(allCubes, cube));
        StartCoroutine(ResetRecommendedCubeAfterDelay(3f)); // Reset after 3 seconds
    }

    private IEnumerator ResetRecommendedCubeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetRecommendedCube();
    }

    private void ResetRecommendedCube()
    {
        if (recommendedCube != null)
        {
            recommendedCube.GetComponent<Renderer>().material.color = Color.white;
            Debug.Log("Recommendation reset for Cube: " + System.Array.IndexOf(allCubes, recommendedCube));
            recommendedCube = null;
        }
    }
}
