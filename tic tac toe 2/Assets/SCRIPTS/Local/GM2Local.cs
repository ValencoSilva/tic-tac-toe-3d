using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class GM2Local : MonoBehaviour
{
    public enum PlayerType { Human, Human2 }
    public PlayerType currentTurn = PlayerType.Human;
    public VictoryCheck ScriptA;
    public GameObject[] clickableObjects; // Array of game objects that can be clicked on.
    [SerializeField] private GameObject painelGameStarter;
    [SerializeField] private GameObject painelTurno;
    [SerializeField] private GameObject Quadrados;
    [SerializeField] private GameObject Cubos;
    [SerializeField] private GameObject resetButton;
    [SerializeField] private GameObject Sinalizacao;
    [SerializeField] private Text turnIndicatorText; // Text to display whose turn it is
    [SerializeField] private Image turnIndicatorImage; // Optional image to display the player's turn
    [SerializeField] private Text timerText; // Text to display the remaining time
    [SerializeField] private Text lastMoveText; // Text to display the last move made




    public Text logText; // Reference to the UI text element to display the log
    private List<string> moveLog = new List<string>(); // List to store the log of moves
    [SerializeField] private GameObject panelLog;
    private bool isProcessingAI = false;

    private float turnDuration = 20f; // Duration of each turn in seconds
    private float remainingTime;

    public void Start()
    {
        ScriptA = GameObject.FindObjectOfType<VictoryCheck>();
        UpdateTurnIndicator();
    }

    private void Update()
    {
        if(!ScriptA.IsGameOver())
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }

        if (remainingTime <= 0 && !ScriptA.IsGameOver())
        {
                // Time's up, switch turns
            ChangeTurn();
        }
        if (currentTurn == PlayerType.Human && !ScriptA.IsGameOver())
        {
            CheckForObjectClick();
        }

        if (currentTurn == PlayerType.Human2 && !ScriptA.IsGameOver())
        {
            CheckForObjectClick2();
        }
    }

    void UpdateTimerDisplay()
    {
        timerText.text = $"Tempo Restante: {remainingTime:F1}"; // Display time with one decimal place
    }

    void UpdateTurnIndicator()
    {
        if (turnIndicatorText != null)
        {
            turnIndicatorText.text = currentTurn == PlayerType.Human ? "Player 1's Turn" : "Player 2's Turn";
            turnIndicatorText.color = currentTurn == PlayerType.Human ? ScriptA.humanColor : ScriptA.aiColor;
        }
    }

    void UpdateLastMoveText(PlayerType player, GameObject obj)
    {
        string position = (System.Array.IndexOf(clickableObjects, obj)+1).ToString();
        lastMoveText.text = $"{player} moved to position {position}";
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
                        obj.GetComponent<Renderer>().material.color = ScriptA.humanColor; // Human's color
                        LogMove(currentTurn, obj);
                        UpdateLastMoveText(currentTurn, obj);
                        ChangeTurn();
                        ScriptA.CheckAllWinningConditions();
                        break;
                    }
                }
            }
        }
    }

    void CheckForObjectClick2()
    {
        ScriptA.CheckAllWinningConditions();
        if (ScriptA.winner != VictoryCheck.Winner.None || ScriptA.IsDraw())
        {
            return;  // Exit the method to prevent further interaction
        }

        if (Input.GetMouseButtonDown(0) && currentTurn == PlayerType.Human2)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                foreach (GameObject obj in clickableObjects)
                {
                    if (hit.transform.gameObject == obj && obj.GetComponent<Renderer>().material.color == Color.white)
                    {
                        obj.GetComponent<Renderer>().material.color = ScriptA.aiColor; // Human's color
                        LogMove(currentTurn, obj);
                        UpdateLastMoveText(currentTurn, obj);
                        ChangeTurn();
                        ScriptA.CheckAllWinningConditions();
                        break;
                    }
                }
            }
        }
    }


    void ChangeTurn()
    {
        ScriptA.CheckAllWinningConditions();
        currentTurn = currentTurn == PlayerType.Human ? PlayerType.Human2 : PlayerType.Human;
        remainingTime = turnDuration; // Reset the timer
        UpdateTurnIndicator();
        ScriptA.CheckAllWinningConditions();
    }

    void LogMove(PlayerType player, GameObject obj)
    {
        string position = System.Array.IndexOf(clickableObjects, obj).ToString();
        moveLog.Add($"{player} moved to position {position}");
    }

    public void DisplayLog()
    {
        Debug.Log("teste");
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
        painelTurno.SetActive(true);
        Sinalizacao.SetActive(true);
        resetButton.SetActive(true);
        currentTurn = PlayerType.Human;
        remainingTime = turnDuration;
        UpdateTurnIndicator();
        painelGameStarter.SetActive(false);
    }

    public void StartAsPlayer2()
    {
        Quadrados.SetActive(true);
        Cubos.SetActive(true);
        painelTurno.SetActive(true);
        Sinalizacao.SetActive(true);
        resetButton.SetActive(true);
        currentTurn = PlayerType.Human2;
        remainingTime = turnDuration;
        UpdateTurnIndicator();
        painelGameStarter.SetActive(false);
    }
}