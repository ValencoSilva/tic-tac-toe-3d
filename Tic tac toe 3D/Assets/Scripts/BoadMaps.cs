using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoadMaps : MonoBehaviour
{
    public GameObject cubeMaps;
   public float spacing = 2;

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
                    Instantiate(cubeMaps, position, Quaternion.identity);
                }
            }
        }
    }
}
