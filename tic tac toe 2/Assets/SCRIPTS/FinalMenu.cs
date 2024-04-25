using UnityEngine;
using UnityEngine.SceneManagement; // Required for managing scenes

public class FinalMenu : MonoBehaviour
{
    public string MenuPrincipal;
       // Specify the name of the scene to load in the Inspector

    public void onJogarNovamente()
    {
        SceneManager.LoadScene(MenuPrincipal);
    }

    public void onSair()
    {
        Debug.Log("saiu do jogo!");
        Application.Quit();
    }
}