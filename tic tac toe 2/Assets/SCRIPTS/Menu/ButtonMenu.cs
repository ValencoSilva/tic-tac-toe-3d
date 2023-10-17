using UnityEngine;
using UnityEngine.SceneManagement; // Required for managing scenes

public class ButtonMenu : MonoBehaviour
{
    public string sceneNameToLoadJogar;
    public string sceneNameToLoadJogarIA;
       // Specify the name of the scene to load in the Inspector

    public void OnButtonPressedJogarIA()
    {
        SceneManager.LoadScene(sceneNameToLoadJogarIA);
    }

     public void OnButtonPressedJogar()
    {
        SceneManager.LoadScene(sceneNameToLoadJogar);
    }

    public void OnButtonPressedMenu()
    {
    
        Debug.Log("saiu do jogo!");
    }
}
