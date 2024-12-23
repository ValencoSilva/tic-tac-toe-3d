using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class GMTeste : MonoBehaviour
{
    public enum PlayerType { Human, Human2 }
    public PlayerType currentTurn = PlayerType.Human;
    public VictoryCheckTeste ScriptA;
    public GameObject[] clickableObjects; // Array of game objects that can be clicked on.
    public JogadaAleatoria jogadaAleatoria; // Reference to JogadaAleatoria script
    public ZeroTimePower zeroTimePower;  // Reference to the ZeroTimePower script



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
    [SerializeField] private InputField player1Input; // Input field for player 1's name
    [SerializeField] private InputField player2Input; // Input field for player 2's name
    

    public Text gameStarterPlayer1Name;
    public Text gameStarterPlayer2Name;
    public Text VictoryPlayer1;
    public Text VictoryPlayer2;
    public List<GameObject> lastPlayedCubes = new List<GameObject>();
    private int currentRound = 1;  // Tracks the current round


    public Text logText; // Reference to the UI text element to display the log
    public List<string> moveLog = new List<string>(); // List to store the log of moves
    [SerializeField] private GameObject panelLog;

    public float turnDuration = 20f; // Duration of each turn in seconds
    public float remainingTime;

    private string player1Name = "Jogador 1"; // Default name for player 1
    private string player2Name = "Jogador 2"; // Default name for player 2

    public void Start()
    {
        ScriptA = GameObject.FindObjectOfType<VictoryCheckTeste>();
        UpdateTurnIndicator();
    }

    public void SetPlayerNames()
    {
        player1Name = player1Input.text != "" ? player1Input.text : "Jogador 1";
        player2Name = player2Input.text != "" ? player2Input.text : "Jogador 2";
    }

    private void Update()
    {
        SetPlayerNames();
        UpdatePlayerNameDisplay();
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

    public void TriggerPower()
    {
        if (jogadaAleatoria != null)
        {
            jogadaAleatoria.OnPoder1(currentTurn); // Pass current player to OnPoder1
        }
        else
        {
            Debug.LogError("JogadaAleatoria reference is missing.");
        }
    }
    public void UpdateTimerDisplay()
    {
        timerText.text = $"Tempo Restante: {remainingTime:F1}"; // Display time with one decimal place
    }

    public void UpdateTurnIndicator()
    {
        if (turnIndicatorText != null)
        {
            turnIndicatorText.text = currentTurn == PlayerType.Human ? $"Turno de {player1Name}" : $"Turno de {player2Name}";
            turnIndicatorText.color = currentTurn == PlayerType.Human ? ScriptA.humanColor : ScriptA.aiColor;
        }
    }

    void UpdatePlayerNameDisplay()
    {
        gameStarterPlayer1Name.text = $"{player1Name}";
        gameStarterPlayer2Name.text = $"{player2Name}";
        VictoryPlayer1.text = $"Parabéns {player1Name}, você venceu";
        VictoryPlayer2.text = $"Parabéns {player2Name}, você venceu";
    }

    void UpdateLastMoveText(PlayerType player, GameObject obj)
    {
        string position = (System.Array.IndexOf(clickableObjects, obj)+1).ToString();
        lastMoveText.text = $"{(player == PlayerType.Human ? player1Name : player2Name)} moveu para a posição {position}";
    }

   

    void CheckForObjectClick()
    {
        ScriptA.CheckAllWinningConditions();
      
        if (ScriptA.winner != VictoryCheckTeste.Winner.None || ScriptA.IsDraw())
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
        if (ScriptA.winner != VictoryCheckTeste.Winner.None || ScriptA.IsDraw())
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


    public void ChangeTurn()
    {
        ScriptA.CheckAllWinningConditions();

        // Increment the round at every turn change
        if (currentTurn == PlayerType.Human2) // Only increment after both players have played
        {
            currentRound++;
        }

        // Check if ZeroTimePower's skipNextTurn is true
        if (zeroTimePower != null && zeroTimePower.skipNextTurn)
        {
            zeroTimePower.skipNextTurn = false; // Reset the flag after use
            remainingTime = turnDuration;  // Reset the timer for the extra turn
            UpdateTurnIndicator();
            return;  // Skip changing the turn
        }

        // Normal turn change
        currentTurn = currentTurn == PlayerType.Human ? PlayerType.Human2 : PlayerType.Human;
        remainingTime = turnDuration; // Reset the timer
        UpdateTurnIndicator();
        ScriptA.CheckAllWinningConditions();
    }


    public int GetCurrentRound()
    {
        return currentRound;
    }


    public void LogMove(PlayerType player, GameObject obj)
    {
        string position = System.Array.IndexOf(clickableObjects, obj).ToString();
        moveLog.Add($"{(player == PlayerType.Human ? player1Name : player2Name)} moveu para a posição {position}");

        // Track the last played cubes
        if (!lastPlayedCubes.Contains(obj)) // Avoid adding duplicates
        {
            lastPlayedCubes.Add(obj);
        }

        if (lastPlayedCubes.Count > 4)
        {
            lastPlayedCubes.RemoveAt(0); // Keep only the last 2 moves
        }

        Debug.Log($"Logged move for {player}: Cube at position {position}.");
    }


    public void DisplayLog()
    {
        //Debug.Log("teste");
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