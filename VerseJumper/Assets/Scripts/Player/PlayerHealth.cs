using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Import the UnityEngine.UI namespace.

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private bool canTakeDamage = true;
    public float damageCooldown = 1.0f;

    public Slider healthSlider; // Reference to the Slider UI element for health.

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (!canTakeDamage)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown <= 0.0f)
            {
                canTakeDamage = true;
                damageCooldown = 1.0f;
            }
        }
    }

    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                Die();
            }

            canTakeDamage = false;
        }
        UpdateHealthUI(); // Update the health bar when taking damage.
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (canTakeDamage && collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / (float)maxHealth; // Update the health bar's value based on the current health.
        }
    }
}
