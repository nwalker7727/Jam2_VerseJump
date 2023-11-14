using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMOD.Studio;
using FMODUnity;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private bool canTakeDamage = true;
    public float damageCooldown = 1.0f;
    public Slider healthSlider;

    [SerializeField]
    private LayerMask spike; // LayerMask variable

    [SerializeField]
    private Transform spawnPoint; // You can add a public Transform variable for specifying a spawn point in the Inspector.

    [field: SerializeField] 
    public EventReference hitSoundEvent { get; private set; }

    private AudioManager am;

    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        currentHealth = maxHealth; // Set initial health to maxHealth
    }

    void Update()
    {
        // Update the cooldown timer if the player cannot take damage
        if (!canTakeDamage)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown <= 0.0f)
            {
                canTakeDamage = true;
                damageCooldown = 1.0f; // Reset the cooldown for the next time the player can take damage
            }
        }
    }

    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            if (!hitSoundEvent.IsNull)
            {
                EventInstance eventInstance = RuntimeManager.CreateInstance(hitSoundEvent);
                eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform)); // Set 3D position.
                eventInstance.start();

                // Release the FMOD event instance when it's no longer needed.
                eventInstance.release();
            }

            currentHealth--;

            if (currentHealth <= 0)
            {
                Die();
            }

            canTakeDamage = false;
            damageCooldown = 1.0f; // Reset the cooldown for the next time the player can take damage
        }

        UpdateHealthUI();
    }

    public void Die()
    {
        am.thanos();
        // You can specify a spawn point for respawning
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (canTakeDamage && collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
        if (collision.gameObject.layer == spike)
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
