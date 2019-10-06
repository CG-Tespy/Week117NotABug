using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockable : MonoBehaviour
{
    protected bool isLocked = true;

    public void ApplyAction()
    {
        if (isLocked)
        {
            Unlock();
        }
        else
        {
            Lock();
        }
    }

    virtual protected void Unlock()
    {
        isLocked = false;
    }

   virtual protected void Lock()
    {
        isLocked = true;
    }
}
