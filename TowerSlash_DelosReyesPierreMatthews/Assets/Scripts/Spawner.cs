using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : GenericSingletonClass<Spawner>
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<Enemy> enemyList = new List<Enemy>();

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        GameObject newEnemy = (GameObject)Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        AddEnemyToList(newEnemy.GetComponent<Enemy>());
    }

    void AddEnemyToList(Enemy enemy)
    {
        enemyList.Add(enemy);
    }
}
