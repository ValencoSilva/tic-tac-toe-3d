
using UnityEngine;
using UnityEngine.SceneManagement; // Required for managing scenes

public class buttonPlay : MonoBehaviour
{
    public string twoPlayers;
    public string threePlayers;
    public string fourPlayers;
    public string Voltar;
       
    public void ontwoPlayers()
    {
        SceneManager.LoadScene(twoPlayers);
    }

    public void onThreePlayers()
    {
        SceneManager.LoadScene(threePlayers);
    }

    public void onFourPlayers()
    {
        SceneManager.LoadScene(fourPlayers);
    }
    public void onVoltar()
    {
        SceneManager.LoadScene(Voltar);
    }


}
