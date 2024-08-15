using UnityEngine;
using System.Collections;

public class SaveLoadManager : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 playerStartPosition;

    public Texture2D savedTexture; // Texture for "Game saved!" feedback
    public Texture2D loadedTexture; // Texture for "Game loaded!" feedback

    private Texture2D feedbackTexture; // Variable to hold the current feedback texture

    private void Start()
    {
        // Assuming your player has a Transform component.
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerStartPosition = playerTransform.position;

        // Load your textures here (assign textures to savedTexture and loadedTexture)
        // Example: savedTexture = Resources.Load<Texture2D>("SavedTexture");
        // Example: loadedTexture = Resources.Load<Texture2D>("LoadedTexture");

        // Save the game when the game starts
        SaveGame();
        SetFeedbackTexture(savedTexture);
    }

    void Update()
    {
        // Save the game when F5 is pressed
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
            SetFeedbackTexture(savedTexture);
        }

        // Load the game when F9 is pressed
        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadGame();
            SetFeedbackTexture(loadedTexture);
        }
    }

    void OnGUI()
    {
        // Display the feedback texture at the top-middle of the screen
        if (feedbackTexture != null)
        {
            // Center the texture horizontally and place it at the top
            float textureWidth = feedbackTexture.width;
            float textureHeight = feedbackTexture.height;
            float xPosition = Screen.width / 2 - textureWidth / 2;
            float yPosition = -3;  // Adjust this value to set the vertical position

            GUI.DrawTexture(new Rect(xPosition, yPosition, textureWidth, textureHeight), feedbackTexture);
        }
    }

    void SaveGame()
    {
        // Save player position
        PlayerPrefs.SetFloat("playerPosX", playerTransform.position.x);
        PlayerPrefs.SetFloat("playerPosY", playerTransform.position.y);
        PlayerPrefs.SetFloat("playerPosZ", playerTransform.position.z);

        // You can save other game data here as needed.
        PlayerPrefs.Save();
    }

    void LoadGame()
    {
        // Load player position
        float loadedplayerPosX = PlayerPrefs.GetFloat("playerPosX", playerStartPosition.x);
        float loadedplayerPosY = PlayerPrefs.GetFloat("playerPosY", playerStartPosition.y);
        float loadedplayerPosZ = PlayerPrefs.GetFloat("playerPosZ", playerStartPosition.z);

        // Set the player position
        playerTransform.position = new Vector3(loadedplayerPosX, loadedplayerPosY, loadedplayerPosZ);

        // You can load and set other game data here as needed.
    }

    void SetFeedbackTexture(Texture2D texture)
    {
        feedbackTexture = texture;

        // Wait for a few seconds before clearing the texture
        StartCoroutine(ClearFeedbackTexture());
    }

    IEnumerator ClearFeedbackTexture()
    {
        yield return new WaitForSeconds(2f); // Adjust the duration as needed
        feedbackTexture = null;
    }
}
