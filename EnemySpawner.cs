using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{

    public GameObject spawn;
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 10f;
    public Transform spawnPosition;
    public bool setup;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator Spawner()
    {
        while (true)
        {
            Debug.Log("Spawner started");
            float delay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(delay);
            Instantiate(spawn, transform.position, Quaternion.identity);
            Debug.Log("Enemy spawned.");
        }
    }
}
