using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingletonClass<GameManager>
{
    public Player Player { get; set; }
    [field: SerializeField] public InputHandler InputHandler { get; set; }
    [field: SerializeField] public ButtonManager ButtonManager { get; set; }
    [SerializeField] public float Score { get {return score; } set { score = value; } }

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject[] arrowPrefabs;
    public List<Enemy> enemyList = new List<Enemy>();

    [SerializeField] float spawnRate;

    float score = 0;
 
    void Update()
    {
        foreach (var enemy in enemyList)
        {
            if (enemy.currentArrow.killDirection == InputHandler.PlayerInput)
            {
                enemy.KillSelf();
                score += 1f;
                GameManager.Instance.Player.IncreaseGauge();
                if (Random.Range(0, 100) <= 3)
                    GameManager.Instance.Player.AddHp();

                break;
            } else if (enemy.currentArrow.killDirection != InputHandler.PlayerInput && InputHandler.PlayerInput != DirectionType.None)
            {
                enemy.KillSelf();
                GameManager.Instance.Player.TakeDmg(); // deduct player hp
            }
        }
        GameManager.Instance.InputHandler.PlayerInput = DirectionType.None;
        score += 0.001f;
    }

    public void Die()
    {
        CancelInvoke(nameof(SpawnEnemy));
        SceneManager.LoadScene(0); // return to menu
    }

    public void StartSpawning()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0, spawnRate);
    }

    void SpawnEnemy()
    {
        Vector2 spawnPoint = new Vector2(2, 6);
        GameObject newEnemy = (GameObject)Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        AddEnemyToList(newEnemy.GetComponent<Enemy>());

        GameObject newArrow = null;

        Vector2 ArrowPos = new Vector2(spawnPoint.x - 1.5f, spawnPoint.y);

        newArrow = Instantiate(arrowPrefabs[Random.Range(0, arrowPrefabs.Length)], ArrowPos, Quaternion.identity);

        newArrow.transform.parent = newEnemy.transform;

        // Setting current arrow

        Enemy enemy = newEnemy.GetComponent<Enemy>();
        Arrow arrow = newArrow.GetComponent<Arrow>();

        enemy.currentArrow = arrow;
    }

    public void AddEnemyToList(Enemy enemy)
    {
        enemyList.Add(enemy);
    }

    public void RemoveEnemyFromList(Enemy e)
    {
        enemyList.Remove(e);
    }
}
