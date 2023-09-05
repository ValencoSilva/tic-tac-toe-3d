using UnityEngine;

public class testeia : MonoBehaviour
{
    public GameObject[] cubes; // Reference to the cubes
    public Material playerMaterial; // Reference to the new material for player's cube
    public Material aiMaterial; // Reference to the new material for AI's cube

    // Start is called before the first frame update
    void Start()
    {
        // Make sure cubes have MeshRenderer components
        foreach(GameObject cube in cubes)
        {
            if(cube.GetComponent<MeshRenderer>() == null)
            {
                cube.AddComponent<MeshRenderer>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Detect mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the mouse cursor on screen in the direction of the camera
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < cubes.Length; i++)
                {
                    if (hit.collider.gameObject == cubes[i])
                    {
                        // Change the color of the selected cube
                        cubes[i].GetComponent<MeshRenderer>().material = playerMaterial;
                        
                        // Call the AI's turn
                        AITurn(i);
                        break;
                    }
                }
            }
        }
    }

    void AITurn(int playerCubeIndex)
    {
        // Find remaining cubes the AI can choose from
        int aiCubeIndex;
        do
        {
            aiCubeIndex = Random.Range(0, cubes.Length);
        } while (aiCubeIndex == playerCubeIndex); // Make sure the AI doesn't choose the player's cube

        // Change the color of the AI's cube
        cubes[aiCubeIndex].GetComponent<MeshRenderer>().material = aiMaterial;
    }
}
