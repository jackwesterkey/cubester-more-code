using UnityEngine;

public class CloseAppOnDestroy : MonoBehaviour
{
    // This public variable allows you to assign the object to monitor in the Inspector
    public GameObject objectToMonitor;

    private void Update()
    {
        // Check if the monitored object is actually destroyed
        if (objectToMonitor == null)
        {
            // The object has been destroyed, proceed to close the application
            CloseApplication();
        }
        else if (!objectToMonitor.activeInHierarchy)
        {
            // Optionally, you can log when the object is inactive but not destroyed
            Debug.Log("The monitored object is inactive but not destroyed.");
        }
    }

    private void CloseApplication()
    {
        // Log the closing action for debugging purposes
        Debug.Log("Closing application because the monitored object has been destroyed.");

        // Check if the application is running in the editor
#if UNITY_EDITOR
        // Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quit the application
        Application.Quit();
#endif
    }
}
