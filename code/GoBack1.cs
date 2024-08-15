using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBack1 : MonoBehaviour
{
    public string destinationScene;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player touched the 'Go Back' object.");

            // Save the current player position for teleportation
            SaveTeleportPosition();

            // Set the flag to enable teleportation in the next scene
            PlayerPrefs.SetInt("EnableTeleport", 1);

            // Load the destination scene
            SceneManager.LoadScene(destinationScene);
        }
    }

    // Save the current player position for teleportation
    void SaveTeleportPosition()
    {
        // Assuming the player has a Rigidbody2D component
        Rigidbody2D playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            PlayerPrefs.SetFloat("TeleportX", playerRb.position.x);
            PlayerPrefs.SetFloat("TeleportY", playerRb.position.y);
        }
    }
}
