using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded;
    private Rigidbody rb;
    private Collider col; // Use the base class for colliders
    private bool is2D = false; // A flag to indicate 2D or 3D
    private ForceMode jumpForceMode;

    [SerializeField]
    private LayerMask groundLayer; // Serialized field for ground layer

    [SerializeField]
    private Animator animator; // Serialized field for the Animator component

    void Start()
    {
        // Detect whether the object uses 2D or 3D components
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        if (rb is Rigidbody2D)
        {
            is2D = true;
            jumpForceMode = ForceMode.Impulse; // Use 2D force mode
        }
        else
        {
            jumpForceMode = ForceMode.Impulse; // Use 3D force mode
        }
    }

    void Update()
    {
        // Player movement
        float moveInput = Input.GetAxis("Horizontal");

        // Separate the sprite flipping logic
        if (moveInput < 0)
        {
            // Flip the object horizontally when moving left
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveInput > 0)
        {
            // Reset the scale when moving right
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Set animation based on key input
        bool isMoving = Mathf.Abs(moveInput) > 0;
        animator.SetBool("IsMoving", isMoving);

        // Use different velocity assignments for 2D and 3D rigidbodies
        if (is2D)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector3(moveInput * moveSpeed, rb.velocity.y, 0);
        }

        // Jumping
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            // Set IsJumping to true
            animator.SetBool("IsJumping", true);
            rb.AddForce(Vector3.up * jumpForce, jumpForceMode);
        }

        // Set animation based on jumping state
        if (rb.velocity.y > 0)
        {
            animator.SetBool("IsJumping", true);
        }
        else if (rb.velocity.y < 0)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }
    }

    private bool IsGrounded()
    {
        float rayLength = 1.1f; // Adjust this value as needed

        if (is2D)
        {
            RaycastHit2D hit = Physics2D.Raycast(col.bounds.center, Vector2.down, col.bounds.extents.y + 0.1f, groundLayer);
            Debug.DrawRay(col.bounds.center, Vector2.down * (col.bounds.extents.y + 0.1f), Color.red);
            return hit.collider != null;
        }
        else
        {
            RaycastHit hit;
            return Physics.Raycast(col.bounds.center, Vector3.down, out hit, col.bounds.extents.y + 0.1f, groundLayer);
        }
    }
}
