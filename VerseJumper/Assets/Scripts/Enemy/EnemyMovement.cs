using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform pointA; // The first patrol point
    public Transform pointB; // The second patrol point
    public float moveSpeed = 3.0f;
    private bool movingToA = true; // Start by moving towards point A
    SpriteRenderer rendering;

    void Start(){
        rendering = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Determine the target point based on the current direction
        Transform targetPoint = movingToA ? pointA : pointB;

        // Move towards the target point
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        // Check if we've reached the target point
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.5f)
        {
            // If we're at point A, start moving to point B, and vice versa
            rendering.flipX = !rendering.flipX;
            movingToA = !movingToA;
        }
    }
}
