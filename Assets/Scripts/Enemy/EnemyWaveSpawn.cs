using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawn : MonoBehaviour
{
    public EnemyWave[] waves;
    [SerializeField] private bool waveClear = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnWave()
    {
        foreach (EnemyWave wave in waves)
        {
            for (int x = 0; x < wave.enemies.Length; x++)
            {
                //instantiating enemies
                //GameObject obj = Instantiate(wave.enemies[x], wave.spawnpoints[x].position, Quaternion.identity);

                //culling enemies
                //wave.enemies[x].transform.position = wave.spawnpoints[x].position;
                wave.enemies[x].SetActive(true);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}