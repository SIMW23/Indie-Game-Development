using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public Transform leftPos, rightPos;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2);

        int randomNumber = Random.Range(0, 2);

        if(randomNumber == 0)
        {
            EnemyStats enemyStats = Instantiate(enemy, leftPos.position, Quaternion.identity).GetComponent<EnemyStats>();
            enemyStats.SpawnPosition = SpawnPosition.LeftScreen;
        }
        else
        {
            EnemyStats enemyStats = Instantiate(enemy, rightPos.position, Quaternion.identity).GetComponent<EnemyStats>();
            enemyStats.SpawnPosition = SpawnPosition.RightScreen;
        }

        StartCoroutine(SpawnEnemy());
    }
}
