using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private bool canTakeDamage = true;
    public float damageCooldown = 1.0f;
    public Slider healthSlider;

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
        UpdateHealthUI();
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Spikes"))
        {
            Die();
        }
    }

    // Handle trigger collisions with trigger colliders.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (canTakeDamage && other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
        if (canTakeDamage && other.gameObject.CompareTag("Spike"))
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / (float)maxHealth;
        }
    }
}
