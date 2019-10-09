using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    protected string pickupMessage;
    private AudioSource audioSource;
    
    protected void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag != "Player")
        {
            return;
        }

        Debug.Log("Collision with player detected");
        OnPickUp();
    }

    virtual protected void OnPickUp()
    {
        Vanish();
        PlayPickupSound();
        DisplayMessage();
        Destruct();
    }

    private void PlayPickupSound()
    {
        if (null == audioSource)
        {
            return;
        }

        audioSource.PlayOneShot(audioSource.clip);
    }

    private void Vanish()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    private void DisplayMessage()
    {

    }

    private void Destruct()
    {
        float bufferTimeToEnsureSoundPlays = audioSource.clip.length;
        Object.Destroy(gameObject, bufferTimeToEnsureSoundPlays);
    }
}
