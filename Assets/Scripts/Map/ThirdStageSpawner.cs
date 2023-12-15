using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ThirdStageSpawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public float timer = 3f;

    void Start()
    {
        InitializeSpawnPoints();
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

    void Update()                       
     {
         timer += Time.deltaTime;

         if(timer > 3f)
         {
             timer = 0;
            EnemySpawn(); 
         }
     }
  


    public void EnemySpawn()
    {
        int randPos = Random.Range(0, spawnPoint.Length);

        GameObject enemy = GameManager.Instance.ThEnemySpanwer.Get((Random.Range(0, 3)));
        enemy.transform.position = spawnPoint[randPos].transform.position;
    }

}
