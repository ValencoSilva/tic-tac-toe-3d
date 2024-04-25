using UnityEngine;
using System.Collections.Generic;

public class HardMode : MonoBehaviour
{
    public enum PlayerType { Human, AI }
    public PlayerType currentTurn = PlayerType.Human;

    public GameObject[] clickableObjects; // Array of game objects that can be clicked on.

    void Update()
    {
        CheckForObjectClick();

        if (currentTurn == PlayerType.AI)
        {
            AI_IntelligentTurn();
        }
    }

    void CheckForObjectClick()
    {
        if (Input.GetMouseButtonDown(0) && currentTurn == PlayerType.Human)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                foreach (GameObject obj in clickableObjects)
                {
                    if (hit.transform.gameObject == obj && obj.GetComponent<Renderer>().material.color != Color.red && obj.GetComponent<Renderer>().material.color != Color.blue)
                    {
                        obj.GetComponent<Renderer>().material.color = Color.red; // Human color
                        ChangeTurn();
                        break;
                    }
                }
            }
        }
    }

    void AI_IntelligentTurn()
    {
        GameObject criticalBlock = FindCriticalBlock(); // This will find a block if the player is about to win
        
        if (criticalBlock != null)
        {
            criticalBlock.GetComponent<Renderer>().material.color = Color.blue; // AI blocks the player
        }
        else
        {
            // Make a random move
            List<GameObject> availableSpots = new List<GameObject>();
            foreach (GameObject obj in clickableObjects)
            {
                if (obj.GetComponent<Renderer>().material.color != Color.red && obj.GetComponent<Renderer>().material.color != Color.blue)
                {
                    availableSpots.Add(obj);
                }
            }

            if (availableSpots.Count > 0)
            {
                int randomIndex = Random.Range(0, availableSpots.Count);
                availableSpots[randomIndex].GetComponent<Renderer>().material.color = Color.blue; // AI's color
            }
        }

        ChangeTurn();
    }

    GameObject FindCriticalBlock()
    {
        // Implement a method to find a critical block move
        // This is a placeholder for where you'd check for three in a row with an open fourth spot
        return null; // Return null if no critical block found
    }

    void ChangeTurn()
    {
        currentTurn = (currentTurn == PlayerType.Human) ? PlayerType.AI : PlayerType.Human;
        Debug.Log($"It's now {currentTurn}'s turn.");
    }
}
