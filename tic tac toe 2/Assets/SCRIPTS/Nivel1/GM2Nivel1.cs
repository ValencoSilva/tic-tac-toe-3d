using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class GM2Nivel1 : MonoBehaviour
{
    public enum PlayerType { Human, AI }
    public PlayerType currentTurn = PlayerType.Human;
    public VictoryCheck ScriptA;
    public GameObject[] clickableObjects; // Array of game objects that can be clicked on.
    [SerializeField] private GameObject painelGameStarter;
    [SerializeField] private GameObject Quadrados;
    [SerializeField] private GameObject Cubos;
    [SerializeField] private GameObject resetButton;
    [SerializeField] private GameObject Sinalizacao;

    public Text logText; // Reference to the UI text element to display the log
    private List<string> moveLog = new List<string>(); // List to store the log of moves
    [SerializeField] private GameObject panelLog;
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
            yield break;  // Exit the coroutine early if the game is over
        }

        AI_EasyTurn();

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
                        obj.GetComponent<Renderer>().material.color = ScriptA.humanColor; // Human's color
                        LogMove(currentTurn, obj);
                        ChangeTurn();
                        ScriptA.CheckAllWinningConditions();
                        break;
                    }
                }
            }
        }
    }


    void AI_EasyTurn()
    {
        List<GameObject> availableSpots = clickableObjects.Where(obj => obj.GetComponent<Renderer>().material.color == Color.white).ToList();
        if (availableSpots.Count > 0)
        {
            GameObject randomSpot = availableSpots[Random.Range(0, availableSpots.Count)];
            randomSpot.GetComponent<Renderer>().material.color = ScriptA.aiColor;
            LogMove(currentTurn, randomSpot);
            ChangeTurn();
        }
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