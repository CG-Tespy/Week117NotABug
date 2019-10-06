using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float runSpeed = 15f;
    [SerializeField] float jumpPower = 25f;
    [SerializeField] LayerMask groundLayer;

    private float horizontalMovement = 0f;
    private bool attemptJump = false;

    private Rigidbody2D rigidBody;
    private Collider2D colider;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        colider = GetComponent<Collider2D>();
    }
    
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void GetInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * runSpeed;
        attemptJump = Input.GetAxisRaw("Vertical") == 1 ? true : false;
    }

    private void Move()
    {
        MoveHorizontally();
        MoveVertically();
    }

    private void MoveHorizontally()
    {
        rigidBody.velocity = new Vector2(horizontalMovement, rigidBody.velocity.y);
    }

    private void MoveVertically()
    {
        if (attemptJump)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (!IsGrounded())
        {
            return;
        }

        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpPower);
    }

    private bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;

        float distanceToBoundary = colider.bounds.size.y / 2;
        float extraPadding = 0.4f;
        float distance = distanceToBoundary + extraPadding;

#if DEBUG
        Debug.DrawRay(position, direction * distance, Color.green, 2);
#endif

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
