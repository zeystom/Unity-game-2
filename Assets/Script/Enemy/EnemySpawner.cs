using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static int enemyCounter;
    public GameObject enemy;
    public GameObject boss;
    int count = 5;
    int mincount = 1;
    int maxcount = 10;
    public List<Transform> spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        count = Random.Range(mincount, maxcount);
        for (int i = 0; i < count; i++) 
        {
            var rndPoint = Random.Range(0, spawnPoints.Count);
            SpawnEnemies(spawnPoints[rndPoint]);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyCounter >= count)
        {
            Instantiate(boss,transform.position,Quaternion.identity);
            enemyCounter = 0;
        }
    }

    void SpawnEnemies(Transform point)
    {
        Instantiate(enemy, GetRandomPoint(point.position,3), Quaternion.identity);
    }
    public Vector3 GetRandomPoint(Vector3 transpos, float radius)
    {
        return transpos + (Random.insideUnitSphere * radius);
    }
}
