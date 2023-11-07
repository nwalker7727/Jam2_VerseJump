using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public int meleeDamage = 10; // Damage for melee attacks.
    public int rangedDamage = 20; // Damage for ranged attacks.
    public float attackRate = 1.0f; // Time delay between attacks.
    public float meleeRange = 2.0f; // Define the melee attack range.
    private float nextAttackTime = 0f; // Time of the next allowed attack.
    private bool canMeleeAttack = true; // Control if the player can perform a melee attack.

    public Animator playerAnimator; // Reference to the Animator component.

    void Start()
    {
        // Make sure to assign the player's Animator component in the Inspector.
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0) && canMeleeAttack) // Left-click (melee attack).
            {
                MeleeAttack();
            }
            else if (Input.GetMouseButtonDown(1)) // Right-click (ranged attack).
            {
                RangedAttack();
            }
        }
    }

    bool IsInMeleeRange()
    {
        // Check if there is an enemy in melee range.
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, meleeRange);
        foreach (Collider2D col in hitColliders)
        {
            if (col.CompareTag("Enemy"))
            {
                return true; // An enemy is within melee range.
            }
        }
        return false; // No enemy is within melee range.
    }

    void MeleeAttack()
    {
        if (IsInMeleeRange())
        {
            // Set the "Melee" parameter to true in the Animator.
            playerAnimator.SetBool("Melee", true);

            // Implement the logic for melee attack.
            // Make sure the player is within melee range before attacking.
            // Use a trigger collider and OnTriggerEnter2D to detect and damage nearby enemies.

            // Start the attack cooldown timer.
            StartCoroutine(MeleeCooldown());
        }
    }

    IEnumerator MeleeCooldown()
    {
        // Set canMeleeAttack to false during the cooldown period.
        canMeleeAttack = false;

        // Wait for the specified cooldown duration.
        yield return new WaitForSeconds(0.2f); // Adjust the cooldown duration as needed.

        // Reset the "Melee" parameter to false in the Animator.
        playerAnimator.SetBool("Melee", false);

        // Re-enable melee attacks after the cooldown.
        canMeleeAttack = true;
    }

    void RangedAttack()
    {
        // Set the "Ranged" parameter to true in the Animator.
        playerAnimator.SetBool("Ranged", true);

        // Implement the logic for ranged attack.
        // You can use raycasting, instantiate projectiles, or other methods for ranged attacks.
        // Apply ranged damage to enemies if they are hit.
    }

    void DealDamage(int damageAmount)
    {
        // This method can be used to handle damage logic common to both melee and ranged attacks.
        // You can apply damage to enemies, play attack animations, etc.
        // Implement your damage logic here.
        
        // Update the next attack time based on the attack rate.
        nextAttackTime = Time.time + 1f / attackRate;
    }
}