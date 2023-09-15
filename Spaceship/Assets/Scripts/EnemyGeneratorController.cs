using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{

    public GameObject horizontalEnemyPrefab;
    public int eachEnemyPoolSize;
    public int maxGenerationTime;
    private ObjectPool horizontalEnemyPool;
    private float maxX = 8.5f;
    private float maxY = 4f;
    private int remainingTime;

    // Start is called before the first frame update
    void Start()
    {
        this.horizontalEnemyPool = new ObjectPool(horizontalEnemyPrefab, eachEnemyPoolSize);
        this.remainingTime = this.maxGenerationTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (remainingTime == 0)
        {
            GameObject newEnemy = this.horizontalEnemyPool.GetFromPool();
            newEnemy.transform.position = this.getRandomPosition();
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
