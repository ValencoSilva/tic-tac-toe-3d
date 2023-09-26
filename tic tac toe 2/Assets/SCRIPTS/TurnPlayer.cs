using UnityEngine;

public class TurnPlayer : MonoBehaviour
{
    public enum PlayerType { Human, AI }
    public PlayerType currentTurn = PlayerType.Human;

    public GameObject[] clickableObjects;  // An array of clickable GameObjects

    private void Update()
    {
        // Checking for player clicking on any of the GameObjects
        if (Input.GetMouseButtonDown(0))  // 0 indicates left mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                foreach (GameObject obj in clickableObjects)
                {
                    if (hit.transform.gameObject == obj)
                    {
                        ChangeTurn();
                        break; // Exit the loop once we've found the clicked object
                    }
                }
            }
        }
    }

    private void ChangeTurn()
    {
        if (currentTurn == PlayerType.Human)
        {
            currentTurn = PlayerType.AI;
            Debug.Log("AI's turn");
        }
        else
        {
            currentTurn = PlayerType.Human;
            Debug.Log("Human's turn");
        }
    }
}
