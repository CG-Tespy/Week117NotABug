using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Lockable
{
    private Vector2 originalPosition;
    
    void Start()
    {
        originalPosition = transform.position;
    }

    protected override void Unlock()
    {
        base.Unlock();
        transform.position = new Vector2(transform.position.x, transform.position.y + 3);
        Debug.Log("The door has been unlocked.");
    }

    protected override void Lock()
    {
        base.Lock();
        transform.position = originalPosition;
        Debug.Log("The door has been locked.");
    }
}
