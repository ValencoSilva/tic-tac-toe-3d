using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class GM2Nivel3 : MonoBehaviour
{
    public enum PlayerType { Human, AI }
    public PlayerType currentTurn = PlayerType.Human;
    public VictoryCheckAI ScriptA;
    public GameObject[] clickableObjects; // Array of game objects that can be clicked on.
    [SerializeField] private GameObject painelGameStarter;
    [SerializeField] private GameObject Quadrados;
    [SerializeField] private GameObject Cubos;
    [SerializeField] private GameObject resetButton;
    [SerializeField] private GameObject Sinalizacao;

    public Text logText; // Reference to the UI text element to display the log
    private List<string> moveLog = new List<string>(); // List to store the log of moves
    
    [SerializeField] private GameObject panelLog;

    public void Start()
    {
       ScriptA = GameObject.FindObjectOfType<VictoryCheckAI>();
    }
    

    private void Update()
    {
    //ScriptA.CheckAllWinningConditions();
    if (currentTurn == PlayerType.Human && !ScriptA.IsGameOver())
    {
        CheckForObjectClick();
    }
    

    // Only start the coroutine if it's the AI's turn and the AI is not already processing
    if (currentTurn == PlayerType.AI && !isProcessingAI && !ScriptA.IsGameOver())
    {
        StartCoroutine(AIDelayedTurn());
    }
    }


    bool isProcessingAI = false;

    IEnumerator AIDelayedTurn()
    {
    isProcessingAI = true;
    yield return new WaitForSeconds(1.5f);  // Delay AI turn by 1 second

    // Check if the game is over before making a move
    if (ScriptA.IsGameOver())
    {
        isProcessingAI = false;
        Debug.Log("Game over. No AI moves allowed.");
        yield break;  // Exit the coroutine early if the game is over
    }

    // Now perform the AI's turn
    AI_HardTurn();

    // Check if the game ended after AI's decision
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
        if (ScriptA.winner != VictoryCheckAI.Winner.None || ScriptA.IsDraw())
        {
            //Debug.Log("Game over. No more moves allowed.");
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
                        obj.GetComponent<Renderer>().material.color = ScriptA.humanColor; // Human's color
                        LogMove(currentTurn, obj);
                        ChangeTurn();
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


        if (ScriptA.winner != VictoryCheckAI.Winner.None || ScriptA.IsDraw())
        {
        //Debug.Log("Game over. No AI moves allowed.");
        return;  // Exit the method to prevent further AI interaction
        }

        ScriptA.CheckAllWinningConditions();
        int bestScore = int.MinValue;
        GameObject bestMove = null;

        foreach (GameObject spot in clickableObjects)
        {
            if (spot.GetComponent<Renderer>().material.color == Color.white) // Check only unoccupied spots
            {
                spot.GetComponent<Renderer>().material.color = ScriptA.aiColor; // AI's color
                int score = EvaluateBoard();
                spot.GetComponent<Renderer>().material.color = Color.white; // Undo move
                //Debug.Log(score);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = spot;
                }
            }
        }

        if (bestMove != null && ScriptA.winner == VictoryCheckAI.Winner.None)
        {
            bestMove.GetComponent<Renderer>().material.color = ScriptA.aiColor;
            LogMove(currentTurn, bestMove);
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
            randomSpot.GetComponent<Renderer>().material.color = ScriptA.aiColor;
            LogMove(currentTurn, randomSpot); // Log the AI's random move
        }
    }

int[] strategicPositions = { 21, 22, 25, 26, 37, 38, 41, 42 }; // Example positions, choose based on your game's strategy

    void PlayStrategicMove()
    {
    List<GameObject> strategicSpots = new List<GameObject>();
    // Fill the list only with strategic positions that are still available (assumed color white is available)
    foreach (int pos in strategicPositions)
    {
        if (clickableObjects[pos].GetComponent<Renderer>().material.color == Color.white)
        {
            strategicSpots.Add(clickableObjects[pos]);
        }
    }

    if (strategicSpots.Count > 0)
    {
        // Select a random strategic spot from those available
        GameObject strategicSpot = strategicSpots[Random.Range(0, strategicSpots.Count)];
        strategicSpot.GetComponent<Renderer>().material.color = ScriptA.aiColor;  // AI's color
        LogMove(currentTurn, strategicSpot);
        ChangeTurn();
    }
    else
    {
        // Fallback to random move if no strategic spots are available
        PlayRandomMove();
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
        }

    // Diagonal checks across layers
    score += EvaluateLine(0, 17, 34, 51);
    score += EvaluateLine(4, 21, 38, 55);
    score += EvaluateLine(8, 25, 42, 59);
    score += EvaluateLine(12, 29, 46, 63);
    score += EvaluateLine(3, 18, 33, 48);
    score += EvaluateLine(7, 22, 37, 52);
    score += EvaluateLine(11, 26, 41, 56);
    score += EvaluateLine(15, 30, 45, 60);

    // Check 3D diagonals
    score += EvaluateLine(0, 21, 42, 63);
    score += EvaluateLine(3, 22, 41, 60);
    score += EvaluateLine(12, 25, 38, 51);
    score += EvaluateLine(15, 26, 37, 48);

    Debug.Log(score);
    return score;
    }

    int EvaluateLine(params int[] indices)
    {
    int aiCount = 0;
    int humanCount = 0;
    
    foreach (int index in indices)
    {
        Color color = clickableObjects[index].GetComponent<Renderer>().material.color;
        if (color == ScriptA.aiColor) // AI's color
            aiCount++;
        else if (color == ScriptA.humanColor) // Human's color
            humanCount++;
    }

    if (aiCount > 0 && humanCount > 0) return 0; // Mixed line, no potential
    if (aiCount == 4) return 1000; // AI wins
    if (humanCount == 4) return -900; // Human wins
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

    void LogMove(PlayerType player, GameObject obj)
    {
        string position = System.Array.IndexOf(clickableObjects, obj).ToString();
        moveLog.Add($"{player} moved to position {position}");
    }

    public void DisplayLog()
    {
        logText.text = string.Join("\n", moveLog);
        panelLog.SetActive(true);
    }

    public void OnSairLog()
    {
        panelLog.SetActive(false);
    }

    public void StartAsPlayer()
    {
        Quadrados.SetActive(true);
        Cubos.SetActive(true);
        Sinalizacao.SetActive(true);
        resetButton.SetActive(true);
        currentTurn = PlayerType.Human;
        painelGameStarter.SetActive(false); 
    }

    public void StartAsAI()
    {
        Quadrados.SetActive(true);
        Cubos.SetActive(true);
        Sinalizacao.SetActive(true);
        resetButton.SetActive(true);
        currentTurn = PlayerType.AI;
        painelGameStarter.SetActive(false);
    }
}
