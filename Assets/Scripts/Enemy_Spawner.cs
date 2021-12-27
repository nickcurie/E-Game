using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabsToSpawn;
    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnTimeRandom;

    private GameObject enemyPrefab;
    private float timeUntilNextSpawn;

    private void Start()
    {
        ResetSpawnTimer();
    }

    private void Update()
    {
        timeUntilNextSpawn -= Time.deltaTime;
        if(timeUntilNextSpawn <= 0.0f)
        {
            float num = Random.Range(0.0f, 1.0f);
            if (num < 0.8f){
                enemyPrefab = prefabsToSpawn[0];
            }
            else{
                enemyPrefab = prefabsToSpawn[1];
            }
            //Debug.Log(num);
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            ResetSpawnTimer();
        }

        if (Input.GetKey("`"))
        {
            Debug.Log("adasd");
            spawnTime = 200.0f;
        }
    }

    private void ResetSpawnTimer()
    {
        timeUntilNextSpawn = (float)(spawnTime + Random.Range(0, spawnTimeRandom * 100) / 100.0);
    }
}
