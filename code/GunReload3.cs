using UnityEngine;
using System.Collections;

public class GunReload3 : MonoBehaviour
{
    public AmmoManager1 ammoManager1; // Reference to the AmmoManager1 script
    public Animator animator; // Reference to the Animator component
    public AudioSource reloadSound; // Reference to the AudioSource component

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ammoManager1 != null)
            {
                StartCoroutine(ReloadCoroutine(1.0f)); // Start reloading with a specified duration
            }
            else
            {
                Debug.LogWarning("AmmoManager1 reference is not assigned in GunReload3 script.");
            }
        }
    }

    private IEnumerator ReloadCoroutine(float duration)
    {
        // Play the reload sound
        if (reloadSound != null)
        {
            reloadSound.Play();
        }
        else
        {
            Debug.LogWarning("Reload sound is not assigned in GunReload3 script.");
        }

        // Start the reload animation
        animator.Play("reload-m-dash-1");

        // Call the ReloadGun method in the AmmoManager1 script to handle actual reloading logic
        ammoManager1.ReloadGun();

        // Wait for the reload animation to finish
        yield return new WaitForSeconds(duration);

        // Transition back to the idle animation
        animator.Play("idle-1");

        // Reload is complete
        Debug.Log("Reloading complete!");
    }
}
