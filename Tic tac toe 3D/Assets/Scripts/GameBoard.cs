using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    private int[,,] board;
    public const int Size = 4;

    public GameBoard()
    {
        board = new int[Size, Size, Size];
        ResetBoard();
    }

    public void ResetBoard()
    {
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                for (int z = 0; z < Size; z++)
                {
                    board[x, y, z] = 0;
                }
            }
        }
    }

    public bool PlaceMark(int player, int x, int y, int z)
    {
        // Check if the spot is already occupied
        if (board[x, y, z] != 0)
        {
            return false;
        }

        board[x, y, z] = player;
        return true;
    }

    public int GetMark(int x, int y, int z)
    {
        return board[x, y, z];
    }
     public bool CheckWinCondition(int x, int y, int z, int mark)
    {
        // Check along x-axis
        if (IsLine(x, y, z, 1, 0, 0, mark))
            return true;
        
        // Check along y-axis
        if (IsLine(x, y, z, 0, 1, 0, mark))
            return true;

        // Check along z-axis
        if (IsLine(x, y, z, 0, 0, 1, mark))
            return true;

        // Check along the 4 diagonals
        if (IsLine(x, y, z, 1, 1, 0, mark))
            return true;
        if (IsLine(x, y, z, 1, -1, 0, mark))
            return true;
        if (IsLine(x, y, z, 1, 0, 1, mark))
            return true;
        if (IsLine(x, y, z, 1, 0, -1, mark))
            return true;

        return false;
    }
      private bool IsLine(int x, int y, int z, int dx, int dy, int dz, int mark)
    {
        for (int i = 0; i < Size; i++)
        {
            int nx = x + i * dx;
            int ny = y + i * dy;
            int nz = z + i * dz;

            // check if the position is inside the board and contains the player's mark
            if (nx < 0 || nx >= Size || ny < 0 || ny >= Size || nz < 0 || nz >= Size)
                return false;
            if (board[nx, ny, nz] != mark)
                return false;
        }

        return true;
    }
}


