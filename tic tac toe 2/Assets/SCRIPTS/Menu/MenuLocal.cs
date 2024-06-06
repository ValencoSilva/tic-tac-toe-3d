using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLocal : MonoBehaviour
{
    public Button twoPlayersButton;
    public Button threePlayersButton;
    public Button fourPlayersButton;

    public string MenuModos;

    public Sprite defaultSprite;
    public Sprite selectedSprite;

    private int playerCount = 0;

    private void Start()
    {
        // Add listeners to the buttons
        twoPlayersButton.onClick.AddListener(() => TogglePlayerCount(2));
        threePlayersButton.onClick.AddListener(() => TogglePlayerCount(3));
        fourPlayersButton.onClick.AddListener(() => TogglePlayerCount(4));
        
        // Initialize button sprites
        ResetButtonSprites();
    }

    private void TogglePlayerCount(int count)
    {
        if (playerCount == count)
        {
            playerCount = 0; // Deselect if already selected
        }
        else
        {
            playerCount = count;
        }
        Debug.Log("Player Count set to: " + playerCount);
        UpdateButtonSprites();
    }


    public void StartGame()
    {
        string sceneName = "";

        // Determine the scene name based on player count
        if (playerCount == 2)
        {
            sceneName = "2Local";
        }
        else if (playerCount == 3)
        {
            sceneName = "3Local";
        }
        else if (playerCount == 4)
        {
            sceneName = "4Local";
        }

        // Ensure the scene name is valid
        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log("Loading scene: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Invalid scene selection.");
        }
    }

    private void UpdateButtonSprites()
    {
        // Reset all button sprites to default
        ResetButtonSprites();

        // Highlight selected buttons for player count
        if (playerCount == 2)
        {
            twoPlayersButton.GetComponent<Image>().sprite = selectedSprite;
        }
        else if (playerCount == 3)
        {
            threePlayersButton.GetComponent<Image>().sprite = selectedSprite;
        }
        else if (playerCount == 4)
        {
            fourPlayersButton.GetComponent<Image>().sprite = selectedSprite;
        }
    }

    private void ResetButtonSprites()
    {
        // Set all buttons to default sprite
        twoPlayersButton.GetComponent<Image>().sprite = defaultSprite;
        threePlayersButton.GetComponent<Image>().sprite = defaultSprite;
        fourPlayersButton.GetComponent<Image>().sprite = defaultSprite;
    }

    public void OnVoltar()
    {
        SceneManager.LoadScene(MenuModos);
    }
}
