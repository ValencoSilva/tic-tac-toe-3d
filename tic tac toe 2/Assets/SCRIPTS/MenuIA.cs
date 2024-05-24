using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuIA : MonoBehaviour
{
    public Button twoPlayersButton;
    public Button threePlayersButton;
    public Button fourPlayersButton;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    public string MenuModos;

    public Sprite defaultSprite;
    public Sprite selectedSprite;

    private int playerCount = 0;
    private string difficulty = "";

    private Color selectedColor = Color.green; // Color to show when selected
    private Color defaultColor = Color.white;  // Default color of the button

    private void Start()
    {
        // Add listeners to the buttons
        twoPlayersButton.onClick.AddListener(() => TogglePlayerCount(2));
        threePlayersButton.onClick.AddListener(() => TogglePlayerCount(3));
        fourPlayersButton.onClick.AddListener(() => TogglePlayerCount(4));
        easyButton.onClick.AddListener(() => ToggleDifficulty("Easy"));
        mediumButton.onClick.AddListener(() => ToggleDifficulty("Medium"));
        hardButton.onClick.AddListener(() => ToggleDifficulty("Hard"));
        
        // Initialize button colors
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

    private void ToggleDifficulty(string diff)
    {
        if (difficulty == diff)
        {
            difficulty = ""; // Deselect if already selected
        }
        else
        {
            difficulty = diff;
        }
        Debug.Log("Difficulty set to: " + difficulty);
        UpdateButtonSprites();
    }


    private void StartGame()
    {
        Debug.Log("Starting game with " + playerCount + " players on " + difficulty + " difficulty.");
        string sceneName = "";

        // Determine the scene name based on player count and difficulty
        if (playerCount == 2 && difficulty == "Easy")
        {
            sceneName = "2Easy";
        }
        else if (playerCount == 2 && difficulty == "Medium")
        {
            sceneName = "2Medium";
        }
        else if (playerCount == 2 && difficulty == "Hard")
        {
            sceneName = "2Hard";
        }
        else if (playerCount == 3 && difficulty == "Easy")
        {
            sceneName = "3Easy";
        }
        else if (playerCount == 3 && difficulty == "Medium")
        {
            sceneName = "3Medium";
        }
        else if (playerCount == 3 && difficulty == "Hard")
        {
            sceneName = "3Hard";
        }
        else if (playerCount == 4 && difficulty == "Easy")
        {
            sceneName = "4Easy";
        }
        else if (playerCount == 4 && difficulty == "Medium")
        {
            sceneName = "4Medium";
        }
        else if (playerCount == 4 && difficulty == "Hard")
        {
            sceneName = "4Hard";
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

        // Highlight selected buttons for difficulty
        if (difficulty == "Easy")
        {
            easyButton.GetComponent<Image>().sprite = selectedSprite;
        }
        else if (difficulty == "Medium")
        {
            mediumButton.GetComponent<Image>().sprite = selectedSprite;
        }
        else if (difficulty == "Hard")
        {
            hardButton.GetComponent<Image>().sprite = selectedSprite;
        }
    }

    private void ResetButtonSprites()
    {
        // Set all buttons to default sprite
        twoPlayersButton.GetComponent<Image>().sprite = defaultSprite;
        threePlayersButton.GetComponent<Image>().sprite = defaultSprite;
        fourPlayersButton.GetComponent<Image>().sprite = defaultSprite;
        easyButton.GetComponent<Image>().sprite = defaultSprite;
        mediumButton.GetComponent<Image>().sprite = defaultSprite;
        hardButton.GetComponent<Image>().sprite = defaultSprite;
    }

    public void OnVoltar()
    {
        SceneManager.LoadScene(MenuModos);
    }
}

public static class GameSettings
{
    public static int playerCount;
    public static string difficulty;
}
