using UnityEngine;

public class CheckM : MonoBehaviour
{
    public GameObject[] cubes; // Should be an array of 64 GameObjects (4x4x4)
    public Color ignoreColor = Color.white; // Set this to the color you want to ignore
    public bool winDetected = false;
    public string FinalScene;
    public Winner winner = Winner.None; // Track the winner
    public enum Winner { None, Human, AI }
    [SerializeField] private GameObject painelVitoria;
    [SerializeField] private GameObject painelDerrota;
    [SerializeField] private GameObject painelEmpate;
    [SerializeField] private GameObject Sinalizacao;

    private void Update()
    {
        if (cubes.Length != 64)
        {
            Debug.LogError("Incorrect number of game objects. There should be exactly 64 objects.");
            return;
        }

        CheckAllWinningConditions();

        if (winDetected)
        {
            if (winner == Winner.Human)
            {
                painelVitoria.SetActive(true);
            }
            else if (winner == Winner.AI)
            {
                painelDerrota.SetActive(true);
            }
        }
        else if (IsDraw())
        {
            painelEmpate.SetActive(true);
        }
    }

    public bool UpdateAndCheckGameEnd()
    {
        CheckAllWinningConditions();
        return (winner != Winner.None || IsDraw());
    }

    public bool IsGameOver()
    {
        return winDetected || IsDraw();
    }

    public void CheckAllWinningConditions()
    {
        // Horizontal lines within each layer
        for (int layer = 0; layer < 4; layer++)
        {
            for (int row = 0; row < 4; row++)
            {
                int start = layer * 16 + row * 4;
                CheckCondition(start, start + 1, start + 2, start + 3, "horizontal line");
            }
        }

        // Vertical lines within each layer
        for (int layer = 0; layer < 4; layer++)
        {
            for (int col = 0; col < 4; col++)
            {
                int start = layer * 16 + col;
                CheckCondition(start, start + 4, start + 8, start + 12, "vertical line");
            }
        }

        // Major diagonals within each layer
        for (int layer = 0; layer < 4; layer++)
        {
            int start = layer * 16;
            CheckCondition(start, start + 5, start + 10, start + 15, "major diagonal");
        }

        // Minor diagonals within each layer
        for (int layer = 0; layer < 4; layer++)
        {
            int start = layer * 16 + 3;
            CheckCondition(start, start + 3, start + 6, start + 9, "minor diagonal");
        }

        // Vertical lines through layers
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                int start = row * 4 + col;
                CheckCondition(start, start + 16, start + 32, start + 48, "vertical through layers");
            }
        }

        // 2D Major Diagonals through Layers (Aligned)
        for (int col = 0; col < 4; col++)
        {
            CheckCondition(col, 20 + col, 40 + col, 60 + col, "2D major diagonal through layers from front to back");
        }

        for (int col = 0; col < 4; col++)
        {
            CheckCondition(12 + col, 24 + col, 36 + col, 48 + col, "2D major diagonal through layers from back to front");
        }

        // 2D Minor Diagonals through Layers (Aligned)
        for (int row = 0; row < 4; row++)
        {
            CheckCondition(3 + row * 16, 18 + row * 4, 33 + row, 48 + row, "2D minor diagonal through layers from front to back");
        }

        for (int row = 0; row < 4; row++)
        {
            CheckCondition(15 - row * 16, 26 - row * 4, 37 - row, 48 + row, "2D minor diagonal through layers from back to front");
        }

        // 3D diagonals through the entire grid
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
            if (cubes[a].GetComponent<Renderer>().material.color == Color.red)
            {
                Debug.Log($"Winning condition met for Human on {description}.");
                winner = Winner.Human;
                winDetected = true;
            }
            else if (cubes[a].GetComponent<Renderer>().material.color == Color.blue)
            {
                Debug.Log($"Winning condition met for AI on {description}.");
                winner = Winner.AI;
                winDetected = true;
            }
        }
    }

    private int AreColorsAlmostSame(int a, int b, int c, int d)
    {
        int[] indices = { a, b, c, d };
        Color firstColor = GetNonIgnoredColor(indices);
        if (firstColor == Color.clear) return 0; // No valid color found

        int matchCount = 0;
        foreach (int index in indices)
        {
            Color currentColor = cubes[index].GetComponent<Renderer>().material.color;
            if (currentColor != ignoreColor && currentColor == firstColor)
                matchCount++;
        }

        return matchCount;
    }

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
        if (winDetected) return false;

        foreach (GameObject cube in cubes)
        {
            if (cube.GetComponent<Renderer>().material.color == Color.white) // Assuming white is the unoccupied color
            {
                return false;
            }
        }
        return true;
    }
}
