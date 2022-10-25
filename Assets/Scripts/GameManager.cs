using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject wallBrickPrefab;
    public GameObject enemyPrefab;
    public GameObject enemiesParent;
    public TMPro.TextMeshProUGUI score;
    private int points = 0;

    private void Awake()
    {
        //Spawn walls
        float wallBrickStartPosX = 6.7f - 9.6f;
        float wallBrickStartPosY = 2.15f - 5.4f;
        float spaceBetweenWalls = 1.75f;
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

        //Spawn enemies
        float enemiesStartPosX = 6.7f - 9.6f;
        float enemiesStartPosY = 5.15f - 5.4f;
        float spaceBetweenEnemiesX = 0.46f;
        float spaceBetweenEnemiesY = 0.6f;
        float enemiesWidth = 0.125f;
        float enemiesHeight = 0.125f;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                Vector2 enemyPosition = new Vector2(enemiesStartPosX + j * enemiesWidth + j * spaceBetweenEnemiesX, enemiesStartPosY + i * enemiesHeight + i * spaceBetweenEnemiesY);
                GameObject enemy = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
                enemy.transform.parent = enemiesParent.transform;
                enemy.GetComponent<StandardEnemy>().movementOffset = i * 1.5f + j * 0.1f;

                if (i == 2 || i == 3)
                {
                    enemy.GetComponent<StandardEnemy>().points = 20;
                }
                if (i == 4)
                {
                    enemy.GetComponent<StandardEnemy>().points = 30;
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SpaceInvaders");
        }
    }

    public void PlayerDestroyed()
    {

    }

    public void PlayerKilledEnemy(int points)
    {
        this.points += points;
        score.text = this.points.ToString("D4");

        int enemiesCount = enemiesParent.transform.childCount;
        float speed = (55 - enemiesCount) * 0.1f;
        for (int i = 0; i < enemiesCount; i++)
        {
            GameObject enemyGO = enemiesParent.transform.GetChild(i).gameObject;
            Enemy enemy = enemyGO.GetComponent<Enemy>();
            enemy.speed = speed;
        }
    }
    public void PlayerLost()
    {
        Time.timeScale = 0f;
    }
}
