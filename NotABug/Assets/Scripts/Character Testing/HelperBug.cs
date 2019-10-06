using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperBug : MonoBehaviour
{
    [SerializeField]
    private Character character;
    
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = character.artwork;
        character.rigidBody = GetComponent<Rigidbody2D>();
        character.transform = transform;
        BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        HandleSpecialAbilityUpdate();
    }

    private void FixedUpdate()
    {
        HandleSpecialAbilityFixedUpdate();
    }

    private void HandleSpecialAbilityUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            character.Update();
        }
    }

    private void HandleSpecialAbilityFixedUpdate()
    {
        character.FixedUpdate();
    }
}
