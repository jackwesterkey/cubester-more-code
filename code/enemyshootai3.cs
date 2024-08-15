using UnityEngine;
using System.Collections;

public class enemyshootai3 : MonoBehaviour
{
    public Transform target; // The player's transform
    public float optimalDistance = 10.0f; // The optimal distance to maintain from the player
    public float moveSpeed = 5.0f; // Movement speed
    public float smoothness = 5.0f; // Smoothing factor
    public GameObject projectilePrefab; // The projectile to shoot
    public Transform bulletSpawnPoint; // The point where bullets are spawned
    public float shootingInterval = 2.0f; // Time interval between shots
    public float bulletSpeed = 10.0f; // Speed of the bullets
    public float bulletLifetime = 3f; // Lifetime of the bullets
    public AudioClip shootSound; // Audio clip to play when shooting
    private float timeSinceLastShot;
    private bool canShoot = false;

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        // Add an initial delay of 2 seconds before the enemy can start shooting.
        yield return new WaitForSeconds(0.0f);

        // Set the flag to allow shooting.
        canShoot = true;

        // Now, the enemy can start executing its regular logic in the Update method.
    }

    void Update()
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
        Vector3 toPlayer = target.position - transform.position;
        float distanceToPlayer = toPlayer.magnitude;
        toPlayer.Normalize();

        // Calculate the destination point at the optimal distance.
        Vector3 destination = target.position - toPlayer * optimalDistance;

        // Move towards the destination point using Lerp.
        Vector3 newPosition = Vector3.Lerp(transform.position, destination, Time.deltaTime * smoothness);
        transform.position = newPosition;

        // Rotate the enemy's body towards the player.
        Quaternion targetRotation = Quaternion.LookRotation(toPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothness);

        // Shooting logic
        if (canShoot)
        {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= shootingInterval)
            {
                Shoot();
                timeSinceLastShot = 0.0f;
            }
        }
    }

    private void Shoot()
    {
        // Calculate the direction to the player.
        Vector3 toPlayer = target.position - transform.position;
        toPlayer.Normalize();

        // Use the specified spawn point or the enemy's position if not specified.
        Vector3 spawnPosition = (bulletSpawnPoint != null) ? bulletSpawnPoint.position : transform.position;

        // Instantiate a projectile at the spawn position and rotation.
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.LookRotation(toPlayer));

        // Play the shoot sound at the spawn position.
        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, spawnPosition);
        }

        // Add force to the projectile to make it move at the specified speed.
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        if (projectileRb != null)
        {
            projectileRb.AddForce(toPlayer * bulletSpeed, ForceMode.Impulse);

            // Set the lifetime of the projectile.
            Destroy(projectile, bulletLifetime);
        }
    }
}
