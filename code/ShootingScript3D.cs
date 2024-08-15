using UnityEngine;

public class ShootingScript3D : MonoBehaviour
{
    // New variables for shooting
    public Transform target; // The player's transform
    public GameObject projectilePrefab; // The projectile to shoot
    public Transform bulletSpawnPoint; // The point where bullets are spawned
    public float shootingInterval = 2.0f; // Time interval between shots
    public float bulletSpeed = 10.0f; // Speed of the bullets
    public float bulletLifetime = 3f; // Lifetime of the bullets
    public AudioClip shootSound; // Audio clip to play when shooting
    private float timeSinceLastShot; // Time since the last shot

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        // Shoot at the player if enough time has passed
        if (timeSinceLastShot >= shootingInterval)
        {
            ShootAtPlayer();
            timeSinceLastShot = 0f;
        }
    }

    void ShootAtPlayer()
    {
        if (target != null && projectilePrefab != null && bulletSpawnPoint != null)
        {
            // Calculate the direction towards the player
            Vector3 directionToPlayer = (target.position - bulletSpawnPoint.position).normalized;

            // Instantiate the projectile at the spawn point
            GameObject projectile = Instantiate(projectilePrefab, bulletSpawnPoint.position, Quaternion.identity);

            // Play the shoot sound at the projectile's position
            if (shootSound != null)
            {
                AudioSource.PlayClipAtPoint(shootSound, projectile.transform.position);
            }

            // Set the velocity of the projectile to track the player
            projectile.GetComponent<Rigidbody>().velocity = directionToPlayer * bulletSpeed;

            // Set the lifetime of the projectile
            Destroy(projectile, bulletLifetime);
        }
    }
}
