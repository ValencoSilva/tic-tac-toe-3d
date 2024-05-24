using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class TesteHardMode : MonoBehaviour
{
    public enum PlayerType { Human, AI }
    public PlayerType currentTurn = PlayerType.Human;
    public VictoryCheck ScriptA;
    public GameObject[] clickableObjects; // Array of game objects that can be clicked on.

    private bool isProcessingAI = false;

    public void Start()
    {
        ScriptA = GameObject.FindObjectOfType<VictoryCheck>();
    }

    private void Update()
    {
        if (currentTurn == PlayerType.Human && !ScriptA.IsGameOver())
        {
            CheckForObjectClick();
        }

        if (currentTurn == PlayerType.AI && !isProcessingAI && !ScriptA.IsGameOver())
        {
            StartCoroutine(AIDelayedTurn());
        }
    }

    IEnumerator AIDelayedTurn()
    {
        isProcessingAI = true;
        yield return new WaitForSeconds(1.5f);  // Delay AI turn by 1.5 seconds

        if (ScriptA.IsGameOver())
        {
            isProcessingAI = false;
            Debug.Log("Game over. No AI moves allowed.");
            yield break;
        }

        AI_HardTurn();

        if (ScriptA.IsGameOver())
        {
            isProcessingAI = false;
            Debug.Log("Game over after AI decision. No further moves allowed.");
            yield break;
        }

        isProcessingAI = false;
    }

    void CheckForObjectClick()
    {
        ScriptA.CheckAllWinningConditions();
        if (ScriptA.winner != VictoryCheck.Winner.None || ScriptA.IsDraw())
        {
            return;  // Exit the method to prevent further interaction
        }

        if (Input.GetMouseButtonDown(0) && currentTurn == PlayerType.Human)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                foreach (GameObject obj in clickableObjects)
                {
                    if (hit.transform.gameObject == obj && obj.GetComponent<Renderer>().material.color == Color.white)
                    {
                        obj.GetComponent<Renderer>().material.color = Color.red; // Human's color
                        ChangeTurn();
                        ScriptA.CheckAllWinningConditions();
                        break;
                    }
                }
            }
        }
    }

    void AI_HardTurn()
    {
        if (ScriptA.IsGameOver())
        {
            Debug.Log("Game over. Exiting AI turn.");
            return;  // Do not proceed if the game is over
        }

        if (IsFirstMove())
        {
            PlayStrategicMove();
            return;
        }

        if (ScriptA.winner != VictoryCheck.Winner.None || ScriptA.IsDraw())
        {
            return;  // Exit the method to prevent further AI interaction
        }

        ScriptA.CheckAllWinningConditions();
        
        Dictionary<GameObject, int> moveScores = EvaluateBoard();
        if (moveScores.Count > 0)
        {
            var bestMove = moveScores.OrderByDescending(kvp => kvp.Value).First().Key;
            bestMove.GetComponent<Renderer>().material.color = Color.blue;
            ChangeTurn();
            ScriptA.CheckAllWinningConditions();
        }
    }

    bool IsFirstMove()
    {
        // Checks if all cubes are white, meaning no moves have been made
        return clickableObjects.All(obj => obj.GetComponent<Renderer>().material.color == Color.white);
    }

    void PlayRandomMove()
    {
        List<GameObject> availableSpots = clickableObjects.Where(obj => obj.GetComponent<Renderer>().material.color == Color.white).ToList();
        if (availableSpots.Count > 0)
        {
            GameObject randomSpot = availableSpots[Random.Range(0, availableSpots.Count)];
            randomSpot.GetComponent<Renderer>().material.color = Color.blue;
            ChangeTurn();
        }
    }

    int[] strategicPositions = { 21, 22, 25, 26 }; // Example positions, choose based on your game's strategy

    void PlayStrategicMove()
    {
        List<GameObject> strategicSpots = new List<GameObject>();
        foreach (int pos in strategicPositions)
        {
            if (clickableObjects[pos].GetComponent<Renderer>().material.color == Color.white)
            {
                strategicSpots.Add(clickableObjects[pos]);
            }
        }

        if (strategicSpots.Count > 0)
        {
            GameObject strategicSpot = strategicSpots[Random.Range(0, strategicSpots.Count)];
            strategicSpot.GetComponent<Renderer>().material.color = Color.blue;
            ChangeTurn();
        }
        else
        {
            PlayRandomMove();
        }
    }

    Dictionary<GameObject, int> EvaluateBoard()
    {
        var moveScores = new Dictionary<GameObject, int>();

        foreach (GameObject spot in clickableObjects)
        {
            if (spot.GetComponent<Renderer>().material.color == Color.white) // Only evaluate unoccupied spots
            {
                spot.GetComponent<Renderer>().material.color = Color.blue; // AI's color
                int maxScore = int.MinValue;

                for (int i = 0; i < 64; i++)
                {
                    if (i % 4 < 1) maxScore = Mathf.Max(maxScore, EvaluateLine(i, i + 1, i + 2, i + 3));
                    if (i % 16 < 4) maxScore = Mathf.Max(maxScore, EvaluateLine(i, i + 4, i + 8, i + 12));
                    if (i % 16 == 0) maxScore = Mathf.Max(maxScore, EvaluateLine(i, i + 5, i + 10, i + 15));
                    if (i % 16 == 3) maxScore = Mathf.Max(maxScore, EvaluateLine(i, i + 3, i + 6, i + 9));
                    if (i < 16) maxScore = Mathf.Max(maxScore, EvaluateLine(i, i + 16, i + 32, i + 48));
                }

                for (int col = 0; col < 4; col++)
                {
                    maxScore = Mathf.Max(maxScore, EvaluateLine(col, 20 + col, 40 + col, 60 + col));
                    maxScore = Mathf.Max(maxScore, EvaluateLine(12 + col, 24 + col, 36 + col, 48 + col));
                }

                maxScore = Mathf.Max(maxScore, EvaluateLine(0, 17, 34, 51));
                maxScore = Mathf.Max(maxScore, EvaluateLine(4, 21, 38, 55));
                maxScore = Mathf.Max(maxScore, EvaluateLine(8, 25, 42, 59));
                maxScore = Mathf.Max(maxScore, EvaluateLine(12, 29, 46, 63));
                maxScore = Mathf.Max(maxScore, EvaluateLine(3, 18, 33, 48));
                maxScore = Mathf.Max(maxScore, EvaluateLine(7, 22, 37, 52));
                maxScore = Mathf.Max(maxScore, EvaluateLine(11, 26, 41, 56));
                maxScore = Mathf.Max(maxScore, EvaluateLine(15, 30, 45, 60));
                maxScore = Mathf.Max(maxScore, EvaluateLine(0, 21, 42, 63));
                maxScore = Mathf.Max(maxScore, EvaluateLine(3, 22, 41, 60));
                maxScore = Mathf.Max(maxScore, EvaluateLine(12, 25, 38, 51));
                maxScore = Mathf.Max(maxScore, EvaluateLine(15, 26, 37, 48));

                spot.GetComponent<Renderer>().material.color = Color.white; // Undo move
                moveScores[spot] = maxScore;
            }
        }
        return moveScores;
    }

    int EvaluateLine(params int[] indices)
    {
        int aiCount = 0;
        int humanCount = 0;

        foreach (int index in indices)
        {
            Color color = clickableObjects[index].GetComponent<Renderer>().material.color;
            if (color == Color.blue) // AI's color
                aiCount++;
            else if (color == Color.red) // Human's color
                humanCount++;
        }

        if (aiCount > 0 && humanCount > 0) return 0; // Mixed line, no potential
        if (aiCount == 4) return 100; // AI wins
        if (humanCount == 4) return -99; // Human wins
        if (aiCount == 3) return 50; // AI is one move away from winning
        if (humanCount == 3) return -70; // Human is one move away from winning
        if (aiCount == 2) return 10; // Two in a line for AI
        if (humanCount == 2) return -11; // Two in a line for Human

        return 0;
    }

    void ChangeTurn()
    {
        ScriptA.CheckAllWinningConditions();
        currentTurn = currentTurn == PlayerType.Human ? PlayerType.AI : PlayerType.Human;
        ScriptA.CheckAllWinningConditions();
    }
}
