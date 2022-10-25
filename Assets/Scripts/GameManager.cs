using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject wallBrickPrefab;

    private void Awake()
    {
        //Spawn walls
        float wallBrickStartPosX = 6.7f - 9.6f;
        float wallBrickStartPosY = 2.15f - 5.4f;
        float spaceBetweenWalls = 1.6f;
        float wallBrickWidth = 0.2f;
        float wallBrickHeight = 0.175f;

        for (int i=0; i<4; i++)
        {
            for(int j=0; j<4; j++)
            {
                for(int k=0; k<4; k++)
                {
                    if (!((k == 1 || k == 2) && j == 0))
                    {
                        Vector2 wallBrickPosition = new Vector2(wallBrickStartPosX + k * wallBrickWidth + i * spaceBetweenWalls, wallBrickStartPosY + j * wallBrickHeight);
                        GameObject wallBrick = Instantiate(wallBrickPrefab, wallBrickPosition, Quaternion.identity);
                    }
                }
            }
        }
    }
}
