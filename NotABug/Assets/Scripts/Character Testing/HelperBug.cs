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
        BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        character.AttemptAbilityFromUpdate();
    }

    private void FixedUpdate()
    {
        character.AttemptAbilityFromFixedUpdate();
    }
}
