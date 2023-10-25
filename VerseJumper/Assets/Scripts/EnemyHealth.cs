using UnityEngine;
using System.Collections;
using FMOD.Studio;
using FMODUnity;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100.0f;
    private float currentHealth;
    private bool isDying = false; // Track the dying state.

    public Rigidbody2D rb; // Reference to the Rigidbody2D component.
    public float jumpForce = 5.0f; // Adjust the jump force as needed.
    public GameObject pickupPrefab; // Reference to the pickup sprite prefab.
    public float pickupDelay = 1.0f; // Delay before the loot becomes available for pickup.

    public Animator enemyAnimator; // Reference to the enemy's Animator component.

    [field: SerializeField] public EventReference deathSoundEvent { get; private set; }
    [field: SerializeField] private RuntimeAnimatorController deathAnimationController; // Serialized field for the death animation controller.
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerController playerController;

    private Renderer enemyRenderer; // Reference to the enemy's Renderer component.

    private Collider2D enemyCollider; // Reference to the enemy's collider.

    private void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        currentHealth = maxHealth;
        enemyCollider = GetComponent<Collider2D>();
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isDying)
        {
            return; // If already dying, do nothing.
        }

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0; // Ensure health doesn't go below 0.
            PlayDeathAnimation();
            StartCoroutine(DieWithDelay());
        }
        else
        {
            JumpBackward();
        }
    }

    private IEnumerator DieWithDelay()
    {
        isDying = true; // Set the dying state to true.
        // Disable the enemy's renderer to make it invisible.
        enemyRenderer.enabled = false;

        if (!deathSoundEvent.IsNull)
        {
            EventInstance eventInstance = RuntimeManager.CreateInstance(deathSoundEvent);
            eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform)); // Set 3D position.
            eventInstance.start();

            // Release the FMOD event instance when it's no longer needed.
            eventInstance.release();
        }

        // Play the death animation on the enemy using the Animator Controller.
        if (playerAnimator != null && deathAnimationController != null)
        {
            playerAnimator.runtimeAnimatorController = deathAnimationController;
        }

        // Wait for the enemy death animation to finish and then switch back to the original player animation.
        float deathAnimationDuration = 0.5f;
        yield return new WaitForSeconds(deathAnimationDuration);

        // Call the method in the PlayerController script to switch back to the original player animation.
        playerController.SwitchToOriginalController();

        // Delay before dropping the loot.
        yield return new WaitForSeconds(pickupDelay);

        // Disable the enemy's collider.
        enemyCollider.enabled = false;

        // Instantiate the pickup object at the enemy's position.
        Instantiate(pickupPrefab, transform.position, Quaternion.identity);

        // Add code to handle enemy death, such as playing additional death animations.
        Destroy(gameObject); // Optionally remove the enemy GameObject.
    }

    private void JumpBackward()
    {
        // Calculate the direction from the enemy to the player.
        Vector2 jumpDirection = (PlayerPosition() - new Vector2(transform.position.x, transform.position.y)).normalized;

        // Apply an upward jump force in the opposite direction.
        rb.AddForce(-jumpDirection * jumpForce, ForceMode2D.Impulse);
    }

    private Vector2 PlayerPosition()
    {
        // Implement a method to get the player's position here. You can use a tag or other methods to find the player GameObject.
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            return player.transform.position;
        }
        else
        {
            // Return a default position or handle the case where the player is not found.
            return Vector2.zero;
        }
    }

    private void PlayDeathAnimation()
    {
        if (playerAnimator != null && deathAnimationController != null)
        {
            playerAnimator.runtimeAnimatorController = deathAnimationController;
        }
    }

    public bool IsDying() // Public method to check the dying state.
    {
        return isDying;
    }
}
