using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    public float speed = 5.0f;
    private GameObject playerObject;
    public AudioSource walkingSound; // AudioSource for walking sound
    public float raycastDistance = 0.2f; // Adjust this based on your player's size

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("player");
        walkingSound = GetComponent<AudioSource>(); // Assuming the AudioSource is attached to the same GameObject as this script
        walkingSound.loop = true; // Set the audio source to loop
    }

    // Update is called once per frame
    void Update()
    {
        // Check if any of the movement keys (W, A, S, D) is actively pressed
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        // Move the player only if the keys are pressed
        if (isMoving)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float forwardInput = Input.GetAxis("Vertical");

            // Move the player
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

            // Check if the player is grounded and providing input
            if (IsGrounded() && isMoving)
            {
                if (!walkingSound.isPlaying)
                {
                    walkingSound.Play();
                }
            }
            else
            {
                walkingSound.Stop(); // Stop the sound if the player is not grounded or not providing input
            }
        }
        else
        {
            walkingSound.Stop(); // Stop the sound if the keys are not actively pressed
        }
    }

    bool IsGrounded()
    {
        // Cast a ray downward to check if the player is grounded
        return Physics.Raycast(transform.position, Vector3.down, raycastDistance);
    }
}
