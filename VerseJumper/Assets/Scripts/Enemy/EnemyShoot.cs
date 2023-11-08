using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab.
    public float shootInterval = 1.0f; // Time interval between shots.
    public float shootSpeed = 5.0f; // Speed of the projectile.
    public Vector2 shootDirection = Vector2.right; // Shooting direction.

    private void Start()
    {
        // Start shooting projectiles when the enemy is created.
        StartCoroutine(ShootProjectiles());
    }

    IEnumerator ShootProjectiles()
    {
        while (true)
        {
            // Create a new projectile instance.
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Set the projectile's initial velocity based on the shootDirection.
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = shootDirection.normalized * shootSpeed;

            // Destroy the projectile after a certain time if it doesn't hit anything.
            Destroy(projectile, 5.0f);

            // Wait for the next shot based on the specified shootInterval.
            yield return new WaitForSeconds(shootInterval);
        }
    }
}