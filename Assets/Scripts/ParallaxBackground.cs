using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]
    private float backgroundLength;
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private float parallaxSpeed;

    private Transform[] backgroundStates;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;
    private float lastCameraX;

	void Start ()
    {
        lastCameraX = cameraTransform.position.x;
        backgroundStates = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            backgroundStates[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = backgroundStates.Length - 1;
	}

	void Update ()
    {
        float cameraDeltaX = cameraTransform.position.x - lastCameraX;
        transform.position += Vector3.right * (cameraDeltaX * parallaxSpeed);
        lastCameraX = cameraTransform.position.x;
		if(cameraTransform.position.x < (backgroundStates[leftIndex].transform.position.x + viewZone))
        {
            ScrollLeft();
        }

        if (cameraTransform.position.x > (backgroundStates[rightIndex].transform.position.x - viewZone))
        {
            ScrollRight();
        }
    }

    private void ScrollLeft()
    {
        backgroundStates[rightIndex].position = new Vector3(backgroundStates[leftIndex].position.x - backgroundLength, backgroundStates[leftIndex].position.y, 0);
        leftIndex = rightIndex;
        rightIndex--;
        if(rightIndex < 0)
        {
            rightIndex = backgroundStates.Length - 1;
        }
        foreach (Transform bg in backgroundStates)
        {
            bg.gameObject.name = "-";
        }
        backgroundStates[leftIndex].gameObject.name = "Left";
        backgroundStates[rightIndex].gameObject.name = "Right";
    }

    private void ScrollRight()
    {
        backgroundStates[leftIndex].position = new Vector3(backgroundStates[rightIndex].position.x + backgroundLength, backgroundStates[leftIndex].position.y, 0);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == backgroundStates.Length)
        {
            leftIndex = 0;
        }
        foreach (Transform bg in backgroundStates)
        {
            bg.gameObject.name = "-";
        }
        backgroundStates[leftIndex].gameObject.name = "Left";
        backgroundStates[rightIndex].gameObject.name = "Right";
    }
}
