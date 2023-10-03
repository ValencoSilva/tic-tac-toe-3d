using UnityEngine;

public class BlockCor : MonoBehaviour
{
    private bool isClicked = false;  // Flag to check if the cube has already been clicked

    void OnMouseDown()
    {
        // If the cube is already clicked
        if (isClicked)
        {
            Debug.Log("Cube already selected, select another");
            return;
        }

        // Check the current color of the cube
        Color cubeColor = this.GetComponent<Renderer>().material.color;

        // If the cube is neither blue nor red, allow changing the colour (or your desired action)
        if (cubeColor != Color.blue && cubeColor != Color.red)
        {
            // Change to red for this example, you can adjust as needed
            this.GetComponent<Renderer>().material.color = Color.red;
            isClicked = true;  // Update the flag to indicate that this cube has been clicked
        }
        else
        {
            Debug.Log("Cube already selected, select another");
        }
    }
}
