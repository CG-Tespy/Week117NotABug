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
    private bool isSwimming = false;
    private float normalGravity;
    private float swimTimeLeft;
    private float health = 100;
    
    private Collider2D colider;
    private SpriteRenderer spriteRenderer;
    private LayerMask groundLayer;
    private LayerMask waterLayer;

    private const float MAX_SWIM_TIME = 8f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = character.artwork;
        character.rigidBody = GetComponent<Rigidbody2D>();
        character.transform = transform;
        colider = gameObject.AddComponent<BoxCollider2D>();
        groundLayer = LayerMask.GetMask("Ground");
        waterLayer = LayerMask.GetMask("Water");
        normalGravity = character.rigidBody.gravityScale;
    }
    
    void Update()
    {
        if (health <= 0)
        {
            return;
        }

        HandleSpecialAbilityUpdate();
        GetInput();
        CheckDrowning();
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            return;
        }

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
            if (isSwimming)
            {
                SwimmingJump();
            }
            else
            {
                Jump();
            }
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

    private void SwimmingJump()
    {
        character.rigidBody.velocity = new Vector2(character.rigidBody.velocity.x, 4f);
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        HandleSwimmingEvent(collider, true);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        HandleSwimmingEvent(collider, false);
    }

    private void HandleSwimmingEvent(Collider2D collider, bool isOnEnter)
    {
        if (collider.gameObject.tag == "Water")
        {
            if (isOnEnter)
            {
                StartSwimming();
            }
            else
            {
                StopSwimming();
            }
        }
    }

    private void StartSwimming()
    {
        swimTimeLeft = MAX_SWIM_TIME;
        Debug.Log("I am swimming my dude");
        isSwimming = true;

        character.rigidBody.gravityScale = 2f;
        character.rigidBody.velocity = new Vector2(character.rigidBody.velocity.x, -3f);
    }

    private void StopSwimming()
    {
        isSwimming = false;
        Debug.Log("I am no longer swimming my dude");
        character.rigidBody.gravityScale = normalGravity;
        swimTimeLeft = MAX_SWIM_TIME;
    }

    private void CheckDrowning()
    {
        if (!isSwimming)
        {
            return;
        }

        swimTimeLeft -= Time.deltaTime;

        if (swimTimeLeft <= 0)
        {
            Debug.Log("You have drowned...");
            Die();
        }
    }

    private void Die()
    {
        health = 0;
        spriteRenderer.transform.Rotate(Vector3.forward * -90);
    }
}
