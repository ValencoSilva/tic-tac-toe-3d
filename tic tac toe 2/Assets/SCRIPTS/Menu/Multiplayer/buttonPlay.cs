
using UnityEngine;
using UnityEngine.SceneManagement; // Required for managing scenes

public class buttonPlay : MonoBehaviour
{
    public string sceneNameToLoad2;
    public string sceneNameToLoad3;
    //public string sceneNameToLoad4;
       
    public void OnButtonPressed2()
    {
        SceneManager.LoadScene(sceneNameToLoad2);
    }

     public void OnButtonPressed3()
    {
        SceneManager.LoadScene(sceneNameToLoad3);
    }

    //public void OnButtonPressed4()
    //{
    
     //   SceneManager.LoadScene(sceneNameToLoad4);
    //}
}
