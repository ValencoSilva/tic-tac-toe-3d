using UnityEngine;
using UnityEngine.SceneManagement; // Required for managing scenes

public class FinalMenu : MonoBehaviour
{
    public string MenuPrincipal;
    public string Revanche;
    [SerializeField] private GameObject painelVitoria;
    [SerializeField] private GameObject painelDerrota;
    [SerializeField] private GameObject painelEmpate;
    [SerializeField] private GameObject Sinalizacao;
       // Specify the name of the scene to load in the Inspector

    public void onJogarNovamente()
    {
        SceneManager.LoadScene(MenuPrincipal);
    }
    public void onOlhar()
    {
        painelVitoria.SetActive(false);
        painelEmpate.SetActive(false);
        painelDerrota.SetActive(false);
        Sinalizacao.SetActive(true);
    }
    public void onRevanche()
    {
        SceneManager.LoadScene(Revanche);
    }
    public void onVoltar()
    {

    }

    public void onSair()
    {
        Debug.Log("saiu do jogo!");
        Application.Quit();
    }
}