using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public Transform player;
    public string obstacleName;
    public float deleteDistance = 2f;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < deleteDistance)
        {
            Debug.Log("Player bumped into " + obstacleName);
            DeleteObjects();
        }
    }

    void DeleteObjects()
    {
        // Assuming this script is attached to the obstacle, you can destroy the obstacle itself.
        Destroy(gameObject); // Destroy the obstacle

        // If you want to destroy other objects, you can use the following example:
        // Destroy(otherObject);
    }
}
