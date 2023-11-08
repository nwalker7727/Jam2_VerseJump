using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10; // Damage inflicted by the projectile.

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the trigger collision is with the player.
        if (other.CompareTag("Player"))
        {
            // Get the PlayerHealth script from the player GameObject (assuming you have a script for player health).
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // Check if the playerHealth script is found.
            if (playerHealth != null)
            {
                // Deduct the player's health based on the projectile's damage.
                playerHealth.TakeDamage();
            }

            // Destroy the projectile upon trigger collision.
            Destroy(gameObject);
        }
        else
        {
            // If the trigger collision is with anything other than the player, destroy the projectile.
            Destroy(gameObject);
        }
    }
}
