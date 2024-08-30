using UnityEngine;
using UnityEngine.SceneManagement; // Required for managing scenes

public class FinalMenuAI : MonoBehaviour
{
    public string MenuPrincipal;
    public string Revanche;
    [SerializeField] private GameObject painelVitoria;
    [SerializeField] private GameObject painelEmpate;
    [SerializeField] private GameObject painelDerrota;
    [SerializeField] private GameObject painelNomeJogadores;
    [SerializeField] private GameObject botaoVoltarV;
    [SerializeField] private GameObject botaoVoltarE;
    [SerializeField] private GameObject botaoVoltarD;
    [SerializeField] private GameObject Sinalizacao;
    [SerializeField] private GameObject Tabuleiro;
    [SerializeField] private GameObject Cubo;
       // Specify the name of the scene to load in the Inspector

    public void onJogarNovamente()
    {
        SceneManager.LoadScene(MenuPrincipal);
    }

    public void onOK()
    {
        painelNomeJogadores.SetActive(false);
    }
    
    public void onOlharVitoria()
    {
        painelVitoria.SetActive(false);
        Sinalizacao.SetActive(true);
        Tabuleiro.SetActive(true);
        Cubo.SetActive(true);
        botaoVoltarV.SetActive(true);
    }

    public void onOlharDerrota()
    {
        painelDerrota.SetActive(false);
        Sinalizacao.SetActive(true);
        Tabuleiro.SetActive(true);
        Cubo.SetActive(true);
        botaoVoltarD.SetActive(true);
    }

    
    public void onOlharEmpate()
    {
        painelEmpate.SetActive(false);
        Sinalizacao.SetActive(true);
        Tabuleiro.SetActive(true);
        Cubo.SetActive(true);
        botaoVoltarE.SetActive(true);
    }

    public void onRevanche()
    {
        SceneManager.LoadScene(Revanche);
    }

    public void onVoltarDerrota()
    {
        botaoVoltarD.SetActive(false);
        painelDerrota.SetActive(true);
        Sinalizacao.SetActive(false);
    }

    public void onVoltarVitoria()
    {
        botaoVoltarV.SetActive(false);
        painelVitoria.SetActive(true);
        Sinalizacao.SetActive(false);
    }

    public void onVoltarEmpate()
    {
        botaoVoltarE.SetActive(false);
        painelEmpate.SetActive(true);
        Sinalizacao.SetActive(false);
    }



    public void onSair()
    {
        Debug.Log("saiu do jogo!");
        Application.Quit();
    }
}