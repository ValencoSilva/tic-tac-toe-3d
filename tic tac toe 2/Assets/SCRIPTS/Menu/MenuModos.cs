using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuModos : MonoBehaviour
{
    public string MenuIA;
    public string MenuLocal;
    public string MenuPrincipal;
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
        Debug.Log("ainda em desenvolvimento");
    }

    public void OnVoltar()
    {
        SceneManager.LoadScene(MenuPrincipal);
    }

}

