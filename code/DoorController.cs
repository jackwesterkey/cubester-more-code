using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform player;
    public Transform teleportPosition;

    public string doorName;

    public float teleportDistance = 2f;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < teleportDistance)
        {
            Debug.Log("Player bumped into " + doorName);
            TeleportPlayer();
        }
    }

    void TeleportPlayer()
    {
        player.position = teleportPosition.position;
    }
}
