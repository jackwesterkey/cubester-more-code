using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float swayAmount = 0.02f;   // Amount of sway
    public float maxSwayAmount = 0.06f; // Maximum sway amount
    public float smoothTime = 5.0f;    // Smoothing speed

    private Vector3 initialPosition;    // Initial position of the weapon

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calculate the new position of the weapon based on mouse input
        float targetX = mouseX * swayAmount;
        float targetY = mouseY * swayAmount;

        // Apply smoothing to the sway
        targetX = Mathf.Clamp(targetX, -maxSwayAmount, maxSwayAmount);
        targetY = Mathf.Clamp(targetY, -maxSwayAmount, maxSwayAmount);

        Vector3 finalPosition = new Vector3(targetX, targetY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothTime);
    }
}
