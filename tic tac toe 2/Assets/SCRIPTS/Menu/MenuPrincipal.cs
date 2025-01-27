using UnityEngine;
using UnityEngine.SceneManagement; // Required for managing scenes

public class MenuPrincipal : MonoBehaviour
{
    public string MenuModos;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelCreditos;
    [SerializeField] private GameObject painelLanguage;
    [SerializeField] private GameObject Cubo;

    public void OnJogar()
    {
        SceneManager.LoadScene(MenuModos);
    }

    public void Opcoes()
    {
        painelOpcoes.SetActive(true);
    }

    public void Language()
    {
        painelLanguage.SetActive(true);
    }

    public void Creditos()
    {
        painelCreditos.SetActive(true);
        Cubo.SetActive(false);
    }

    public void VoltarOpcoes()
    {
        painelOpcoes.SetActive(false);
    }

    public void VoltarLanguage()
    {
        painelLanguage.SetActive(false);
    }

    public void VoltarCreditos()
    {
        painelCreditos.SetActive(false);
        Cubo.SetActive(true);
    }

    public void Sair()
    {
    
        Debug.Log("saiu do jogo!");
        Application.Quit();
    }
}
