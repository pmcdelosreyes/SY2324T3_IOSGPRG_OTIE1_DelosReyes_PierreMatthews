using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] spawnablePrefabs;
    public List<Enemy> enemyList = new List<Enemy>();
    float randPosX;
    float randPosY;
    public UnityEvent OnWinEvent = new UnityEvent();

    private void Update()
    {
        if (enemyList.Count == 0 && OnWinEvent != null)
            OnWinEvent.Invoke();

        for (int i = 0; i < enemyList.Count; i++)
            if (enemyList[i].enemyHealth.curHp < 1)
            {
                enemyList.RemoveAt(i);
                enemyList[i].enemyHealth.Death();
            }
    }

    private void Start()
    {
        for (int i = 0; i < Random.Range(10, 20); i++)
        {
            Spawn();
            SpawnEnemies();
        }

        OnWinEvent.AddListener(AllEnemiesSlain);
        OnWinEvent.AddListener(GameManager.Instance.Winner);
    }

    void Spawn()
    {
        int randSpawnable = Random.Range(0, spawnablePrefabs.Length - 1);
        randPosX = Random.Range(-18, 18);
        randPosY = Random.Range(-18, 18);
        GameObject nSpawn = Instantiate(spawnablePrefabs[randSpawnable], new Vector3(randPosX, randPosY, 0), Quaternion.identity);

        if (nSpawn.GetComponent<Enemy>())
            enemyList.Add(nSpawn.GetComponent<Enemy>());
    }

    void SpawnEnemies()
    {
        randPosX = Random.Range(-18, 18);
        randPosY = Random.Range(-18, 18);
        GameObject nSpawn = Instantiate(spawnablePrefabs[5], new Vector3(randPosX, randPosY, 0), Quaternion.identity);

        if (nSpawn.GetComponent<Enemy>())
            enemyList.Add(nSpawn.GetComponent<Enemy>());
    }

    void AllEnemiesSlain()
    {
        OnWinEvent.RemoveListener(AllEnemiesSlain);
    }
}