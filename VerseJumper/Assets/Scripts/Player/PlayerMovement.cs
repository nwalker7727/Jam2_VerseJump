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

    private bool IsGrounded(){
        return Physics2D.BoxCast(cl.bounds.center, cl.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
    }
}
