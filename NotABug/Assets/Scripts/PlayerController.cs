using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_RunSpeed = 40f;
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
        if (m_HorizontalMovement != 0)
        {
            Debug.Log("Velocity: " + m_RigidBody.velocity);
        }

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
        
        m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, 25f);
    }

    private bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = m_Colider.bounds.size.y / 2 + 0.2f;

        Debug.DrawRay(position, direction * distance, Color.green, 60);

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
