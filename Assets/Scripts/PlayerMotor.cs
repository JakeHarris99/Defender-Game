using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMotor : MonoBehaviour
{

    [SerializeField]
    private Camera gameCamera;
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private Transform cameraTransforms;

    private float lastWarpTime;
    private float warpCooldown = 0.3f;
    private Rigidbody2D playerRigidbody;
    private SpriteRenderer playerRenderer;
    private float verticalMovementSpeed = 8f;
    private float cameraMovementSpeed = 20f;
    private float currentCameraMovementSpeed;
    private float acceleration = 20f;

    void Start()
    {
        lastWarpTime = Time.time;
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        float horizontalInput = Mathf.Round(Input.GetAxisRaw("Horizontal"));
        float verticalInput = Mathf.Round(Input.GetAxisRaw("Vertical"));

        transform.position = Vector2.Lerp(transform.position, new Vector2(gameCamera.transform.position.x, transform.position.y), cameraMovementSpeed / 400f);

        Vector3 cameraMovement = Vector3.right * currentCameraMovementSpeed;

        cameraTransforms.position = cameraTransforms.position + cameraMovement * Time.deltaTime;

        if (horizontalInput != 0)
        {
            if (horizontalInput > 0)
            {
                playerRenderer.flipX = false;
                currentCameraMovementSpeed += acceleration * Time.deltaTime;
                if (currentCameraMovementSpeed > cameraMovementSpeed)
                {
                    currentCameraMovementSpeed = cameraMovementSpeed;
                }
            }
            if (horizontalInput < 0)
            {
                playerRenderer.flipX = true;
                currentCameraMovementSpeed -= acceleration * Time.deltaTime;
                if (currentCameraMovementSpeed < -cameraMovementSpeed)
                {
                    currentCameraMovementSpeed = -cameraMovementSpeed;
                }
            }
            playerAnimator.SetFloat("Input", Mathf.Abs(horizontalInput));
        }
        else
        {
            currentCameraMovementSpeed = Mathf.Lerp(currentCameraMovementSpeed, 0, cameraMovementSpeed / 700f);
            playerAnimator.SetFloat("Input", 0f);
        }

        Vector2 shipVerticalMovement = Vector2.zero;

        if (verticalInput != 0)
        {
            shipVerticalMovement = Vector2.up * verticalInput * verticalMovementSpeed;
            playerRigidbody.MovePosition(playerRigidbody.position + shipVerticalMovement * Time.deltaTime);
        }

        if ((Time.time - lastWarpTime > warpCooldown) && Input.GetAxisRaw("Warp") != 0)
        {
            cameraTransforms.position = cameraTransforms.position + Vector3.right * (100f + Random.Range(-100f, 100f));
            playerRigidbody.MovePosition(cameraTransforms.position);
            lastWarpTime = Time.time;
        }
    }
}
