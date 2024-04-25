using UnityEngine;
using System.Collections.Generic; // This directive is necessary for Lists

public class GameManager2PlayersLocal : MonoBehaviour
{
    public enum PlayerType { Human1, Human2 }
    public PlayerType currentTurn = PlayerType.Human1;

    public GameObject[] squares; // Array of game objects that can be clicked on.

    private void Update()
    {
        CheckForObjectClickHuman1();
        CheckForObjectClickHuman2();
    }

    void CheckForObjectClickHuman1()
    {
        if (Input.GetMouseButtonDown(0) && currentTurn == PlayerType.Human1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                foreach (GameObject obj in squares)
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

    void CheckForObjectClickHuman2()
    {
        if (Input.GetMouseButtonDown(0) && currentTurn == PlayerType.Human2)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                foreach (GameObject obj in squares)
                {
                    if (hit.transform.gameObject == obj && obj.GetComponent<Renderer>().material.color != Color.red && obj.GetComponent<Renderer>().material.color != Color.blue)
                    {
                        obj.GetComponent<Renderer>().material.color = Color.blue; // Assuming red is the human's colour
                        ChangeTurn();
                        break; // Exit the loop once we've found the clicked object.
                    }
                }
            }
        }
    }


    void ChangeTurn()
    {
        if (currentTurn == PlayerType.Human1)
        {
            currentTurn = PlayerType.Human2;
            Debug.Log("It's the Human2's turn now!");
        }
        else
        {
            currentTurn = PlayerType.Human1;
            Debug.Log("It's the Human1's turn now!");
        }
    }
}
