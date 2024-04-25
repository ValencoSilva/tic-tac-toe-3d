using UnityEngine;

public class CheckM : MonoBehaviour
{
    public GameObject[] cubes; // Should be an array of 64 GameObjects (4x4x4)
    public Color ignoreColor = Color.white; // Set this to the color you want to ignore
    

    private void Update()
    {
        if (cubes.Length != 64)
        {
            Debug.LogError("Incorrect number of game objects. There should be exactly 64 objects.");
            return;
        }

        CheckAllWinningConditions();
    }
    
    public bool CheckAllWinningConditions()
    {
        bool winDetected = false;
        // Similar loop structure as previously explained
        for (int i = 0; i < 64; i++)
        {
            // Only check valid starting positions for each type of line
            if (i % 4 < 1) CheckCondition(i, i + 1, i + 2, i + 3, "horizontal line"); winDetected = true; 
            if (i % 16 < 4) CheckCondition(i, i + 4, i + 8, i + 12, "vertical line"); winDetected = true;
            if (i % 16 == 0) CheckCondition(i, i + 5, i + 10, i + 15, "major diagonal"); winDetected = true;
            if (i % 16 == 3) CheckCondition(i, i + 3, i + 6, i + 9, "minor diagonal"); winDetected = true;
            if (i < 16) CheckCondition(i, i + 16, i + 32, i + 48, "vertical through layers"); winDetected = true;
        }
        for (int col = 0; col < 4; col++)
        {
            CheckCondition(col, 20 + col, 40 + col, 60 + col, "2D major diagonal through layers from front to back"); winDetected = true;
            CheckCondition(12 + col, 24 + col, 36 + col, 48 + col, "2D major diagonal through layers from back to front"); winDetected = true;

            CheckCondition(3 + col, 18 + col, 33 + col, 48 + col, "2D minor diagonal through layers from front to back"); winDetected = true;
            CheckCondition(15 + col, 26 + col, 37 + col, 48 + col, "2D minor diagonal through layers from back to front"); winDetected = true;
        }

        // Check 3D diagonals
        CheckCondition(0, 21, 42, 63, "3D diagonal from top-left-front to bottom-right-back"); winDetected = true;
        CheckCondition(3, 22, 41, 60, "3D diagonal from top-right-front to bottom-left-back"); winDetected = true;
        CheckCondition(12, 25, 38, 51, "3D diagonal from top-left-back to bottom-right-front"); winDetected = true;
        CheckCondition(15, 26, 37, 48, "3D diagonal from top-right-back to bottom-left-front"); winDetected = true;
        return winDetected;
    }

    public void CheckCondition(int a, int b, int c, int d, string description)
    {
        int nearWins = AreColorsAlmostSame(a, b, c, d);
        if (nearWins == 4)
        {
            Debug.Log($"Winning condition met on {description}.");
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
}
