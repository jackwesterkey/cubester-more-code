using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationXandZlimiter : MonoBehaviour
{
    public float minXRotation = -45f; // Minimum X rotation angle
    public float maxXRotation = 0f;  // Maximum X rotation angle
    public float minZRotation = -45f; // Minimum Z rotation angle
    public float maxZRotation = 0f;  // Maximum Z rotation angle

    void Update()
    {
        // Get the current rotation of the GameObject
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // Limit X rotation
        currentRotation.x = Mathf.Clamp(currentRotation.x, minXRotation, maxXRotation);

        // Limit Z rotation
        currentRotation.z = Mathf.Clamp(currentRotation.z, minZRotation, maxZRotation);

        // Apply the limited rotation back to the GameObject
        transform.rotation = Quaternion.Euler(currentRotation);
    }
}
