using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject painelPoderes;
    [SerializeField] private GameObject painelPoderes2;
    [SerializeField] public GameObject painelFaces;
    [SerializeField] public Collider[] collidersToDisable; // Array of colliders you want to disable (like BoxColliders)
    [SerializeField] private GameObject parentGameObject;

    [SerializeField] public Collider[] additionalColliders; // Extra colliders that are not children of parentGameObject

    [SerializeField] private List<Collider> allCollidersToDisable; // List to store all colliders (parent and additional)
    void Start()
    {
        // Initialize the list of colliders
        allCollidersToDisable = new List<Collider>();

        // Automatically find all colliders under the parent object (game board or similar)
        Collider[] parentColliders = parentGameObject.GetComponentsInChildren<Collider>();

        // Add the parent colliders to the list
        allCollidersToDisable.AddRange(parentColliders);

        // Add any additional colliders manually assigned in the Inspector
        if (additionalColliders != null && additionalColliders.Length > 0)
        {
            allCollidersToDisable.AddRange(additionalColliders);
        }

        // Ensure we found some colliders
        if (allCollidersToDisable.Count == 0)
        {
            Debug.LogWarning("No colliders found under parentGameObject or in additional colliders.");
        }
    }


    public void OnVoltar()
    {
        painelPoderes.SetActive(false);
        painelPoderes2.SetActive(false);
        painelFaces.SetActive(false);

        // Re-enable all colliders in the list
        foreach (Collider col in allCollidersToDisable)
        {
            col.enabled = true;  // Enable the collider again
        }

    }

    public void OnPoderes()
    {
        painelPoderes.SetActive(true);
        painelPoderes2.SetActive(true);

        // Disable all colliders in the list
        foreach (Collider col in allCollidersToDisable)
        {
            col.enabled = false;  // Disable the collider so it doesn't receive clicks
        }
    }

    public void OnGirarFace()
    {
        painelFaces.SetActive(true);
        painelPoderes.SetActive(false);
        painelPoderes2.SetActive(false);
    } 

    public void OnVoltarGirarFace()
    {
        painelFaces.SetActive(false);
        painelPoderes.SetActive(true);
        painelPoderes2.SetActive(true);
    } 
}
