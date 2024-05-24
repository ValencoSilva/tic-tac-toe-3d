using UnityEngine;
using UnityEngine.SceneManagement; // Required for managing scenes

public class MenuPrincipal : MonoBehaviour
{
    public string MenuModos;
    [SerializeField] private GameObject painelOpcoes;

    public void OnJogar()
    {
        SceneManager.LoadScene(MenuModos);
    }

    public void Opcoes()
    {
        painelOpcoes.SetActive(true);
    }

    public void Sair()
    {
    
        Debug.Log("saiu do jogo!");
        Application.Quit();
    }
}
