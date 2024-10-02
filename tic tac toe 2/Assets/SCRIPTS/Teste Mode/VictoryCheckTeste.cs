using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryCheckTeste : MonoBehaviour
{
    public GameObject[] cubes; // Should be an array of 64 GameObjects (4x4x4)
    public Color ignoreColor = Color.white; // Set this to the color you want to ignore
    public bool winDetected = false;
    public string FinalScene;
    public Winner winner = Winner.None; // Track the winner
    public enum Winner { None, Human, AI, AI2, AI3 }
    public Color humanColor = Color.yellow;
    public Color aiColor = new Color(0.5f, 0f, 0.5f);
    public Color ai2Color = Color.red;
    public Color ai3Color = Color.red;
    
    [SerializeField] private GameObject painelVitoria;
    [SerializeField] private GameObject painelDerrota;
    [SerializeField] private GameObject painelDerrotaAI2;
    [SerializeField] private GameObject painelDerrotaAI3;
    [SerializeField] private GameObject painelEmpate;
    [SerializeField] private GameObject Sinalizacao;
    [SerializeField] private GameObject painelContagem;
    [SerializeField] private GameObject buttonPoderes;
    int i = 0;

    private void Update()
    {
        if (cubes.Length != 64)
        {
            Debug.LogError("Incorrect number of game objects. There should be exactly 64 objects.");
            return;
        }

        if (i == 0 && AllCubesFilled())
        {
            IsDraw();
            CheckForDraw();
            Debug.Log("upd teste");
            painelContagem.SetActive(false);
            i += 1;
        }
    }

    public bool AllCubesFilled()
    {
        foreach (GameObject cube in cubes)
        {
            if (cube.GetComponent<Renderer>().material.color == Color.white)
            {
                return false;
            }
        }
        return true;
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
        }

        CheckCondition(0, 17, 34, 51, "diagonal marcos"); 
        CheckCondition(4, 21, 38, 55, "diagonal marcos"); 
        CheckCondition(8, 25, 42, 59, "diagonal marcos"); 
        CheckCondition(12, 29, 46, 63, "diagonal marcos"); 
        CheckCondition(3, 18, 33, 48, "diagonal marcos invertida"); 
        CheckCondition(7, 22, 37, 52, "diagonal marcos invertida"); 
        CheckCondition(11, 26, 41, 56, "diagonal marcos invertida"); 
        CheckCondition(15, 30, 45, 60, "diagonal marcos invertida");

        // Check 3D diagonals
        CheckCondition(0, 21, 42, 63, "3D diagonal from top-left-front to bottom-right-back"); 
        CheckCondition(3, 22, 41, 60, "3D diagonal from top-right-front to bottom-left-back"); 
        CheckCondition(12, 25, 38, 51, "3D diagonal from top-left-back to bottom-right-front"); 
        CheckCondition(15, 26, 37, 48, "3D diagonal from top-right-back to bottom-left-front");

        if (IsDraw() && winner == Winner.None)
        {
            Debug.Log($"Game Over: It's a Draw!");
            painelEmpate.SetActive(true);
            Sinalizacao.SetActive(false);
        }

        IsDraw();
        CheckForDraw();
    }

    public void CheckCondition(int a, int b, int c, int d, string description)
    {
        int nearWins = AreColorsAlmostSame(a, b, c, d);
        if (nearWins == 4)
        {
            if (cubes[a].GetComponent<Renderer>().material.color == humanColor)
            {
                Debug.Log($"Winning condition met for Human on {description}.");
                winner = Winner.Human;
                winDetected = true;
                painelVitoria.SetActive(true);
                Sinalizacao.SetActive(false);
                painelContagem.SetActive(false);
                buttonPoderes.SetActive(false);
            }
            else if (cubes[a].GetComponent<Renderer>().material.color == aiColor)
            {
                Debug.Log($"Winning condition met for AI on {description}.");
                winner = Winner.AI;
                winDetected = true;
                painelDerrota.SetActive(true);
                Sinalizacao.SetActive(false);
                painelContagem.SetActive(false);
                buttonPoderes.SetActive(false);
            }
            else if (cubes[a].GetComponent<Renderer>().material.color == ai2Color)
            {
                Debug.Log($"Winning condition met for AI2 on {description}.");
                winner = Winner.AI2;
                winDetected = true;
                painelDerrotaAI2.SetActive(true);
                Sinalizacao.SetActive(false);
                painelContagem.SetActive(false);
                buttonPoderes.SetActive(false);
            }
            else if (cubes[a].GetComponent<Renderer>().material.color == ai3Color)
            {
                Debug.Log($"Winning condition met for AI3 on {description}.");
                winner = Winner.AI3;
                winDetected = true;
                painelDerrotaAI3.SetActive(true);
                Sinalizacao.SetActive(false);
                painelContagem.SetActive(false);
                buttonPoderes.SetActive(false);
            }
        }
    }

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

    public void CheckForDraw()
    {
        if (IsDraw())
        {
            Debug.Log("It's a draw!");
            winner = Winner.None;
            winDetected = true;
            painelEmpate.SetActive(true);
            Sinalizacao.SetActive(false);
        }
    }

    public bool IsDraw()
    {
        if (winDetected) return false;

        foreach (GameObject cube in cubes)
        {
            if (cube.GetComponent<Renderer>().material.color == Color.white)
            {
                return false;
            }
        }

        return true;
    }

    // Method to rotate the front face counterclockwise in Y axis and update indices
public void RotateFrontFaceCounterclockwise()
{
    // Temporary array to store the rotated cubes
    GameObject[] tempCubes = new GameObject[16];
    
    // Store the rotated cubes in tempCubes (counterclockwise mapping)
    tempCubes[0] = cubes[3];
    tempCubes[1] = cubes[7];
    tempCubes[2] = cubes[11];
    tempCubes[3] = cubes[15];
    tempCubes[4] = cubes[2];
    tempCubes[5] = cubes[6];
    tempCubes[6] = cubes[10];
    tempCubes[7] = cubes[14];
    tempCubes[8] = cubes[1];
    tempCubes[9] = cubes[5];
    tempCubes[10] = cubes[9];
    tempCubes[11] = cubes[13];
    tempCubes[12] = cubes[0];
    tempCubes[13] = cubes[4];
    tempCubes[14] = cubes[8];
    tempCubes[15] = cubes[12];

    // Apply the rotated cubes back to the original cubes array
    for (int i = 0; i < 16; i++)
    {
        cubes[i] = tempCubes[i];
    }
}

// Second face (indices 16–31)
    public void RotateSecondFaceCounterclockwise()
    {
        GameObject[] tempCubes = new GameObject[16];
        tempCubes[0] = cubes[19];
        tempCubes[1] = cubes[23];
        tempCubes[2] = cubes[27];
        tempCubes[3] = cubes[31];
        tempCubes[4] = cubes[18];
        tempCubes[5] = cubes[22];
        tempCubes[6] = cubes[26];
        tempCubes[7] = cubes[30];
        tempCubes[8] = cubes[17];
        tempCubes[9] = cubes[21];
        tempCubes[10] = cubes[25];
        tempCubes[11] = cubes[29];
        tempCubes[12] = cubes[16];
        tempCubes[13] = cubes[20];
        tempCubes[14] = cubes[24];
        tempCubes[15] = cubes[28];

        for (int i = 0; i < 16; i++)
        {
            cubes[16 + i] = tempCubes[i];
        }
    }

    // Third face (indices 32–47)
    public void RotateThirdFaceCounterclockwise()
    {
        GameObject[] tempCubes = new GameObject[16];
        tempCubes[0] = cubes[35];
        tempCubes[1] = cubes[39];
        tempCubes[2] = cubes[43];
        tempCubes[3] = cubes[47];
        tempCubes[4] = cubes[34];
        tempCubes[5] = cubes[38];
        tempCubes[6] = cubes[42];
        tempCubes[7] = cubes[46];
        tempCubes[8] = cubes[33];
        tempCubes[9] = cubes[37];
        tempCubes[10] = cubes[41];
        tempCubes[11] = cubes[45];
        tempCubes[12] = cubes[32];
        tempCubes[13] = cubes[36];
        tempCubes[14] = cubes[40];
        tempCubes[15] = cubes[44];

        for (int i = 0; i < 16; i++)
        {
            cubes[32 + i] = tempCubes[i];
        }
    }

    // Fourth face (indices 48–63)
    public void RotateFourthFaceCounterclockwise()
    {
        GameObject[] tempCubes = new GameObject[16];
        tempCubes[0] = cubes[51];
        tempCubes[1] = cubes[55];
        tempCubes[2] = cubes[59];
        tempCubes[3] = cubes[63];
        tempCubes[4] = cubes[50];
        tempCubes[5] = cubes[54];
        tempCubes[6] = cubes[58];
        tempCubes[7] = cubes[62];
        tempCubes[8] = cubes[49];
        tempCubes[9] = cubes[53];
        tempCubes[10] = cubes[57];
        tempCubes[11] = cubes[61];
        tempCubes[12] = cubes[48];
        tempCubes[13] = cubes[52];
        tempCubes[14] = cubes[56];
        tempCubes[15] = cubes[60];

        for (int i = 0; i < 16; i++)
        {
            cubes[48 + i] = tempCubes[i];
        }
    }

}
