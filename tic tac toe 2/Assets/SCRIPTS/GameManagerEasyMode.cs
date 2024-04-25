using UnityEngine;
using System.Collections.Generic; // This directive is necessary for Lists

public class GameManagerEasyMode : MonoBehaviour
{
    public enum PlayerType { Human, AI }
    public PlayerType currentTurn = PlayerType.Human;

    public GameObject[] clickableObjects; // Array of game objects that can be clicked on.

    private void Update()
    {
        CheckForObjectClick();

        if (currentTurn == PlayerType.AI)
        {
            AI_EasyTurn();
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
                        obj.GetComponent<Renderer>().material.color = Color.red; // Assuming red is the human's colour
                        ChangeTurn();
                        break; // Exit the loop once we've found the clicked object.
                    }
                }
            }
        }
    }

    void AI_EasyTurn()
    {
        // Make a list of available spots (those that haven't been chosen yet).
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
            GameObject randomSpot = availableSpots[randomIndex];
            randomSpot.GetComponent<Renderer>().material.color = Color.blue; // Assuming blue is the AI's colour
            ChangeTurn();
        }
    }

    void ChangeTurn()
    {
        if (currentTurn == PlayerType.Human)
        {
            currentTurn = PlayerType.AI;
            Debug.Log("It's the AI's turn now!");
        }
        else
        {
            currentTurn = PlayerType.Human;
            Debug.Log("It's the Human's turn now!");
        }
    }
}
