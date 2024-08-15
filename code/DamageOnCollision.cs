using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public HealthManager playerHealthManager; // Reference to the player's HealthManager script
    public EnemyHealthManager enemyHealthManager; // Reference to the EnemyHealthManager script

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the other collider has a HealthManager component (player or enemy)
        HealthManager otherHealthManager = collision.collider.GetComponent<HealthManager>();
        EnemyHealthManager otherEnemyHealthManager = collision.collider.GetComponent<EnemyHealthManager>();

        if (otherHealthManager != null)
        {
            // Reduce other collider's health by 10 (for player)
            playerHealthManager.TakeDamage(1);
        }
        else if (otherEnemyHealthManager != null)
        {
            // Reduce other collider's health by 10 (for enemy)
            enemyHealthManager.TakeDamage(5);
        }
        else
        {
            //Debug.LogWarning("No HealthManager component found on the collided object.");
        }
    }
}
