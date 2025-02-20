using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstEnemy : MonoBehaviour
{
    public TransformsEnemy transformsEnemy;

    void OnTriggerEnter2D(Collider2D collision)
    {
        InstantiateEnemy();
    }

    private void InstantiateEnemy()
    {
    //GameObject currentEnemy = Instantiate(EnemyPrefab, transformsEnemy.TEnemy().position, Quaternion.identity);
    	var enemy = EnemyPool.Instance.GetEnemy();
        enemy.transform.position = transformsEnemy.TEnemy().position;
    }
}