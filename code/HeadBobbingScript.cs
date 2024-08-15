using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobbingScript : MonoBehaviour
{
    public GameObject Camera;
    private Animator cameraAnimator;

    private bool isBobbing = false;
    private Vector3 initialCameraPosition;
    private float resetSpeed = 2.0f; // Adjust the reset speed as needed

    private void Start()
    {
        cameraAnimator = Camera.GetComponent<Animator>();
        initialCameraPosition = Camera.transform.localPosition;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (!isBobbing)
            {
                StartBobbing();
                isBobbing = true;
            }
        }
        else
        {
            StopBobbing();
            isBobbing = false;
        }
    }

    void StartBobbing()
    {
        cameraAnimator.Play("HeadBobbing");
    }

    void StopBobbing()
    {
        ResetCameraPosition();
        cameraAnimator.Play("New State");
    }

    void ResetCameraPosition()
    {
        // Reset the camera's position immediately
        Camera.transform.localPosition = initialCameraPosition;
    }
}
