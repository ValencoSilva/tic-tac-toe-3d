using UnityEngine;

public class BoardVisualizer : MonoBehaviour
{
    public GameObject cubePrefab;
    
    public float spacing = 1.1f;

    void Start()
    {
        CreateBoard();
    }

    void CreateBoard()
    {
        for (int x = 0; x < GameBoard.Size; x++)
        {
            for (int y = 0; y < GameBoard.Size; y++)
            {
                for (int z = 0; z < GameBoard.Size; z++)
                {
                    // Create a new cube at this position
                    Vector3 position = new Vector3(x * spacing, y * spacing, z * spacing);
                    Instantiate(cubePrefab, position, Quaternion.identity);
                }
            }
        }
    }
}
