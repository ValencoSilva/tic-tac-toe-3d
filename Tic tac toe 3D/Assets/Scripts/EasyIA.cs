using System;
using System.Linq;
using System.Collections.Generic;

public class EasyIA 
{
    private int mark;
    private Random random;

    public EasyIA(int mark)
    {
        this.mark = mark;
        random = new Random();
    }

    public (int, int, int) CalculateMove(GameBoard board)
    {
        List<(int, int, int)> availableMoves = new List<(int, int, int)>();

        // Find all available spots
        for (int x = 0; x < GameBoard.Size; x++)
        {
            for (int y = 0; y < GameBoard.Size; y++)
            {
                for (int z = 0; z < GameBoard.Size; z++)
                {
                    if (board.GetMark(x, y, z) == 0)
                    {
                        availableMoves.Add((x, y, z));
                    }
                }
            }
        }

        // Pick a random move
        int index = random.Next(availableMoves.Count);
        return availableMoves[index];
    }
}
