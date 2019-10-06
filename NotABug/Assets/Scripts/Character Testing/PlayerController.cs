using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float runSpeed = 15f;
    [SerializeField]
    float jumpPower = 25f;
    [SerializeField]
    private Character character;

    private float horizontalMovement = 0f;
    private bool attemptJump = false;
    
    private Collider2D colider;
    private LayerMask groundLayer;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = character.artwork;
        character.rigidBody = GetComponent<Rigidbody2D>();
        character.transform = transform;
        colider = gameObject.AddComponent<BoxCollider2D>();
        groundLayer = LayerMask.GetMask("Ground");
    }
    
    void Update()
    {
        HandleSpecialAbilityUpdate();
        GetInput();
    }

    private void FixedUpdate()
    {
        HandleSpecialAbilityFixedUpdate();
        Move();
    }

    private void GetInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * runSpeed;
        attemptJump = Input.GetAxisRaw("Vertical") == 1 ? true : false;
    }

    private void HandleSpecialAbilityUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            character.Update();
        }
    }

    private void HandleSpecialAbilityFixedUpdate()
    {
        character.FixedUpdate();
    }
    
    private void Move()
    {
        MoveHorizontally();
        MoveVertically();
    }

    private void MoveHorizontally()
    {
        character.rigidBody.velocity = new Vector2(horizontalMovement, character.rigidBody.velocity.y);
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

        character.rigidBody.velocity = new Vector2(character.rigidBody.velocity.x, jumpPower);
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
