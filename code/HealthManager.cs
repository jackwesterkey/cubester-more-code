using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 60;
    private int currentHealth;
    public int fontSize = 12;
    public Texture2D healthIcon;
    public Vector2 healthIconSize = new Vector2(20, 20);

    public AudioSource damageSound; // Add this variable for the damage sound

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Current Health: " + currentHealth);

        // Play the damage sound
        if (damageSound != null && damageSound.clip != null)
        {
            damageSound.Play();
        }
    }

    public void Heal(int healingAmount)
    {
        currentHealth += healingAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Current Health: " + currentHealth);
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = fontSize;

        int iconsToDisplay = Mathf.CeilToInt((float)currentHealth / maxHealth * (maxHealth / 10));

        for (int i = 0; i < iconsToDisplay; i++)
        {
            float xPos = 10 + i * (healthIconSize.x + 5);
            GUI.DrawTexture(new Rect(xPos, 10, healthIconSize.x, healthIconSize.y), healthIcon);
        }
    }
}
