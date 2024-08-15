using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target; // The player's transform
    public float moveSpeed = 5.0f; // Movement speed
    public float smoothness = 5.0f; // Smoothing factor

    private void Update()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                return;
            }
        }

        // Calculate the direction from the enemy to the player.
        Vector3 moveDirection = target.position - transform.position;
        moveDirection.Normalize();

        // Use Lerp to smoothly move the enemy towards the player.
        Vector3 newPosition = Vector3.Lerp(transform.position, transform.position + moveDirection, Time.deltaTime * smoothness);

        // Move the enemy to the new position.
        transform.position = newPosition;
    }
}
