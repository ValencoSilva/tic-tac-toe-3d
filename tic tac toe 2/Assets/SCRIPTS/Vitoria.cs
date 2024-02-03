using UnityEngine;

public class Vitoria : MonoBehaviour
{
    public GameObject[,,] gameBoard; // The 4x4x4 game board

    public Material player1Material;
    public Material player2Material;

    void Start()
    {
        // Initialize your gameBoard here or via the Inspector
    }

    public bool CheckForHorizontalVictory()
    {
        for (int y = 0; y < 4; y++) // Each horizontal layer
        {
            for (int z = 0; z < 4; z++) // Each row in a layer
            {
                if (IsRowSame(gameBoard[0, y, z], gameBoard[1, y, z], gameBoard[2, y, z], gameBoard[3, y, z]))
                {
                    return true; // Victory found in this row
                }
            }
        }
        return false; // No victory found
    }

    bool IsRowSame(GameObject a, GameObject b, GameObject c, GameObject d)
    {
        Material matA = a.GetComponent<Renderer>().material;
        Material matB = b.GetComponent<Renderer>().material;
        Material matC = c.GetComponent<Renderer>().material;
        Material matD = d.GetComponent<Renderer>().material;

        return matA == matB && matB == matC && matC == matD;
    }
}