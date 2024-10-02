using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuModos : MonoBehaviour
{
    public string MenuIA;
    public string MenuLocal;
    public string MenuPrincipal;
    public string MenuOnline;
    public string Teste;
    public void OnJogarContraIA()
    {
        SceneManager.LoadScene(MenuIA);
    }

    public void OnJogarLocal()
    {
        SceneManager.LoadScene(MenuLocal);
    }

    public void OnJogarOnline()
    {
        //SceneManager.LoadScene(MenuOnline);
    }

    public void OnVoltar()
    {
        SceneManager.LoadScene(MenuPrincipal);
    }

    public void OnTeste()
    {
        SceneManager.LoadScene(Teste);
    }

}

