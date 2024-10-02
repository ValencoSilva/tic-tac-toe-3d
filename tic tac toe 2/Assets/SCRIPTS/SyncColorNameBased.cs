using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncColorNameBased : MonoBehaviour
{
    public Transform parentOfQuadrados;  // The parent object containing all Quadrado objects
    public Transform parentOfCubes;      // The parent object containing all Cube objects

    private List<CubePair> cubePairs = new List<CubePair>(); // Dynamically generated list of cube pairs

    [System.Serializable]
    public class CubePair
    {
        public GameObject quadrado;  // The primary object (Quadrado) whose color will be synced
        public GameObject cube;      // The secondary object (Cube) to sync color with Quadrado
    }

    void Start()
    {
        // Find and associate cubes automatically based on naming convention
        foreach (Transform quadrado in parentOfQuadrados)
        {
            // Find corresponding Cube using naming convention
            string correspondingCubeName = quadrado.name.Replace("Quadrado", "Cube");
            Transform correspondingCube = parentOfCubes.Find(correspondingCubeName);

            if (correspondingCube != null)
            {
                CubePair pair = new CubePair
                {
                    quadrado = quadrado.gameObject,
                    cube = correspondingCube.gameObject
                };
                cubePairs.Add(pair);
            }
            else
            {
                Debug.LogWarning("No corresponding Cube found for: " + quadrado.name);
            }
        }

        // Sync the colors after pairing
        SyncCubes();
    }

    public void SyncCubes()
    {
        foreach (CubePair pair in cubePairs)
        {
            if (pair.quadrado != null && pair.cube != null)
            {
                // Sync the Cube's color with Quadrado
                pair.cube.GetComponent<Renderer>().material.color = pair.quadrado.GetComponent<Renderer>().material.color;
            }
        }
    }
}
