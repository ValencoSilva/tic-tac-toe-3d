using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class GM4Local : MonoBehaviour
{
    public enum PlayerType { Human, Human2, Human3, Human4 }
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

        if (currentTurn == PlayerType.Human3 && !ScriptA.IsGameOver())
        {
            CheckForObjectClick3();
        }

        if (currentTurn == PlayerType.Human4 && !ScriptA.IsGameOver())
        {
            CheckForObjectClick4();
        }
    }

    void UpdateTimerDisplay()
    {
        timerText.text = $"Tempo Restante: {remainingTime:F1}"; // Display time with one decimal place
    }

    void UpdateTurnIndicator()
    {
        switch (currentTurn)
        {
            case PlayerType.Human:
                turnIndicatorText.text = "Player 1's Turn";
                turnIndicatorText.color = ScriptA.humanColor;
                break;
            case PlayerType.Human2:
                turnIndicatorText.text = "Player 2's Turn";
                turnIndicatorText.color = ScriptA.aiColor;
                break;
            case PlayerType.Human3:
                turnIndicatorText.text = "Player 3's Turn";
                turnIndicatorText.color = ScriptA.ai2Color;
                break;
            case PlayerType.Human4:
                turnIndicatorText.text = "Player 4's Turn";
                turnIndicatorText.color = ScriptA.ai3Color;
                break;
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



    void CheckForObjectClick3()
    {
        ScriptA.CheckAllWinningConditions();
        if (ScriptA.winner != VictoryCheck.Winner.None || ScriptA.IsDraw())
        {
            return;  // Exit the method to prevent further interaction
        }

        if (Input.GetMouseButtonDown(0) && currentTurn == PlayerType.Human3)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                foreach (GameObject obj in clickableObjects)
                {
                    if (hit.transform.gameObject == obj && obj.GetComponent<Renderer>().material.color == Color.white)
                    {
                        obj.GetComponent<Renderer>().material.color = ScriptA.ai2Color; // Human's color
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



    void CheckForObjectClick4()
    {
        ScriptA.CheckAllWinningConditions();
        if (ScriptA.winner != VictoryCheck.Winner.None || ScriptA.IsDraw())
        {
            return;  // Exit the method to prevent further interaction
        }

        if (Input.GetMouseButtonDown(0) && currentTurn == PlayerType.Human4)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                foreach (GameObject obj in clickableObjects)
                {
                    if (hit.transform.gameObject == obj && obj.GetComponent<Renderer>().material.color == Color.white)
                    {
                        obj.GetComponent<Renderer>().material.color = ScriptA.ai3Color; // Human's color
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
        if (currentTurn == PlayerType.Human)
        {
            currentTurn = PlayerType.Human2;
            Debug.Log("It's the Human2's turn now!");
        }
        else if (currentTurn == PlayerType.Human2)
        {
            currentTurn = PlayerType.Human3;
            Debug.Log("It's the Human3's turn now!");
        }
        else if (currentTurn == PlayerType.Human3)
        {
            currentTurn = PlayerType.Human4;
            Debug.Log("It's the Human4's turn now!");
        }
        else if (currentTurn == PlayerType.Human4)
        {
            currentTurn = PlayerType.Human;
            Debug.Log("It's the Human's turn now!");
        }
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

    public void StartAsPlayer3()
    {
        Quadrados.SetActive(true);
        Cubos.SetActive(true);
        painelTurno.SetActive(true);
        Sinalizacao.SetActive(true);
        resetButton.SetActive(true);
        currentTurn = PlayerType.Human3;
        remainingTime = turnDuration;
        UpdateTurnIndicator();
        painelGameStarter.SetActive(false);
    }

    public void StartAsPlayer4()
    {
        Quadrados.SetActive(true);
        Cubos.SetActive(true);
        painelTurno.SetActive(true);
        Sinalizacao.SetActive(true);
        resetButton.SetActive(true);
        currentTurn = PlayerType.Human4;
        remainingTime = turnDuration;
        UpdateTurnIndicator();
        painelGameStarter.SetActive(false);
    }
}