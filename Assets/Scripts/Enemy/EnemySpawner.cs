using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    GameObject[] enemyInfo;

    // Start is called before the first frame update
    void Start()
    {
        InitializeSpawnPoints();
        EnemySpawn();
    }

    private void OnEnable()
    {
        if (enemyInfo != null)
        {
            RespawnEnemy();
        }
    }


    void InitializeSpawnPoints()
    {
        int childCount = transform.childCount;
        spawnPoint = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            spawnPoint[i] = transform.GetChild(i);
        }
    }

    public void EnemySpawn()
    {
        int rand = Random.Range(2, spawnPoint.Length);
        List<int> checkList = new List<int>();

        enemyInfo = new GameObject[rand];

        for (int index = 0; index < rand; index++)
        {
            enemyInfo[index] = GameManager.Instance.EnemySpanwer.Get((Random.Range(0, GameManager.Instance.EnemySpanwer.prefabs.Length)));

            int randPos;
            do
            {
                randPos = Random.Range(0, spawnPoint.Length);
            } while (checkList.Contains(randPos));

            checkList.Add(randPos);

            enemyInfo[index].transform.position = spawnPoint[randPos].position;
            enemyInfo[index].transform.parent = transform.parent;
        }
    }

    public void RespawnEnemy()
    {
        foreach (GameObject enemy in enemyInfo)
        {
            enemy.SetActive(false);
        }

        EnemySpawn();
    }
}
