using UnityEngine;
using System.Collections.Generic;

public class EnemyHealthManager : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;
    public List<Material> materialsToApply = new List<Material>();
    public AudioSource damageSound; // Add this variable for the damage sound

    void Start()
    {
        currentHealth = maxHealth;
        UpdateMaterial();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateMaterial();
        Debug.Log("Current Health: " + currentHealth);

        // Play the damage sound
        if (damageSound != null && damageSound.clip != null)
        {
            damageSound.Play();
        }

        if (currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(int healingAmount)
    {
        currentHealth += healingAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateMaterial();
        Debug.Log("Current Health: " + currentHealth);
    }

    private void UpdateMaterial()
    {
        int materialIndex;

        if (currentHealth == 50)
        {
            materialIndex = 4;
        }
        else if (currentHealth == 40)
        {
            materialIndex = 2;
        }
        else if (currentHealth == 30)
        {
            materialIndex = 3;
        }
        else if (currentHealth == 20)
        {
            materialIndex = 0;
        }
        else if (currentHealth == 10)
        {
            materialIndex = 1;
        }
        else
        {
            materialIndex = Mathf.FloorToInt((float)currentHealth / 10);
            materialIndex = Mathf.Clamp(materialIndex, 0, materialsToApply.Count - 1);
        }

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = materialsToApply[materialIndex];
        }

        if (currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }
}
