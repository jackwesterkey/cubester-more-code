using UnityEngine;

public class RandomScriptEnabler : MonoBehaviour
{
    // Add your script components here
    public MonoBehaviour[] scriptsToEnable;

    void Start()
    {
        // Ensure at least one script is provided
        if (scriptsToEnable.Length > 0)
        {
            // Get a random index
            int randomIndex = Random.Range(0, scriptsToEnable.Length);

            // Generate a random boolean to decide whether to enable the script
            bool enableScript = Random.Range(0f, 1f) > 0.5f;

            // Enable or disable the randomly selected script based on the random boolean
            scriptsToEnable[randomIndex].enabled = enableScript;

            if (enableScript)
            {
                Debug.Log("Enabled script: " + scriptsToEnable[randomIndex].GetType().Name);
            }
            else
            {
                Debug.Log("Script: " + scriptsToEnable[randomIndex].GetType().Name + " is not enabled.");
            }
        }
        else
        {
            Debug.LogError("No scripts provided to enable. Attach scripts in the inspector.");
        }
    }
}
