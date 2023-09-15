using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{

    public GameObject horizontalEnemyPrefab;
    public GameObject sineEnemyPrefab;
    public int eachEnemyPoolSize;
    public int maxGenerationTime;
    private ObjectPool horizontalEnemyPool;
    private ObjectPool sineEnemyPool;
    private float maxX = 8.5f;
    private float maxY = 4f;
    private int remainingTime;

    // Start is called before the first frame update
    void Start()
    {
        this.horizontalEnemyPool = new ObjectPool(horizontalEnemyPrefab, eachEnemyPoolSize);
        this.sineEnemyPool = new ObjectPool(sineEnemyPrefab, eachEnemyPoolSize);
        this.remainingTime = this.maxGenerationTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (remainingTime == 0)
        {
            float randomEnemyTipe = UnityEngine.Random.Range(0, 2);
            Vector3 randomPosition = this.getRandomPosition();
            GameObject newEnemy;
            if (randomEnemyTipe == 0)
                newEnemy = this.horizontalEnemyPool.GetFromPool();
            else if (randomEnemyTipe == 1)
            {
                newEnemy = this.sineEnemyPool.GetFromPool();
                newEnemy.GetComponent<SineEnemyController>().initialYPosition = randomPosition.y;
            }
            else
            {
                newEnemy = this.sineEnemyPool.GetFromPool();
                newEnemy.GetComponent<SineEnemyController>().initialYPosition = randomPosition.y;
            }
            newEnemy.transform.position = randomPosition;
            remainingTime = maxGenerationTime + 1;
        }
        remainingTime--;
    }

    private Vector3 getRandomPosition()
    {
        float randomY = UnityEngine.Random.Range(-maxY, maxY);
        Vector3 newPosition = new Vector3(maxX, randomY, 0);
        return newPosition;
    }
}
