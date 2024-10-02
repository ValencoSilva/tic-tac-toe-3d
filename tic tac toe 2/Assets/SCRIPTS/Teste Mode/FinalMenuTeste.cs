using UnityEngine;
using UnityEngine.SceneManagement; // Required for managing scenes

public class FinalMenuTeste : MonoBehaviour
{
    public string MenuPrincipal;
    public string Revanche;
    [SerializeField] private GameObject painelVitoria1;
    [SerializeField] private GameObject painelVitoria2;
    [SerializeField] private GameObject painelVitoria3;
    [SerializeField] private GameObject painelVitoria4;
    [SerializeField] private GameObject painelEmpate;
    [SerializeField] private GameObject painelNomeJogadores;
    [SerializeField] private GameObject botaoVoltarV1;
    [SerializeField] private GameObject botaoVoltarE;
    [SerializeField] private GameObject botaoVoltarV2;
    [SerializeField] private GameObject botaoVoltarV3;
    [SerializeField] private GameObject botaoVoltarV4;
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
    
    public void onOlharWin2()
    {
        painelVitoria2.SetActive(false);
        Sinalizacao.SetActive(true);
        Tabuleiro.SetActive(true);
        Cubo.SetActive(true);
        botaoVoltarV2.SetActive(true);
    }

    public void onOlharWin1()
    {
        painelVitoria1.SetActive(false);
        Sinalizacao.SetActive(true);
        Tabuleiro.SetActive(true);
        Cubo.SetActive(true);
        botaoVoltarV1.SetActive(true);
    }

    public void onOlharWin3()
    {
        painelVitoria3.SetActive(false);
        Sinalizacao.SetActive(true);
        Tabuleiro.SetActive(true);
        Cubo.SetActive(true);
        botaoVoltarV3.SetActive(true);
    }

    public void onOlharWin4()
    {
        painelVitoria4.SetActive(false);
        Sinalizacao.SetActive(true);
        Tabuleiro.SetActive(true);
        Cubo.SetActive(true);
        botaoVoltarV4.SetActive(true);
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

    public void onVoltarWin2()
    {
        botaoVoltarV2.SetActive(false);
        painelVitoria2.SetActive(true);
        Sinalizacao.SetActive(false);
    }

    public void onVoltarWin1()
    {
        botaoVoltarV1.SetActive(false);
        painelVitoria1.SetActive(true);
        Sinalizacao.SetActive(false);
    }

    public void onVoltarWin3()
    {
        botaoVoltarV3.SetActive(false);
        painelVitoria3.SetActive(true);
        Sinalizacao.SetActive(false);
    }

    public void onVoltarWin4()
    {
        botaoVoltarV4.SetActive(false);
        painelVitoria4.SetActive(true);
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