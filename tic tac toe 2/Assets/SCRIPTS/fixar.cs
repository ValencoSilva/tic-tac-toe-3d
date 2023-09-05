using UnityEngine;

public class fixar : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the Camera's Transform
    public Vector3 offset; // Offset from the Camera

    // Update is called once per frame
    void Update()
    {
        // Set the board's position relative to the camera's position
        transform.position = cameraTransform.position + offset;
    }
}
