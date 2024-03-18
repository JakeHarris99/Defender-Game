using UnityEngine;

public class Laser : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
