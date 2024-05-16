using UnityEngine;
using System.Collections;

public class HardMode : MonoBehaviour
{
    public enum PlayerType { Human, AI }
    public PlayerType currentTurn = PlayerType.Human;
    public VictoryCheck ScriptA;
    public GameObject[] clickableObjects; // Array of game objects that can be clicked on.

    public void Start()
    {
       ScriptA = GameObject.FindObjectOfType<VictoryCheck>();
    }
    

    private void Update()
    {
        CheckForObjectClick();

        if (currentTurn == PlayerType.AI && !isProcessingAI)
        {
        StartCoroutine(AIDelayedTurn());
        }
    }


    bool isProcessingAI = false;

    IEnumerator AIDelayedTurn()
    {
    isProcessingAI = true;
    yield return new WaitForSeconds(1.0f);  // Delay AI turn by 1 second, adjust as needed

    // Now perform the AI's turn
    AI_HardTurn();
    isProcessingAI = false;
    }


    void CheckForObjectClick()
    {
        if (ScriptA.winner != VictoryCheck.Winner.None || ScriptA.IsDraw())
        {
            Debug.Log("Game over. No more moves allowed.");
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
        ScriptA.CheckAllWinningConditions();
        if (ScriptA.winner != VictoryCheck.Winner.None || ScriptA.IsDraw())
        {
        Debug.Log("Game over. No AI moves allowed.");
        return;  // Exit the method to prevent further AI interaction
        }
        int bestScore = int.MinValue;
        GameObject bestMove = null;

        foreach (GameObject spot in clickableObjects)
        {
            if (spot.GetComponent<Renderer>().material.color == Color.white) // Check only unoccupied spots
            {
                spot.GetComponent<Renderer>().material.color = Color.blue; // AI's color
                int score = EvaluateBoard();
                spot.GetComponent<Renderer>().material.color = Color.white; // Undo move
                Debug.Log(score);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = spot;
                }
            }
        }

        if (bestMove != null && ScriptA.winner == VictoryCheck.Winner.None)
        {
            bestMove.GetComponent<Renderer>().material.color = Color.blue;
            ChangeTurn();
        }
    }

    int EvaluateBoard()
    {
    int score = 0;

    // Evaluate every potential winning line
    for (int i = 0; i < 64; i++)
    {
        if (i % 4 < 1) score += EvaluateLine(i, i + 1, i + 2, i + 3);
        if (i % 16 < 4) score += EvaluateLine(i, i + 4, i + 8, i + 12);
        if (i % 16 == 0) score += EvaluateLine(i, i + 5, i + 10, i + 15);
        if (i % 16 == 3) score += EvaluateLine(i, i + 3, i + 6, i + 9);
        if (i < 16) score += EvaluateLine(i, i + 16, i + 32, i + 48);
    }

    for (int col = 0; col < 4; col++)
        {
           score += EvaluateLine(col, 20 + col, 40 + col, 60 + col); 
           score += EvaluateLine(12 + col, 24 + col, 36 + col, 48 + col); 

           score += EvaluateLine(3 + col, 18 + col, 33 + col, 48 + col); 
           score += EvaluateLine(15 + col, 26 + col, 37 + col, 48 + col); 
        }

    // Diagonal checks across layers
    score += EvaluateLine(0, 17, 34, 51);
    score += EvaluateLine(4, 21, 38, 55);
    score += EvaluateLine(8, 25, 42, 59);
    score += EvaluateLine(12, 29, 46, 63);

    // Check 3D diagonals
    score += EvaluateLine(0, 21, 42, 63);
    score += EvaluateLine(3, 22, 41, 60);
    score += EvaluateLine(12, 25, 38, 51);
    score += EvaluateLine(15, 26, 37, 48);

    return score;
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
    if (humanCount == 4) return -100; // Human wins
    if (aiCount == 3) return 50; // AI is one move away from winning
    if (humanCount == 3) return -50; // Human is one move away from winning
    if (aiCount == 2) return 10; // Two in a line for AI
    if (humanCount == 2) return -10; // Two in a line for Human

    return 0;
    }


    void ChangeTurn()
    {
        currentTurn = currentTurn == PlayerType.Human ? PlayerType.AI : PlayerType.Human;
    }
}
