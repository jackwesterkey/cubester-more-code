using UnityEngine;
using System.Collections.Generic;

public class DamageOnCollision2 : MonoBehaviour
{
    public List<EnemyHealthManager2> enemyHealthManagers = new List<EnemyHealthManager2>();

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the other collider has an EnemyHealthManager2 component
        EnemyHealthManager2 otherEnemyHealthManager = collision.collider.GetComponent<EnemyHealthManager2>();

        if (otherEnemyHealthManager != null)
        {
            // Add the EnemyHealthManager2 instance to the list if it's not already in the list
            if (!enemyHealthManagers.Contains(otherEnemyHealthManager))
            {
                enemyHealthManagers.Add(otherEnemyHealthManager);
            }

            // Example: Reduce other collider's health by 5 (for enemy)
            otherEnemyHealthManager.TakeDamage(5);
        }
        else
        {
            //Debug.LogWarning("No EnemyHealthManager2 component found on the collided object.");
        }
    }
}
