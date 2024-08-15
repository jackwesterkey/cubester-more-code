using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float jumpForce = 10f;
    public float impactForceThreshold = 5f; // Threshold for impact sound
    public KeyCode jumpKey = KeyCode.Space;
    public int maxJumps = 3;
    private int jumpsRemaining;
    private bool hasJumpedOnce = false; // New variable to track the first jump

    public AudioSource jumpSound;
    public AudioSource impactSound;

    private void Start()
    {
        jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        if (Input.GetKeyDown(jumpKey) && jumpsRemaining > 0)
        {
            Jump();

            // Play the jump sound only on the first jump
            if (!hasJumpedOnce && jumpSound != null && jumpSound.clip != null)
            {
                jumpSound.Play();
                hasJumpedOnce = true;
            }
        }
    }

    private void Jump()
    {
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpsRemaining--;

        if (jumpsRemaining == 0)
        {
            enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the impact force is greater than the threshold
        if (collision.relativeVelocity.magnitude > impactForceThreshold)
        {
            // Check if the collision normal is pointing upwards (along the y-axis)
            if (Vector3.Dot(collision.contacts[0].normal, Vector3.up) > 0.5f)
            {
                // Play the impact sound only for collisions with surfaces below the player
                if (impactSound != null && impactSound.clip != null)
                {
                    impactSound.Play();
                }

                // Treat each ground collision as a new "first jump"
                hasJumpedOnce = false;
            }
        }

        jumpsRemaining = maxJumps;
        enabled = true;
    }
}
