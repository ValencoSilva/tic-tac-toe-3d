using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceRotator1 : MonoBehaviour
{
    public List<GameObject> faceCubes; // List to hold the 16 cubes of the face
    public float rotationSpeed = 300f;  // Speed of the rotation
    private bool isRotating = false;

    // Store the original parent of each cube so we can re-parent them after the rotation
    private Dictionary<GameObject, Transform> originalParents = new Dictionary<GameObject, Transform>();

    // Function to rotate the entire face
    public void RotateFace(Vector3 rotationAxis)
    {
        FindObjectOfType<ScriptDisabler>().DisableScriptForSeconds(1.5f);
        if (!isRotating)
        {
            StartCoroutine(RotateFaceRoutine(rotationAxis));
        }
    }

    // Coroutine to smoothly rotate the entire face of cubes
    private IEnumerator RotateFaceRoutine(Vector3 rotationAxis)
    {
        isRotating = true;

        // Create a temporary pivot for rotation
        GameObject pivot = new GameObject("RotationPivot");
        pivot.transform.position = CalculateFaceCenter();  // Set the pivot at the center of the face

        // Save the original parent and parent the cubes to the pivot for rotation
        foreach (GameObject cube in faceCubes)
        {
            originalParents[cube] = cube.transform.parent;  // Store the original parent (face object)
            cube.transform.SetParent(pivot.transform);      // Parent to pivot for rotation
        }

        Quaternion startRotation = pivot.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(rotationAxis) * startRotation;

        float rotationDuration = 1f;  // Adjust the duration to your liking
        float rotationTime = 0f;

        // Smoothly rotate the cubes over time
        while (rotationTime < rotationDuration)
        {
            rotationTime += Time.deltaTime;
            pivot.transform.rotation = Quaternion.Lerp(startRotation, endRotation, rotationTime / rotationDuration);
            yield return null;
        }

        // Finalize the rotation
        pivot.transform.rotation = endRotation;

        // Re-parent the cubes back to their original face object
        foreach (GameObject cube in faceCubes)
        {
            cube.transform.SetParent(originalParents[cube]);  // Re-parent to the original parent
        }

        // Destroy the temporary pivot after the rotation
        Destroy(pivot);

        isRotating = false;
    }

    // Function to calculate the center of the face for pivot placement
    private Vector3 CalculateFaceCenter()
    {
        Vector3 center = Vector3.zero;
        foreach (GameObject cube in faceCubes)
        {
            center += cube.transform.position;
        }
        center /= faceCubes.Count;
        return center;
    }
}