using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyshootai5 : MonoBehaviour
{
    public Transform target; // The player's transform
    public GameObject projectilePrefab; // The projectile to shoot
    public List<Transform> bulletSpawnPoints; // List of points where bullets are spawned
    public float shootingInterval = 2.0f; // Time interval between shots
    [SerializeField]
    public int maxProjectileCount = 5; // Maximum number of projectiles allowed
    // public float bulletLifetime = 3f; // Lifetime of the bullets

    private float timeSinceLastShot;
    private bool canShoot = false;
    private int currentProjectileCount = 0;

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
        toPlayer.Normalize();

        // Calculate the destination point at the optimal distance.
        float optimalDistance = 10.0f; // Set your desired optimal distance here
        Vector3 destination = target.position - toPlayer * optimalDistance;

        // Move towards the destination point using Lerp.
        float smoothness = 5.0f; // Set your desired smoothness here
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

        // Choose a random spawn point from the list.
        if (bulletSpawnPoints.Count > 0 && currentProjectileCount < maxProjectileCount)
        {
            Transform selectedSpawnPoint = bulletSpawnPoints[Random.Range(0, bulletSpawnPoints.Count)];

            // Instantiate a projectile at the spawn position and rotation.
            GameObject projectile = Instantiate(projectilePrefab, selectedSpawnPoint.position, Quaternion.LookRotation(toPlayer));

            // Increment the projectile count.
            currentProjectileCount++;

            // Uncomment the following line if you want to decrease the count when the projectile is destroyed.
            // StartCoroutine(DestroyProjectile(projectile));
        }
        else
        {
            Debug.LogError("No bullet spawn points assigned or max projectile count reached!");
        }
    }

    // Uncomment the following Coroutine if you want to decrease the count when the projectile is destroyed.
    // IEnumerator DestroyProjectile(GameObject projectile)
    // {
    //     yield return new WaitForSeconds(bulletLifetime);
    //     Destroy(projectile);
    //     currentProjectileCount--;
    // }
}
