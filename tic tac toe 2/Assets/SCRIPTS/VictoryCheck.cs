using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryCheck : MonoBehaviour
{
    public GameObject[] cubes; // Should be an array of 64 GameObjects (4x4x4)
    public Color ignoreColor = Color.white; // Set this to the color you want to ignore
    public bool winDetected = false;
    public string FinalScene;
    

    private void Update()
    {
        if (cubes.Length != 64)
        {
            Debug.LogError("Incorrect number of game objects. There should be exactly 64 objects.");
            return;
        }

        CheckAllWinningConditions();
        // Check for a win or a draw after each move
        if (winDetected == true)
        {
            Debug.Log("Game Over: Win Detected!");
            SceneManager.LoadScene(FinalScene);
            
            // Handle win (disable further moves, show win message, etc.)
        }
        else if (IsDraw())
        {
            Debug.Log("Game Over: It's a Draw!");
            // Handle draw (disable further moves, show draw message, etc.)
        }
    }
    
    public void CheckAllWinningConditions()
    {
        // Similar loop structure as previously explained
        for (int i = 0; i < 64; i++)
        {
            // Only check valid starting positions for each type of line
            if (i % 4 < 1) CheckCondition(i, i + 1, i + 2, i + 3, "horizontal line"); 
            if (i % 16 < 4) CheckCondition(i, i + 4, i + 8, i + 12, "vertical line"); 
            if (i % 16 == 0) CheckCondition(i, i + 5, i + 10, i + 15, "major diagonal"); 
            if (i % 16 == 3) CheckCondition(i, i + 3, i + 6, i + 9, "minor diagonal"); 
            if (i < 16) CheckCondition(i, i + 16, i + 32, i + 48, "vertical through layers"); 
        }
        
        for (int col = 0; col < 4; col++)
        {
            CheckCondition(col, 20 + col, 40 + col, 60 + col, "2D major diagonal through layers from front to back"); 
            CheckCondition(12 + col, 24 + col, 36 + col, 48 + col, "2D major diagonal through layers from back to front"); 

            CheckCondition(3 + col, 18 + col, 33 + col, 48 + col, "2D minor diagonal through layers from front to back"); 
            CheckCondition(15 + col, 26 + col, 37 + col, 48 + col, "2D minor diagonal through layers from back to front"); 
        }

        // Check 3D diagonals
        CheckCondition(0, 21, 42, 63, "3D diagonal from top-left-front to bottom-right-back"); 
        CheckCondition(3, 22, 41, 60, "3D diagonal from top-right-front to bottom-left-back"); 
        CheckCondition(12, 25, 38, 51, "3D diagonal from top-left-back to bottom-right-front"); 
        CheckCondition(15, 26, 37, 48, "3D diagonal from top-right-back to bottom-left-front"); 
    }

    public void CheckCondition(int a, int b, int c, int d, string description)
    {
        int nearWins = AreColorsAlmostSame(a, b, c, d);
        if (nearWins == 4)
        {
            Debug.Log($"Winning condition met on {description}.");
            winDetected = true;
        }
        else if (nearWins == 3)
        {            
            Debug.Log($"Close to winning on {description}.");
        }
    }

    // Modified to ignore specific color (white in this case)
    private int AreColorsAlmostSame(int a, int b, int c, int d)
    {
        int[] indices = {a, b, c, d};
        Color firstColor = GetNonIgnoredColor(indices);
        if (firstColor == Color.clear) return 0; // No valid color found

        int matchCount = 0;
        foreach (int index in indices)
        {
            Color currentColor = cubes[index].GetComponent<Renderer>().material.color;
            if (currentColor != ignoreColor && currentColor == firstColor)
                matchCount++;
            if (currentColor == ignoreColor)
            {

            }
        }

        return matchCount;
    }

    // Helper method to find the first non-ignored color
    private Color GetNonIgnoredColor(int[] indices)
    {
        foreach (int index in indices)
        {
            Color color = cubes[index].GetComponent<Renderer>().material.color;
            if (color != ignoreColor)
                return color;
        }
        return Color.clear; // Return clear if all colors are ignored
    }

    public bool IsDraw()
    {
    // First, check if there's a win, if there's a win, it's not a draw
    if (winDetected == true)
        return false;

    // Check if all spots are filled
    foreach (GameObject cube in cubes)
    {
        if (cube.GetComponent<Renderer>().material.color == Color.white)  // Assuming white is the unoccupied color
        {
            // If any cube is still unoccupied, it's not a draw
            return false;
        }
    }

    // If all cubes are occupied and there's no win, it's a draw
    return true;
    }

}