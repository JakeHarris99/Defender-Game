using UnityEngine;

public class Particle : MonoBehaviour
{
    private float startTime;
    private float duration = 1f;

	void Start ()
    {
        startTime = Time.time;
	}

	void Update ()
    {
		if(Time.time - startTime > duration)
        {
            Destroy(this.gameObject);
        }
	}
}
