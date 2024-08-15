using UnityEngine;
using UnityEngine.SceneManagement;

public class goback : MonoBehaviour
{
    public Transform player;
    public string destinationScene;
    public float teleportDistance = 2f;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < teleportDistance)
        {
            Debug.Log("Player bumped into the 'Go Back' object.");
            SaveTeleportPosition();

            // Set the flag to enable teleportation in the next scene
            PlayerPrefs.SetInt("EnableTeleport", 1);

            // Load the destination scene
            SceneManager.LoadScene(destinationScene);
        }
    }

    void SaveTeleportPosition()
    {
        PlayerPrefs.SetFloat("TeleportX", player.position.x);
        PlayerPrefs.SetFloat("TeleportY", player.position.y);
        PlayerPrefs.SetFloat("TeleportZ", player.position.z);
    }
}
