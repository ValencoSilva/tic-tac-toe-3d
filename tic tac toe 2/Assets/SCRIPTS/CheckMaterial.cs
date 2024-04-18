using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMaterial : MonoBehaviour
{
       [SerializeField]
    private GameObject[] objectsToCheck;
    // Public variable to set the excluded color in the Unity Inspector
    public Color excludeColor = Color.red; // Example: Exclude red materials

    public bool CheckIfMaterialColorsAreTheSame(GameObject[] objectsToCheck)
    {
        if (objectsToCheck.Length < 2)
        {
            Debug.LogError("Need at least two objects to compare materials.");
            return false;
        }


        // Get the first object's material color.
        Color firstObjectColor = objectsToCheck[0].GetComponent<Renderer>().material.color;
        if (firstObjectColor == excludeColor)
        {
            Debug.Log("Branco");
            return false;
        }

        for (int i = 1; i < objectsToCheck.Length; i++)
        {
            Color currentColor = objectsToCheck[i].GetComponent<Renderer>().material.color;

            // If any material's color doesn't match the first object's material color, return false.
            if (currentColor != firstObjectColor)
            {
                return false;
            }
    
        }

        // All materials' colors match.

        return true;
    }

    void Update()
    {
        bool result = CheckIfMaterialColorsAreTheSame(objectsToCheck);

        if(result)
        {
            Debug.Log("All objects have the same material color.");
        }
        else
        {
            Debug.Log("Not all objects have the same material color.");
        }
    }
}

