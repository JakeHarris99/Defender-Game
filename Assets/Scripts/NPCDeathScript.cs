using UnityEngine;

public class NPCDeathScript : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem deathParticleSystem;

    private NPCSpawner nPCSpawner;

    private void Start()
    {
        nPCSpawner = GameObject.FindGameObjectWithTag("Scripts").GetComponent<NPCSpawner>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            GameObject.FindGameObjectWithTag("Scripts").GetComponent<PlayerStats>().addScore(gameObject.GetComponent<NPC>().score);
            Instantiate(deathParticleSystem, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            nPCSpawner.StartCoroutine(GameObject.FindGameObjectWithTag("Scripts").GetComponent<NPCSpawner>().ClearDeadNPC());
        }
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Scripts").GetComponent<PlayerStats>().addLife(-1);
            Destroy(this.gameObject);
            nPCSpawner.StartCoroutine(GameObject.FindGameObjectWithTag("Scripts").GetComponent<NPCSpawner>().ClearDeadNPC());
        }
    }
}
