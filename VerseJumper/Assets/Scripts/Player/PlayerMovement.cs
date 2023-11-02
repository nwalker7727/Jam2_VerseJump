using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded;
    private Rigidbody2D rb;
    private BoxCollider2D cl;

    [SerializeField]
    private LayerMask groundLayer; // Serialized field for ground layer

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cl = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Player movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Jumping
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        float rayLength = 1.1f; // Adjust this value as needed

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);

        Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.red);

        return hit.collider != null;
    }
}
