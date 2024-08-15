using UnityEngine;
using System.Collections; // Add this line for the Coroutine

public class HealingItem : MonoBehaviour
{
    public int healingAmount = 10;
    public float cooldownTime = 1f;
    private bool canHeal = true;

    // Muzzle flash particle system
    public ParticleSystem muzzleFlash;

    // AudioSource for healing sound
    public AudioSource audioSource;

    void Start()
    {
        // Initialize the AudioSource component if not assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && canHeal)
        {
            // Find the HealthManager script on the player GameObject
            HealthManager healthManager = FindObjectOfType<HealthManager>();

            // Check if the HealthManager script is found
            if (healthManager != null)
            {
                // Play the muzzle flash and healing sound before healing
                PlayMuzzleFlash();
                PlayHealingSound();

                // Call the Heal method in HealthManager
                healthManager.Heal(healingAmount);

                // Start the cooldown timer
                StartCoroutine(Cooldown());
            }
            else
            {
                Debug.LogError("HealthManager component not found on any player GameObject.");
            }
        }
    }

    void PlayMuzzleFlash()
    {
        if (muzzleFlash != null)
        {
            // Play the muzzle flash particle system
            muzzleFlash.Play();
        }
    }

    void PlayHealingSound()
    {
        // Play the healing sound directly from the AudioSource component
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.Log("AudioSource not assigned!");
        }
    }

    IEnumerator Cooldown()
    {
        // Set canHeal to false to prevent healing during cooldown
        canHeal = false;

        // Wait for the specified cooldown time
        yield return new WaitForSeconds(cooldownTime);

        // Set canHeal to true to allow healing again
        canHeal = true;
    }
}
