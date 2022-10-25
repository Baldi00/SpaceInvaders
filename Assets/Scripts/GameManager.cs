using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool IsGameRunning;

    public GameObject wallBrickPrefab;
    public GameObject enemyPrefab;
    public GameObject ufoPrefab;
    public GameObject enemyBulletPrefab;
    public GameObject enemiesParent;
    public GameObject wallsParent;
    public GameObject player;
    public GameObject lives;
    public GameObject youLose;
    public GameObject youWin;
    public TMPro.TextMeshProUGUI score;
    public float timeBetweenEnemyBullets = 12f;
    public int iterationsBetweenUfo = 15;
    private float remainingTimeBetweenBullets;
    private int iterationsForUfo = 1;
    private int points = 0;
    private GameObject[] enemiesColumns;
    private int enemiesCount = 55;

    private void Awake()
    {
        IsGameRunning = true;
        remainingTimeBetweenBullets = timeBetweenEnemyBullets;

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
                        wallBrick.transform.parent = wallsParent.transform;
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

        enemiesColumns = new GameObject[11];

        for(int i=0; i<11; i++)
        {
            enemiesColumns[i] = new GameObject();
            enemiesColumns[i].transform.parent = enemiesParent.transform;
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                Vector2 enemyPosition = new Vector2(enemiesStartPosX + j * enemiesWidth + j * spaceBetweenEnemiesX, enemiesStartPosY + i * enemiesHeight + i * spaceBetweenEnemiesY);
                GameObject enemy = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
                enemy.transform.parent = enemiesColumns[j].transform;
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
        if (IsGameRunning)
        {
            remainingTimeBetweenBullets -= 10 * Time.deltaTime;

            //Bullet from enemies spawn
            if (remainingTimeBetweenBullets <= 0 && enemiesCount > 1)
            {
                int column;
                do
                {
                    column = Random.Range(0, 10);
                } while (enemiesColumns[column].transform.childCount <= 0);

                Vector2 bulletSpawnPosition = enemiesColumns[column].transform.GetChild(0).gameObject.transform.position;
                bulletSpawnPosition.y -= 0.1f;

                Instantiate(enemyBulletPrefab,
                    enemiesColumns[column].transform.GetChild(0).gameObject.transform.position,
                    Quaternion.identity);

                remainingTimeBetweenBullets = timeBetweenEnemyBullets;
                iterationsForUfo++;
            }

            if(iterationsForUfo % iterationsBetweenUfo == 0)
            {
                GameObject ufo = Instantiate(ufoPrefab, new Vector2(4f, 3.25f), Quaternion.identity);
                ufo.GetComponent<UFOEnemy>().points = Random.Range(1, 3) * 50;
                SoundManager.Instance.PlayUfo();
                iterationsForUfo++;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            IsGameRunning = true;
            Time.timeScale = 1;
            SceneManager.LoadScene("SpaceInvaders");
        }
    }

    public void PlayerDestroyed()
    {
        IsGameRunning = false;
        int livesCount = lives.transform.childCount;
        Destroy(lives.transform.GetChild(0).gameObject);
        
        if (livesCount <= 1)
            PlayerLost();
        else
            StartCoroutine(WaitThenSpawnNewPlayer());

    }

    public void PlayerKilledEnemy(int points, string enemyTag)
    {
        this.points += points;
        score.text = this.points.ToString("D4");

        if (enemyTag == "Enemy")
        {
            enemiesCount--;
        }

        if(enemiesCount <= 0)
        {
            PlayerWon();
            return;
        }

        float speed = Mathf.Min((55 - enemiesCount) * 0.13f, 5.35f);

        for(int i=0; i<11; i++)
        {
            for(int j=0; j<enemiesColumns[i].transform.childCount; j++)
            {
                GameObject enemyGO = enemiesColumns[i].transform.GetChild(j).gameObject;
                Enemy enemy = enemyGO.GetComponent<Enemy>();
                enemy.speed = speed;
            }
        }
    }
    public void PlayerLost()
    {
        IsGameRunning = false;
        Time.timeScale = 0;
        youLose.SetActive(true);
    }

    public void PlayerWon()
    {
        IsGameRunning = false;
        Time.timeScale = 0;
        youWin.SetActive(true);
    }

    IEnumerator WaitThenSpawnNewPlayer()
    {
        yield return new WaitForSeconds(1.5f);
        player.transform.position = new Vector2(0f, -3.9f);
        player.SetActive(true);
        IsGameRunning = true;
    }
}
