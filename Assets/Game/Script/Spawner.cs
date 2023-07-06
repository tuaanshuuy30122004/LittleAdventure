using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnPoint;
    public GameObject[] EnemyToSpawn;
    private List<Character> spawnedList;
    private bool hasSpawn;


    private void Awake()
    {
        spawnedList = new List<Character>();
    }
    public void SpawnEnemy()
    {
        if (hasSpawn)
        {
            return;
        }
        hasSpawn = true;
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            GameObject t = Instantiate(EnemyToSpawn[i], spawnPoint[i].transform.position, Quaternion.identity);
            spawnedList.Add(t.GetComponent<Character>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SpawnEnemy();
        }
    }
}
