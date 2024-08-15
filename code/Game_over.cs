using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_over : MonoBehaviour
{
    public int maxHealth = 50; // Maximum health
    private int currentHealth;  // Current health
    public List<Material> materialsToApply = new List<Material>(); // List of materials to apply
    public AudioSource audioSource; // AudioSource component for playing sounds

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Set initial health to maxHealth
        UpdateMaterial(); // Update material initially
    }

    // Method to apply damage to the health
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Ensure health doesn't go below 0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Play damage sound
        if (audioSource != null)
        {
            audioSource.Play(); // You may need to set the AudioClip for the audioSource in the Unity Editor
        }

        UpdateMaterial(); // Update material based on current health

        Debug.Log(gameObject.name + " - Current Health: " + currentHealth);

        // Check if health is zero and handle death
        if (currentHealth == 0)
        {
            Die();
        }
    }

    // Method to heal the enemy
    public void Heal(int healingAmount)
    {
        currentHealth += healingAmount;

        // Ensure health doesn't exceed maxHealth
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateMaterial(); // Update material based on current health

        Debug.Log(gameObject.name + " - Current Health: " + currentHealth);
    }

    // Update material based on current health
    private void UpdateMaterial()
    {
        int materialIndex;

        // Determine the material index based on current health
        if (currentHealth == 50)
        {
            materialIndex = 4; // Example: Material for full health
        }
        else if (currentHealth == 40)
        {
            materialIndex = 2; // Example: Material for 40% health
        }
        else if (currentHealth == 30)
        {
            materialIndex = 3; // Example: Material for 30% health
        }
        else if (currentHealth == 20)
        {
            materialIndex = 0; // Example: Material for 20% health
        }
        else if (currentHealth == 10)
        {
            materialIndex = 1; // Example: Material for 10% health
        }
        else
        {
            // Calculate material index based on the current health for lower health values
            float healthPercentage = (float)currentHealth / maxHealth;
            materialIndex = Mathf.FloorToInt(healthPercentage * materialsToApply.Count);
            materialIndex = Mathf.Clamp(materialIndex, 0, materialsToApply.Count - 1);
        }

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && materialsToApply.Count > materialIndex)
        {
            renderer.material = materialsToApply[materialIndex];
        }
        else
        {
            Debug.LogWarning("Material index is out of range or materialsToApply is empty.");
        }
    }

    // Called when the enemy's health reaches zero
    private void Die()
    {
        Debug.Log(gameObject.name + " has died. Exiting the game...");

        // Close the application
        Application.Quit();

        // For editor testing (optional)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
