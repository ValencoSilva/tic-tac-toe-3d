using UnityEngine;
using UnityEngine.SceneManagement; // Required for managing scenes

public class ButtonMenu : MonoBehaviour
{
    public string MenuJogarIA;
    public string MenuMultiplayerLocal;
    public string MenuOpcoes;
    public string MenuMultiplayerOnline;
       // Specify the name of the scene to load in the Inspector

    public void JogarIA()
    {
        SceneManager.LoadScene(MenuJogarIA);
    }

     public void MultiplayerLocal()
    {
        SceneManager.LoadScene(MenuMultiplayerLocal);
    }

    public void MultiplayerOnline()
    {
        SceneManager.LoadScene(MenuMultiplayerOnline);
    }

    public void Opcoes()
    {
        SceneManager.LoadScene(MenuOpcoes);
    }

    public void Sair()
    {
    
        Debug.Log("saiu do jogo!");
        Application.Quit();
    }
}
