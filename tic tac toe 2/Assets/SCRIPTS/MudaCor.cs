using UnityEngine;

public class MudaCor: MonoBehaviour
{
    public int contador = 0;
    public GameObject cubeA; // Reference to the first cube
    public GameObject cubeB; // Reference to the second cube
    public Material newMaterialA; // Reference to the new material for cube A
    public Material newMaterialB; // Reference to the new material for cube B

    private MeshRenderer rendererA; // MeshRenderer component of cubeA
    private MeshRenderer rendererB; // MeshRenderer component of cubeB

    // Start is called before the first frame update
    void Start()
    {
        // Get the MeshRenderer components from the cubes
        rendererA = cubeA.GetComponent<MeshRenderer>();
        rendererB = cubeB.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(contador ==1){
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the mouse cursor on screen in the direction of the camera
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // If Cube A is clicked
                if (hit.collider.gameObject == cubeA)
                {
                    contador++;
                    ChangeCubeColors();
                    Debug.Log(contador);
                }
            }
        }
    }
    }

    void ChangeCubeColors()
    {
        // Change the materials of the cubes
        rendererA.material = newMaterialA;
        rendererB.material = newMaterialB;
    }
}