using UnityEngine;
using UnityEngine.SceneManagement; // Required for managing scenes

public class ButtonIA : MonoBehaviour
{
    public string Facil;
    public string Medio;
    public string Dificil;
    public string Voltar;
       // Specify the name of the scene to load in the Inspector

    public void onFacil()
    {
        SceneManager.LoadScene(Facil);
    }

     public void onMedio()
    {
        SceneManager.LoadScene(Medio);
    }

    public void onDificil()
    {
    
        SceneManager.LoadScene(Dificil);
    }
    public void onVoltar()
    {
    
        SceneManager.LoadScene(Voltar);
    }
}
