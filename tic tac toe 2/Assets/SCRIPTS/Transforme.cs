using UnityEngine;

public class Transforme : MonoBehaviour
{
    public Sprite mySprite;  // Assign your sprite in the inspector.

    void Start()
    {
        // Create a new GameObject
        GameObject newGameObject = new GameObject("MySpriteObject");

        // Add a SpriteRenderer component to it
        SpriteRenderer sr = newGameObject.AddComponent<SpriteRenderer>();

        // Assign the sprite to the SpriteRenderer
        sr.sprite = mySprite;

        // Optionally: Position the GameObject in the world (example: placing it at the origin)
        newGameObject.transform.position = Vector3.zero;
    }
}