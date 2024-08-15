using UnityEngine;
using System.Collections.Generic;

public class EnemyHealthManager2 : MonoBehaviour
{
    public int maxHealth = 50; // Maximum health
    private int currentHealth;   // Current health
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

        // Check if health is zero and destroy the object
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

        // Check specific health values and assign corresponding materials
        if (currentHealth == 50)
        {
            materialIndex = 4; // Material1
        }
        else if (currentHealth == 40)
        {
            materialIndex = 2; // Material2
        }
        else if (currentHealth == 30)
        {
            materialIndex = 3; // Material3
        }
        else if (currentHealth == 20)
        {
            materialIndex = 0; // Material4
        }
        else if (currentHealth == 10)
        {
            materialIndex = 1; // Material5
        }
        else
        {
            // Calculate material index based on the current health for other cases
            float healthPercentage = (float)currentHealth / maxHealth;
            materialIndex = Mathf.FloorToInt(healthPercentage * materialsToApply.Count);
            materialIndex = Mathf.Clamp(materialIndex, 0, materialsToApply.Count - 1);
        }

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = materialsToApply[materialIndex];
        }

        // Check if health is zero and destroy the object
        if (currentHealth == 0)
        {
            Die();
        }
    }

    // Called when the enemy's health reaches zero
    private void Die()
    {
        Destroy(gameObject);
    }
}
