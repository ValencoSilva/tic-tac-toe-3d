using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public HardMode gameManager;
    [SerializeField] private GameObject painelGameStarter;
    [SerializeField] private GameObject Quadrados;
    [SerializeField] private GameObject Cubos;
    [SerializeField] private GameObject resetButton;
    [SerializeField] private GameObject Sinalizacao;

    public void StartAsPlayer()
    {
        Quadrados.SetActive(true);
        Cubos.SetActive(true);
        Sinalizacao.SetActive(true);
        resetButton.SetActive(true);
        gameManager.currentTurn = HardMode.PlayerType.Human;
        painelGameStarter.SetActive(false); 
    }

    public void StartAsAI()
    {
        Quadrados.SetActive(true);
        Cubos.SetActive(true);
        Sinalizacao.SetActive(true);
        resetButton.SetActive(true);
        gameManager.currentTurn = HardMode.PlayerType.AI;
        painelGameStarter.SetActive(false);
    }
}
