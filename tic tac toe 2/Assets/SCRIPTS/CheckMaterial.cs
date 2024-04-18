using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMaterial : MonoBehaviour
{
    public GameObject[] cubes; // Should be an array of 64 GameObjects (4x4x4)
    public Color excludeColor = Color.red;


    private void Update()
    {
        if (cubes.Length != 64)
        {
            Debug.LogError("Incorrect number of game objects. There should be exactly 64 objects.");
            return;
        }

        CheckAllWinningConditions();
    }

    private void CheckAllWinningConditions()
    {
        // Check all horizontal lines in each layer
        for (int layer = 0; layer < 4; layer++)
        {
            for (int row = 0; row < 4; row++)
            {
                if (AreColorsSame(layer * 16 + row * 4, layer * 16 + row * 4 + 1, layer * 16 + row * 4 + 2, layer * 16 + row * 4 + 3))
                {
                    Debug.Log($"Winning condition met at layer {layer + 1}, row {row + 1} (horizontal).");
                }
            }
        }

        // Check all vertical lines in each layer
        for (int layer = 0; layer < 4; layer++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (AreColorsSame(layer * 16 + col, layer * 16 + col + 4, layer * 16 + col + 8, layer * 16 + col + 12))
                {
                    Debug.Log($"Winning condition met at layer {layer + 1}, column {col + 1} (vertical).");
                }
            }
        }

        // Check 2D diagonals within each layer
        for (int layer = 0; layer < 4; layer++)
        {
            if (AreColorsSame(layer * 16, layer * 16 + 5, layer * 16 + 10, layer * 16 + 15))
            {
                Debug.Log($"Winning condition met at layer {layer + 1} on major diagonal.");
            }
            if (AreColorsSame(layer * 16 + 3, layer * 16 + 6, layer * 16 + 9, layer * 16 + 12))
            {
                Debug.Log($"Winning condition met at layer {layer + 1} on minor diagonal.");
            }
        }

        // Check vertical lines through all layers
        for (int index = 0; index < 16; index++)
        {
            if (AreColorsSame(index, index + 16, index + 32, index + 48))
            {
                Debug.Log($"Winning condition met through all layers at position {index % 4 + 1}, {index / 4 + 1} (vertical through layers).");
            }
        }

        // Check 3D diagonals
        if (AreColorsSame(0, 21, 42, 63))
        {
            Debug.Log("Winning condition met on 3D diagonal from top-left-front to bottom-right-back.");
        }
        if (AreColorsSame(3, 22, 41, 60))
        {
            Debug.Log("Winning condition met on 3D diagonal from top-right-front to bottom-left-back.");
        }
        if (AreColorsSame(12, 25, 38, 51))
        {
            Debug.Log("Winning condition met on 3D diagonal from top-left-back to bottom-right-front.");
        }
        if (AreColorsSame(15, 26, 37, 48))
        {
            Debug.Log("Winning condition met on 3D diagonal from top-right-back to bottom-left-front.");
        }
    }

    // Check if specified GameObjects have the same material color
    private bool AreColorsSame(params int[] indices)
    {
        Color firstColor = cubes[indices[0]].GetComponent<Renderer>().material.color;
        if (firstColor == excludeColor)
        {
            return false;
        }

        for (int i = 1; i < indices.Length; i++)
        {
            if (cubes[indices[i]].GetComponent<Renderer>().material.color != firstColor)
                return false;
        }
        return true;
    }
}
