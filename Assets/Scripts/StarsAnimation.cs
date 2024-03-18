using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsAnimation : MonoBehaviour
{
    private float lastChangeTime;
    private float changeCooldown;
    private Renderer starsRenderer;
    // Use this for initialization
    void Start ()
    {
        starsRenderer = GetComponent<Renderer>();
        lastChangeTime = Time.time;
        changeCooldown = Random.Range(0f, 1f) + 0.5f;
	}

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastChangeTime > changeCooldown)
        {
            starsRenderer.enabled = !starsRenderer.enabled;
            lastChangeTime = Time.time;
            changeCooldown = Random.Range(1f, 2f) + 0.5f;
        }
    }
}
