using UnityEngine;

public class Vitoria : MonoBehaviour
{
    public bool CheckForVictory(SpriteRenderer spriteToCheck)
    {
        SpriteRenderer[,,] allSprites = new SpriteRenderer[4, 4, 4];
        
        SpriteCoordinates[] spriteCoords = FindObjectsOfType<SpriteCoordinates>();

        // Fill the array
        foreach (SpriteCoordinates sCoord in spriteCoords)
        {
            Vector3Int coord = sCoord.coords;
            allSprites[coord.x, coord.y, coord.z] = sCoord.GetComponent<SpriteRenderer>();
        }
        
        // Check rows, columns, depths, and diagonals - example for rows:
        for (int z = 0; z < 4; z++)
        {
            for (int y = 0; y < 4; y++)
            {
                int matchCount = 0;
                for (int x = 0; x < 4; x++)
                {
                    if (allSprites[x, y, z] && allSprites[x, y, z].sprite == spriteToCheck.sprite)
                    {
                        matchCount++;
                    }
                }

                if (matchCount == 4) 
                {
                    Debug.Log("Win");
                    return true;
                }
            }
        }

        // Similar checks for columns, depths, and diagonals can be added...

        return false;
    }
}
