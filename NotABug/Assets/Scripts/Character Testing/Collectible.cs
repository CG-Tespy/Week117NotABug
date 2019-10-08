using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    protected string pickupMessage;
    protected AudioClip audioClip;

    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = audioClip;
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
        PlayPickupSound();
        Vanish();
        DisplayMessage();
    }

    private void PlayPickupSound()
    {
        if (null == audioSource)
        {
            return;
        }

        GetComponent<AudioSource>().Play();
    }

    private void Vanish()
    {
        Object.Destroy(gameObject);
    }

    private void DisplayMessage()
    {

    }
}
