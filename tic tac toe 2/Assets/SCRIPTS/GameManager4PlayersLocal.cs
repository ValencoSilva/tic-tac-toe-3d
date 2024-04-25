using UnityEngine;
using System.Collections.Generic; // This directive is necessary for Lists

public class GameManager4PlayersLocal : MonoBehaviour
{
    public enum PlayerType { Human1, Human2, Human3, Human4 }
    public PlayerType currentTurn = PlayerType.Human1;

    public GameObject[] squares; // Array of game objects that can be clicked on.

    private void Update()
    {
        CheckForObjectClickHuman1();
        CheckForObjectClickHuman2();
        CheckForObjectClickHuman3();
        CheckForObjectClickHuman4();
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

    void CheckForObjectClickHuman3()
    {
        if (Input.GetMouseButtonDown(0) && currentTurn == PlayerType.Human3)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                foreach (GameObject obj in squares)
                {
                    if (hit.transform.gameObject == obj && obj.GetComponent<Renderer>().material.color != Color.red && obj.GetComponent<Renderer>().material.color != Color.blue)
                    {
                        obj.GetComponent<Renderer>().material.color = Color.yellow; // Assuming red is the human's colour
                        ChangeTurn();
                        break; // Exit the loop once we've found the clicked object.
                    }
                }
            }
        }
    }

    void CheckForObjectClickHuman4()
    {
        if (Input.GetMouseButtonDown(0) && currentTurn == PlayerType.Human4)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                foreach (GameObject obj in squares)
                {
                    if (hit.transform.gameObject == obj && obj.GetComponent<Renderer>().material.color != Color.red && obj.GetComponent<Renderer>().material.color != Color.blue && obj.GetComponent<Renderer>().material.color != Color.yellow)
                    {
                        obj.GetComponent<Renderer>().material.color = Color.green; // Assuming red is the human's colour
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

        else if (currentTurn == PlayerType.Human2)
        {
            currentTurn = PlayerType.Human3;
            Debug.Log("It's the Human3's turn now!");
        }
        else if (currentTurn == PlayerType.Human3)
        {
            currentTurn = PlayerType.Human4;
            Debug.Log("It's the Human4's turn now!");
        }
        else if (currentTurn == PlayerType.Human4)
        {
            currentTurn = PlayerType.Human1;
            Debug.Log("It's the Human1's turn now!");
        }
    }
}
