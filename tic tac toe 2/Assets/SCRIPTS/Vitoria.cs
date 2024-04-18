using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitoria : MonoBehaviour
{
    public GameObject[] objects; // Assign this array in the inspector with your 64 objects
    public Color excludeColor = Color.red;

    private void Update()
    {
        // Check if Object 1, Object 2, Object 3, Object 4 have the same material color
        if (AreColorsSame(0, 1, 2, 3))
        {
            Debug.Log("Objects 1, 2, 3, and 4 have the same color.");
        }
        else
        {
           // Debug.Log("Objects 1, 2, 3, and 4 do not have the same color.");
        }
        

        // Check if Object 1, Object 5, Object 9, and Object 13 have the same material color
        if (AreColorsSame(0, 4, 8, 12))
        {
            Debug.Log("Objects 1, 5, 9, and 13 have the same color.");
        }
        else
        {
          //  Debug.Log("Objects 1, 5, 9, and 13 do not have the same color.");
        }
    }

    // Method to check if the colors of the specified indices are the same
    bool AreColorsSame(params int[] indices)
    {
        if (indices.Length < 2)
            return true; // Only one or no object, trivially true

        Color firstColor = objects[indices[0]].GetComponent<Renderer>().material.color;
        
        if (firstColor == excludeColor)
        {
            return false;
        }

        for (int i = 1; i < indices.Length; i++)
        {
            Color currentColor = objects[indices[i]].GetComponent<Renderer>().material.color;
            if (currentColor != firstColor)
                return false; // Colors don't match
        }

        return true; // All colors matched
    }
}
