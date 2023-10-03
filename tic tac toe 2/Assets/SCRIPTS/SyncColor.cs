using UnityEngine;

public class SyncColor : MonoBehaviour
{
    public GameObject cubeA;      // Assign your Cube A in the Inspector
    public GameObject cubeA1;     // Assign your Cube A.1 in the Inspector

    private Color previousColorA; // To store the previous color of Cube A

    void Update()
    {
        // Check if the color of Cube A has changed
        if (cubeA.GetComponent<Renderer>().material.color != previousColorA)
        {
            // Update the color of Cube A.1
            cubeA1.GetComponent<Renderer>().material.color = cubeA.GetComponent<Renderer>().material.color;

            // Update the previous color
            previousColorA = cubeA.GetComponent<Renderer>().material.color;
        }
    }
}
