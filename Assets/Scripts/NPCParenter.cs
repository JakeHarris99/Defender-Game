using UnityEngine;

public class NPCParenter : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "NPC")
        {
            collision.transform.SetParent(this.transform);

        }
    }

}
