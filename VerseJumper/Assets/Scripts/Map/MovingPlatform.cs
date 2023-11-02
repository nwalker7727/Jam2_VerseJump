using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // The starting point
    public Transform pointB; // The ending point
    public float moveSpeed = 2.0f;
    public bool toggleOnContact = true; // Toggle feature for player contact activation

    private Vector3 targetPosition;
    private bool playerOnPlatform = false;
    private Transform playerTransform;

    void Start()
    {
        targetPosition = pointA.position;
    }

    void Update()
    {
        if (toggleOnContact && !playerOnPlatform)
        {
            // Player contact activation is enabled, but the player is not on the platform
            return;
        }

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if we've reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Swap the target position (A to B, or B to A)
            targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;
        }

        if (playerOnPlatform && playerTransform != null)
        {
            // Update the player's position relative to the platform
            playerTransform.position = new Vector3(playerTransform.position.x, transform.position.y + 1.0f, playerTransform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (toggleOnContact && collision.gameObject.CompareTag("Player"))
        {
            // Player is on the platform, enable movement
            playerOnPlatform = true;
            playerTransform = collision.transform;

            // Make the player a child of the platform
            playerTransform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (toggleOnContact && collision.gameObject.CompareTag("Player"))
        {
            // Player has left the platform, stop movement
            playerOnPlatform = false;

            // Remove the player as a child of the platform
            playerTransform.SetParent(null);
        }
    }
}
