using UnityEngine;

public class MarkPlacer : MonoBehaviour
{
    private GameBoard gameBoard;

    // Colors for players
    public Color player1Color = Color.red;
    public Color player2Color = Color.blue;

    private int currentPlayer = 1;  // Start with player 1

    void Start()
    {
        gameBoard = new GameBoard();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // If the left mouse button was clicked
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))  // If the ray hit a game object
            {
                Vector3 position = hit.transform.position;
                int x = Mathf.RoundToInt(position.x);
                int y = Mathf.RoundToInt(position.y);
                int z = Mathf.RoundToInt(position.z);

                if (gameBoard.PlaceMark(currentPlayer, x, y, z))  // If the spot is not already occupied
                {
                    // Change the color of the cube to indicate the player's mark
                    hit.transform.GetComponent<Renderer>().material.color =
                        currentPlayer == 1 ? player1Color : player2Color;

                    // Switch to the other player
                    currentPlayer = 3 - currentPlayer;  // This switches between 1 and 2
                }
            }
        }
    }
}
