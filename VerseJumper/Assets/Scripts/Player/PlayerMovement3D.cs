using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded;
    private Rigidbody rb;
    private Collider col;

    [SerializeField]
    private LayerMask groundLayer; // Serialized field for ground layer

    [SerializeField]
    private Animator animator; // Serialized field for the Animator component

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
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

        rb.velocity = new Vector3(moveInput * moveSpeed, rb.velocity.y, 0);

        // Jumping
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            // Set IsJumping to true
            animator.SetBool("IsJumping", true);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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

        RaycastHit hit;
        if (Physics.Raycast(col.bounds.center, Vector3.down, out hit, col.bounds.extents.y + 0.1f, groundLayer))
        {
            Debug.DrawRay(col.bounds.center, Vector3.down * (col.bounds.extents.y + 0.1f), Color.red);
            return true;
        }
        return false;
    }
}
