using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartBomb : MonoBehaviour
{
    [SerializeField]
    private Camera gameCamera;
    [SerializeField]
    private ParticleSystem deathParticleSystem;

    private float lastBombTime;
    private float bombCooldown = 1f;
    private NPCSpawner nPCSpawner;
    private Plane[] planes;

    private void Start()
    {
        lastBombTime = Time.time;
        nPCSpawner = GameObject.FindGameObjectWithTag("Scripts").GetComponent<NPCSpawner>();
    }

    void Update()
    {
        if ((Time.time - lastBombTime > bombCooldown) && Input.GetAxisRaw("Bomb") != 0 && GameObject.FindGameObjectWithTag("Scripts").GetComponent<PlayerStats>().smartBombs > 0)
        {
            var spawnedNPC = GameObject.FindGameObjectWithTag("Scripts").GetComponent<NPCSpawner>().spawnedNPC;
            planes = GeometryUtility.CalculateFrustumPlanes(gameCamera);
            for (int i = spawnedNPC.Count - 1; i >= 0; i--)
            {
                if (GeometryUtility.TestPlanesAABB(planes, spawnedNPC[i].GetComponent<Collider2D>().bounds))
                {
                    GameObject.FindGameObjectWithTag("Scripts").GetComponent<PlayerStats>().addScore(spawnedNPC[i].GetComponent<NPC>().score);
                    Instantiate(deathParticleSystem, spawnedNPC[i].transform.position, Quaternion.identity);
                    Destroy(spawnedNPC[i]);
                }
            }
            GameObject.FindGameObjectWithTag("Scripts").GetComponent<PlayerStats>().addBomb(-1);
            nPCSpawner.StartCoroutine(GameObject.FindGameObjectWithTag("Scripts").GetComponent<NPCSpawner>().ClearDeadNPC());
            lastBombTime = Time.time;
        }          
    }

    /*void Update()
    {
        if ((Time.time - lastBombTime > bombCooldown) && Input.GetAxisRaw("Fire") != 0 && GameObject.FindGameObjectWithTag("Scripts").GetComponent<PlayerStats>().smartBombs > 0)
        {
            var spawnedNPC = GameObject.FindGameObjectWithTag("Scripts").GetComponent<NPCSpawner>().spawnedNPC;
            planes = GeometryUtility.CalculateFrustumPlanes(gameCamera);
            for (int i = spawnedNPC.Count - 1; i >= 0; i--)
            {
                Destroy(spawnedNPC[i]);
                spawnedNPC.RemoveAll(GameObject => GeometryUtility.TestPlanesAABB(planes, spawnedNPC[i].GetComponent<Collider2D>().bounds));
            }
        }
    }*/
}
