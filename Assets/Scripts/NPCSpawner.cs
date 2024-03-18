using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform terrains;
    [SerializeField]
    private GameObject NPC;

    public List<GameObject> spawnedNPC;

    private float enemyWave = 1;
    private float randX;
    private float randY;
    private Transform[] terrainTransforms;
    private Vector2 spawnLocation;
    private float enemiesSpawned;
    private float lastSpawnTime;
    private float spawnTimer = 0.1f;

    void Start ()
    {
        terrainTransforms = new Transform[terrains.childCount];
        for (int i = 0; i < terrains.childCount; i++)
        {
            terrainTransforms[i] = terrains.GetChild(i);
        }
        lastSpawnTime = Time.time;
    }
	
	void Update ()
    {
        if ((Time.time - lastSpawnTime > spawnTimer) && enemiesSpawned < 50f + enemyWave * 5f)
        {
            randX = terrainTransforms[Random.Range(0, 3)].position.x + Random.Range(-40.96f, 40.96f);
            randY = Random.Range(-2f, 2f);
            spawnLocation = new Vector2(randX, randY);
            GameObject NPCInstance = Instantiate(NPC, spawnLocation, Quaternion.identity);
            spawnedNPC.Add(NPCInstance);
            enemiesSpawned += 1;
            lastSpawnTime = Time.time;
        }
    }

    public IEnumerator ClearDeadNPC()
    {
        yield return new WaitForSeconds(0.1f);
        spawnedNPC.RemoveAll(GameObject => GameObject == null);
    }

    void newWave()
    {
        enemyWave += 1;
        enemiesSpawned = 0;
    }
}
