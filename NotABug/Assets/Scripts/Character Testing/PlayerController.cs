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
    
    private new Collider2D collider;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float localScale;

    private LayerMask groundLayer;
    private LayerMask waterLayer;

    private const float MAX_SWIM_TIME = 8f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = character.artwork;
        character.rigidBody = GetComponent<Rigidbody2D>();
        character.transform = transform;
        collider = gameObject.GetComponent<Collider2D>();
        groundLayer = LayerMask.GetMask("Ground");
        waterLayer = LayerMask.GetMask("Water");
        normalGravity = character.rigidBody.gravityScale;
        animator = GetComponent<Animator>();
        localScale = transform.localScale.x;
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
        HandleAnimations();
    }

    private void CheckOutOfBounds()
    {
        if (transform.position.y <= -11)
        {
            Die();
        }
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

        if (horizontalMovement > 0)
        {
            transform.localScale = new Vector2(localScale, transform.localScale.y);
        }
        else if (horizontalMovement < 0)
        {
            transform.localScale = new Vector2(-localScale, transform.localScale.y);
        }
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

        animator.SetTrigger("Jump");

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

        float distanceToBoundary = collider.bounds.size.y / 2;
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

    private void HandleAnimations()
    {
        if (0 == horizontalMovement)
        {
            animator.SetTrigger("StopWalk");
        }
        else
        {
            animator.SetTrigger("Walk");
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(this.name + " collided with " + other.gameObject.name);
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

        character.rigidBody.gravityScale = 1f;
        character.rigidBody.velocity = new Vector2(character.rigidBody.velocity.x, -2f);
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

    public void Die()
    {
        health = 0;
        spriteRenderer.transform.Rotate(Vector3.forward * -90);
        animator.SetTrigger("Dead");
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
        character.rigidBody.velocity = new Vector2(0, 0);
    }
}
