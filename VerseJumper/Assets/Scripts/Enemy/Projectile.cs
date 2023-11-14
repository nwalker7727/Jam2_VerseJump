using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;  // Amount of damage the projectile inflicts

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the trigger collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerHealth component from the player GameObject
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // If the player has a PlayerHealth component, apply damage
            if (playerHealth != null)
            {
                // Decrease player's health
                playerHealth.TakeDamage();

                // Destroy the projectile
                Destroy(gameObject);
            }
        }
        else
        {
            // If the trigger collider belongs to something other than the player, just destroy the projectile
            Destroy(gameObject);
        }
    }
}
