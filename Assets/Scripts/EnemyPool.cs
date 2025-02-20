using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance { get; private set; }

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private int initialSize = 10;

    private Queue<Enemy> availableEnemies;

    void Awake()
    {
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        availableEnemies = new Queue<Enemy>(initialSize);

        for (int i = 0; i < initialSize; ++i)
        {
            var enemy = Instantiate(enemyPrefab, transform).GetComponent<Enemy>();
            enemy.gameObject.SetActive(false);
            availableEnemies.Enqueue(enemy);
        }
    }

    public Enemy GetEnemy()
    {
        if (availableEnemies.Count == 0)
        {
            ExpandPool();
        }

        var enemy = availableEnemies.Dequeue();
        enemy.gameObject.SetActive(true);
        return enemy;
    }

    private void ExpandPool()
    {
        var newEnemy = Instantiate(enemyPrefab, transform).GetComponent<Enemy>();
        newEnemy.gameObject.SetActive(false);
        availableEnemies.Enqueue(newEnemy);
    }

    public void ReturnEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        availableEnemies.Enqueue(enemy);
    }
}