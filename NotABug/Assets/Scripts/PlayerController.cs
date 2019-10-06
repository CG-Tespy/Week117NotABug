using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_RunSpeed = 15f;
    [SerializeField] float m_JumpPower = 25f;
    [SerializeField] LayerMask groundLayer;
    private float m_HorizontalMovement = 0f;
    private Rigidbody2D m_RigidBody;
    private Collider2D m_Colider;
    private bool m_AttemptJump = false;
    
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Colider = GetComponent<Collider2D>();
    }
    
    void Update()
    {
        m_HorizontalMovement = Input.GetAxisRaw("Horizontal") * m_RunSpeed;
        m_AttemptJump = Input.GetAxisRaw("Vertical") == 1 ? true : false ;
    }

    private void FixedUpdate()
    {
        m_RigidBody.velocity = new Vector2(m_HorizontalMovement, m_RigidBody.velocity.y);

        if (m_AttemptJump)
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
        
        m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, m_JumpPower);
    }

    private bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;

        float distanceToBoundary = m_Colider.bounds.size.y / 2;
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
