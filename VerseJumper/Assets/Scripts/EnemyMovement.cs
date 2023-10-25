using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform leftBoundary;
    public Transform rightBoundary;
    public float normalMoveSpeed = 2.0f;
    public float chaseMoveSpeed = 1.0f;
    public float jumpForce = 5.0f;
    public LayerMask groundLayer;
    public float chaseRange = 5.0f;

    public float delayDuration = 2.0f; // Delay duration exposed in the inspector

    private bool movingRight = true;
    private Rigidbody2D rb;
    private Transform player;
    private bool delayMovement = true; // Flag to delay movement upon spawn
    private bool canMove = true; // Flag to control movement

    // Health of the enemy
    private EnemyHealth enemyHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Initialize the Rigidbody2D component.
        player = GameObject.FindWithTag("Player").transform; // Assuming the player has the "Player" tag.
        enemyHealth = GetComponent<EnemyHealth>();
        StartCoroutine(StartDelay()); // Start the delay coroutine upon spawn.
    }

    void Update()
    {
        if (canMove && !delayMovement)
        {
            // Determine the enemy's current position.
            Vector3 position = transform.position;

            // Calculate the distance to the player.
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Access the health value from the EnemyHealth script
            float health = enemyHealth.GetCurrentHealth();

            // Check if the health is greater than 1 to allow movement
            canMove = (health > 1);
            
            if (distanceToPlayer <= chaseRange)
            {
                // The player is within the chase range; chase the player at a slower speed.
                if (transform.position.x < player.position.x)
                {
                    position.x += chaseMoveSpeed * Time.deltaTime;
                }
                else
                {
                    position.x -= chaseMoveSpeed * Time.deltaTime;
                }
            }
            else
            {
                // The player is not within the chase range; move left and right as before at normal speed.
                if (movingRight)
                {
                    position.x += normalMoveSpeed * Time.deltaTime;
                    if (position.x >= rightBoundary.position.x)
                    {
                        movingRight = false;
                    }
                }
                else
                {
                    position.x -= normalMoveSpeed * Time.deltaTime;
                    if (position.x <= leftBoundary.position.x)
                    {
                        movingRight = true;
                    }
                }
            }

            // Update the enemy's position.
            transform.position = position;

            // Check for ground contact and allow the enemy to jump when grounded.
            if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    bool IsGrounded()
    {
        // Raycast to check if there's ground below the enemy.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(delayDuration); // Delay the start of movement.
        delayMovement = false; // Allow movement after the delay.
    }
}
