using UnityEngine;

public class DamageOnCollision3 : MonoBehaviour
{
    public HealthManager playerHealthManager; // Reference to the player's HealthManager script

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the other collider has a HealthManager component (player or enemy)
        HealthManager otherHealthManager = collision.collider.GetComponent<HealthManager>();

        if (otherHealthManager != null)
        {
            // Reduce other collider's health by 10 (for player)
            playerHealthManager.TakeDamage(4);
        }
        else
        {
            //Debug.LogWarning("No HealthManager component found on the collided object.");
        }
    }
}
