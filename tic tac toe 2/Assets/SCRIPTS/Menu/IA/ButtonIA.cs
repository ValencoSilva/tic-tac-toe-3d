using UnityEngine;
using UnityEngine.SceneManagement; // Required for managing scenes

public class ButtonIA : MonoBehaviour
{
    public string sceneNameToLoadFacil;
    public string sceneNameToLoadMedio;
    public string sceneNameToLoadDificil;
       // Specify the name of the scene to load in the Inspector

    public void OnButtonPressedFacil()
    {
        SceneManager.LoadScene(sceneNameToLoadFacil);
    }

     public void OnButtonPressedMedio()
    {
        SceneManager.LoadScene(sceneNameToLoadMedio);
    }

    public void OnButtonPressedDificil()
    {
    
        SceneManager.LoadScene(sceneNameToLoadDificil);
    }
}
