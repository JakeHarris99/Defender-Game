using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    private float lastShootTime;
    private float shootingCooldown = 0.3f;
    private SpriteRenderer playerRenderer;
    private ParticleSystem playerParticleSystem;

    private Color[] trailColours = { new Color(0, 255, 255), new Color(255, 255, 0), new Color(0, 255, 0), new Color(255, 0, 255) };

    [SerializeField]
    private Rigidbody2D projectilePrefab;
	
	void Start ()
    {
        lastShootTime = Time.time;
        playerRenderer = GetComponent<SpriteRenderer>();
	}

	void Update ()
    {
		if((Time.time - lastShootTime > shootingCooldown) && Input.GetAxisRaw("Fire") != 0)
        {
            Rigidbody2D projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as Rigidbody2D;
            playerParticleSystem = projectileInstance.GetComponent<ParticleSystem>();
            var psMain = playerParticleSystem.main;
            psMain.startColor = trailColours[Random.Range(0, 4)];
            if(playerRenderer.flipX)
            {
                projectileInstance.AddForce(new Vector2(-2000, 0));
            }
            else
            {
                projectileInstance.AddForce(new Vector2(2000, 0));
            }
            lastShootTime = Time.time;
        }
	}
}
